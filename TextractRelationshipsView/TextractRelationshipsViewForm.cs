using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextractExtensions;
using TextractExtensions.Dto;

namespace TextractRelationshipsView
{
    public partial class TextractRelationshipsViewForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextractRelationshipsViewForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ドラッグ開始イベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void relationTreeView_DragEnter(object sender, DragEventArgs e)
        {
            // ファイルの場合、エフェクトをAllに変更する
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// ドロップイベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void relationTreeView_DragDrop(object sender, DragEventArgs e)
        {
            // ドロップされたファイル情報を取得する。
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            string filePath = files[0];

            Dictionary<string, Dictionary<ValueDto, List<ValueDto>>> textRelationships = Analyzer.AnalyzeDetectDocumentText(filePath);
            this.relationTreeView.Nodes.Clear();

            // TreeViewを構成する。
            this.SetTextRelationships(textRelationships);
        }

        /// <summary>
        /// テキストの紐付けをTreeViewに設定
        /// </summary>
        /// <param name="textRelationships">テキストの紐付け</param>
        private void SetTextRelationships(Dictionary<string, Dictionary<ValueDto, List<ValueDto>>> textRelationships)
        {
            foreach(KeyValuePair<string, Dictionary<ValueDto, List<ValueDto>>> textRelationPair in textRelationships)
            {
                // PAGEのIDを設定する。
                TreeNode pageNode = new TreeNode(textRelationPair.Key);

                foreach(KeyValuePair<ValueDto, List<ValueDto>> linePair in textRelationPair.Value)
                {
                    // LINEの情報を設定する。
                    TreeNode lineNode = new TreeNode(string.Format("{0}({1})", linePair.Key.Text, linePair.Key.ID));

                    foreach(ValueDto wordDto in linePair.Value)
                    {
                        TreeNode wordNode = new TreeNode(string.Format("{0}({1})", wordDto.Text, wordDto.ID));
                        lineNode.Nodes.Add(wordNode);
                    }

                    pageNode.Nodes.Add(lineNode);
                }

                this.relationTreeView.Nodes.Add(pageNode);
            }
        }
    }
}
