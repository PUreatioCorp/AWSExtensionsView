namespace TextractRelationshipsView
{
    partial class TextractRelationshipsViewForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.relationTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // relationTreeView
            // 
            this.relationTreeView.AllowDrop = true;
            this.relationTreeView.Location = new System.Drawing.Point(12, 12);
            this.relationTreeView.Name = "relationTreeView";
            this.relationTreeView.Size = new System.Drawing.Size(505, 407);
            this.relationTreeView.TabIndex = 0;
            this.relationTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.relationTreeView_DragDrop);
            this.relationTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.relationTreeView_DragEnter);
            // 
            // TextractRelationshipsViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 431);
            this.Controls.Add(this.relationTreeView);
            this.Name = "TextractRelationshipsViewForm";
            this.Text = "TextractRelationshipsView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView relationTreeView;
    }
}

