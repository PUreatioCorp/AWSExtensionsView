using RekognitionExtensions;
using RekognitionExtensions.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RekognitionDetectTextRelationshipsView
{
    public partial class RekognitionDetectTextRelationshipsView : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RekognitionDetectTextRelationshipsView()
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

            Dictionary<ValueDto, List<ValueDto>> textRelationships = Analyzer.AnalyzeDetectText(filePath);
            this.relationTreeView.Nodes.Clear();

            // TreeViewを構成する。
            this.SetTextRelationships(textRelationships);
        }

        /// <summary>
        /// テキストの紐付けをTreeViewに設定
        /// </summary>
        /// <param name="textRelationships">テキストの紐付け</param>
        private void SetTextRelationships(Dictionary<ValueDto, List<ValueDto>> textRelationships)
        {
            foreach (KeyValuePair<ValueDto, List<ValueDto>> linePair in textRelationships)
            {
                // LINEの情報を設定する。
                TreeNode lineNode = new TreeNode(string.Format("{0}({1})", linePair.Key.Text, linePair.Key.ID));

                foreach (ValueDto wordDto in linePair.Value)
                {
                    TreeNode wordNode = new TreeNode(string.Format("{0}({1})", wordDto.Text, wordDto.ID));
                    lineNode.Nodes.Add(wordNode);
                }

                this.relationTreeView.Nodes.Add(lineNode);
            }
        }
    }
}
