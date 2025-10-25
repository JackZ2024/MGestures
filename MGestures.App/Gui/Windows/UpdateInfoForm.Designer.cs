namespace MGestures.App.Gui.Windows
{
    partial class UpdateInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateInfoForm));
            tb_whatsNew = new TextBox();
            label1 = new Label();
            label2 = new Label();
            lnk_gotoUrl = new LinkLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label3 = new Label();
            lb_currentVersion = new Label();
            label5 = new Label();
            lb_newVersion = new Label();
            btn_ok = new Button();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tb_whatsNew
            // 
            tb_whatsNew.Font = new Font("Microsoft YaHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tb_whatsNew.Location = new Point(4, 28);
            tb_whatsNew.Margin = new Padding(4, 4, 4, 4);
            tb_whatsNew.Multiline = true;
            tb_whatsNew.Name = "tb_whatsNew";
            tb_whatsNew.ReadOnly = true;
            tb_whatsNew.ScrollBars = ScrollBars.Vertical;
            tb_whatsNew.Size = new Size(425, 178);
            tb_whatsNew.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 0);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(286, 24);
            label1.TabIndex = 1;
            label1.Text = "MGestures发布了新版本！";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 0);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 0, 0, 4);
            label2.Size = new Size(108, 24);
            label2.TabIndex = 2;
            label2.Text = "主要更新内容:";
            // 
            // lnk_gotoUrl
            // 
            lnk_gotoUrl.AutoSize = true;
            lnk_gotoUrl.Location = new Point(298, 0);
            lnk_gotoUrl.Margin = new Padding(4, 0, 4, 0);
            lnk_gotoUrl.Name = "lnk_gotoUrl";
            lnk_gotoUrl.Size = new Size(106, 24);
            lnk_gotoUrl.TabIndex = 3;
            lnk_gotoUrl.TabStop = true;
            lnk_gotoUrl.Text = "前往下载";
            lnk_gotoUrl.LinkClicked += lnk_gotoUrl_LinkClicked;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(lnk_gotoUrl);
            flowLayoutPanel1.Font = new Font("SimSun", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            flowLayoutPanel1.Location = new Point(14, 12);
            flowLayoutPanel1.Margin = new Padding(4, 2, 4, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(408, 24);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(label3);
            flowLayoutPanel2.Controls.Add(lb_currentVersion);
            flowLayoutPanel2.Controls.Add(label5);
            flowLayoutPanel2.Controls.Add(lb_newVersion);
            flowLayoutPanel2.Location = new Point(14, 48);
            flowLayoutPanel2.Margin = new Padding(4, 2, 4, 2);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(260, 20);
            flowLayoutPanel2.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 0);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 1;
            label3.Text = "当前版本:";
            // 
            // lb_currentVersion
            // 
            lb_currentVersion.AutoSize = true;
            lb_currentVersion.ForeColor = Color.Gray;
            lb_currentVersion.Location = new Point(88, 0);
            lb_currentVersion.Margin = new Padding(4, 0, 4, 0);
            lb_currentVersion.Name = "lb_currentVersion";
            lb_currentVersion.Size = new Size(46, 20);
            lb_currentVersion.TabIndex = 1;
            lb_currentVersion.Text = "x.x.x.x";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(142, 0);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 1;
            label5.Text = "新版本:";
            // 
            // lb_newVersion
            // 
            lb_newVersion.AutoSize = true;
            lb_newVersion.ForeColor = Color.DeepSkyBlue;
            lb_newVersion.Location = new Point(210, 0);
            lb_newVersion.Margin = new Padding(4, 0, 4, 0);
            lb_newVersion.Name = "lb_newVersion";
            lb_newVersion.Size = new Size(46, 20);
            lb_newVersion.TabIndex = 1;
            lb_newVersion.Text = "y.y.y.y";
            // 
            // btn_ok
            // 
            btn_ok.Anchor = AnchorStyles.None;
            btn_ok.Location = new Point(142, 305);
            btn_ok.Margin = new Padding(4, 2, 4, 10);
            btn_ok.Name = "btn_ok";
            btn_ok.Size = new Size(165, 29);
            btn_ok.TabIndex = 6;
            btn_ok.Text = "关闭";
            btn_ok.UseVisualStyleBackColor = true;
            btn_ok.Click += btn_ok_Click;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Controls.Add(tb_whatsNew);
            flowLayoutPanel3.Location = new Point(14, 82);
            flowLayoutPanel3.Margin = new Padding(4, 4, 4, 4);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(435, 214);
            flowLayoutPanel3.TabIndex = 7;
            // 
            // UpdateInfoForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(458, 361);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(btn_ok);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "UpdateInfoForm";
            Padding = new Padding(10, 10, 10, 10);
            StartPosition = FormStartPosition.CenterParent;
            Text = "新版本可用";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_whatsNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnk_gotoUrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_currentVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_newVersion;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}