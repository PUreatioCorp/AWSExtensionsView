using RekognitionExtensions;
using RekognitionExtensions.Const;
using RekognitionExtensions.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RekognitionFaceLandmark
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ドラッグイベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void window_DragOver(object sender, DragEventArgs e)
        {
            // ファイルの場合、エフェクトをAllに変更する
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Handled = true;
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// ドロップイベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void window_Drop(object sender, DragEventArgs e)
        {
            // ドロップされたファイル情報を取得する。
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            string filePath = files[0];
            this.faceCanvas.Children.Clear();

            // 取得したファイルから顔の座標情報を取得する。
            List<FaceLandmark> faceLandmarks = Analyzer.AnalyzeDetectFaces(filePath);
            // 顔の座標情報からLineをCanvasに描く
            foreach (FaceLandmark faceLandmark in faceLandmarks)
            {
                // 口の情報を描く
                this.DrawFaceLandmark(faceLandmark.Mouth, true, Landmarks.MOUTH_LEFT, Landmarks.MOUTH_UP, Landmarks.MOUTH_RIGHT, Landmarks.MOUTH_DOWN);
                // 鼻の情報を描く
                this.DrawFaceLandmark(faceLandmark.Nose, true, Landmarks.NOSE_LEFT, Landmarks.NOSE, Landmarks.NOSE_RIGHT);
                // 左眉の情報を描く
                this.DrawFaceLandmark(faceLandmark.LeftEyeBrow, false, Landmarks.LEFT_EYE_BROW_LEFT, Landmarks.LEFT_EYE_BROW_UP, Landmarks.LEFT_EYE_BROW_RIGHT);
                // 右眉の情報を描く
                this.DrawFaceLandmark(faceLandmark.RightEyeBrow, false, Landmarks.RIGHT_EYE_BROW_LEFT, Landmarks.RIGHT_EYE_BROW_UP, Landmarks.RIGHT_EYE_BROW_RIGHT);
                // 左目の情報を描く
                this.DrawFaceLandmark(faceLandmark.LeftEye, true, Landmarks.LEFT_EYE_LEFT, Landmarks.LEFT_EYE_UP, Landmarks.LEFT_EYE_RIGHT, Landmarks.LEFT_EYE_DOWN);
                // 右目の情報を描く
                this.DrawFaceLandmark(faceLandmark.RightEye, true, Landmarks.RIGHT_EYE_LEFT, Landmarks.RIGHT_EYE_UP, Landmarks.RIGHT_EYE_RIGHT, Landmarks.RIGHT_EYE_DOWN);
                // 下あご輪郭の情報を描く
                this.DrawFaceLandmark(faceLandmark.Jawline, false, Landmarks.UPPER_JAWLINE_LEFT, Landmarks.MID_JAWLINE_LEFT, Landmarks.CHIN_BOTTOM, Landmarks.MID_JAWLINE_RIGHT, Landmarks.UPPER_JAWLINE_RIGHT);
            }

            this.faceCanvas.UpdateLayout();
        }

        /// <summary>
        /// 顔座標情報をCanvasに描画する
        /// </summary>
        /// <param name="landmark">座標情報</param>
        /// <param name="isSurround">最初と最後の座標を繋ぐか否かのフラグ</param>
        /// <param name="keys">描画対象キー</param>
        private void DrawFaceLandmark(Dictionary<string, PointF> landmark, bool isSurround, params string[] keys)
        {
            PointF firstPoint = new PointF();
            PointF prevPoint = new PointF();
            // Aquaの線で描画する。
            SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Colors.Aqua);

            for (int i = 0; i < keys.Length; i++)
            {
                PointF keyPoint = landmark[keys[i]];
                // 最初と最後を繋ぐ場合、座標情報を保持しておく。
                if ((i == 0) && isSurround)
                {
                    firstPoint = keyPoint;
                }

                // 2番目以降の場合、Lineを形成し、描画する。
                if (i > 0)
                {
                    Line line = new Line() { Stroke = brush };
                    line.X1 = prevPoint.X * this.faceCanvas.Width;
                    line.X2 = keyPoint.X * this.faceCanvas.Width;
                    line.Y1 = prevPoint.Y * this.faceCanvas.Height;
                    line.Y2 = keyPoint.Y * this.faceCanvas.Height;
                    this.faceCanvas.Children.Add(line);
                }

                prevPoint = keyPoint;
            }

            // 最初と最後の座標を描画する。
            if (isSurround)
            {
                Line line = new Line() { Stroke = brush };
                line.X1 = prevPoint.X * this.faceCanvas.Width;
                line.X2 = firstPoint.X * this.faceCanvas.Width;
                line.Y1 = prevPoint.Y * this.faceCanvas.Height;
                line.Y2 = firstPoint.Y * this.faceCanvas.Height;
                this.faceCanvas.Children.Add(line);
            }
        }
    }
}
