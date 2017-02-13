namespace BigFileExplorer
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewRoot = new System.Windows.Forms.ListView();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewRoot
            // 
            this.listViewRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(52)))), ((int)(((byte)(43)))));
            this.listViewRoot.Font = new System.Drawing.Font("맑은 고딕", 22F);
            this.listViewRoot.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.listViewRoot.FullRowSelect = true;
            this.listViewRoot.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewRoot.Location = new System.Drawing.Point(53, 46);
            this.listViewRoot.MultiSelect = false;
            this.listViewRoot.Name = "listViewRoot";
            this.listViewRoot.Size = new System.Drawing.Size(248, 490);
            this.listViewRoot.TabIndex = 4;
            this.listViewRoot.UseCompatibleStateImageBehavior = false;
            this.listViewRoot.View = System.Windows.Forms.View.Details;
            // 
            // listViewFiles
            // 
            this.listViewFiles.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(52)))), ((int)(((byte)(43)))));
            this.listViewFiles.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.listViewFiles.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.listViewFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFiles.Location = new System.Drawing.Point(320, 46);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.OwnerDraw = true;
            this.listViewFiles.Size = new System.Drawing.Size(624, 563);
            this.listViewFiles.TabIndex = 5;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonExit.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonExit.Location = new System.Drawing.Point(53, 571);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(110, 45);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.TabStop = false;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.listViewRoot);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Main";
            this.Text = "Big File Explorer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewRoot;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.Button buttonExit;
    }
}

