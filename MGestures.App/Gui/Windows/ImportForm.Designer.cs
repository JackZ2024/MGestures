namespace MGestures.App.Gui.Windows
{
    partial class ImportForm
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
            txt_filePath = new TextBox();
            btn_selectWgb = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            group_importOptions = new GroupBox();
            check_importConfig = new CheckBox();
            check_importGestures = new CheckBox();
            combo_importGesturesOption = new ComboBox();
            flowAlert = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            lb_errMsg = new Label();
            flowLayoutPanel4 = new FlowLayoutPanel();
            btnCancel = new Button();
            btnOk = new Button();
            openFile_wgb = new OpenFileDialog();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            group_importOptions.SuspendLayout();
            flowAlert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // txt_filePath
            // 
            txt_filePath.Location = new Point(0, 4);
            txt_filePath.Margin = new Padding(0, 4, 4, 4);
            txt_filePath.Name = "txt_filePath";
            txt_filePath.ReadOnly = true;
            txt_filePath.Size = new Size(240, 27);
            txt_filePath.TabIndex = 0;
            txt_filePath.TabStop = false;
            txt_filePath.Text = "请选择要导入的文件";
            txt_filePath.WordWrap = false;
            // 
            // btn_selectWgb
            // 
            btn_selectWgb.Location = new Point(248, 4);
            btn_selectWgb.Margin = new Padding(4, 4, 0, 4);
            btn_selectWgb.Name = "btn_selectWgb";
            btn_selectWgb.Size = new Size(94, 26);
            btn_selectWgb.TabIndex = 1;
            btn_selectWgb.Text = "选择...";
            btn_selectWgb.UseVisualStyleBackColor = true;
            btn_selectWgb.Click += btn_selectWgb_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(txt_filePath);
            flowLayoutPanel1.Controls.Add(btn_selectWgb);
            flowLayoutPanel1.Location = new Point(14, 14);
            flowLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(342, 35);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(flowLayoutPanel1);
            flowLayoutPanel2.Controls.Add(group_importOptions);
            flowLayoutPanel2.Controls.Add(flowAlert);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Margin = new Padding(4, 4, 4, 4);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(10, 10, 10, 10);
            flowLayoutPanel2.Size = new Size(378, 247);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // group_importOptions
            // 
            group_importOptions.Controls.Add(check_importConfig);
            group_importOptions.Controls.Add(check_importGestures);
            group_importOptions.Controls.Add(combo_importGesturesOption);
            group_importOptions.Location = new Point(14, 73);
            group_importOptions.Margin = new Padding(4, 20, 4, 4);
            group_importOptions.Name = "group_importOptions";
            group_importOptions.Padding = new Padding(4, 4, 4, 4);
            group_importOptions.Size = new Size(342, 112);
            group_importOptions.TabIndex = 4;
            group_importOptions.TabStop = false;
            group_importOptions.Text = "导入方式";
            group_importOptions.Visible = false;
            // 
            // check_importConfig
            // 
            check_importConfig.AutoSize = true;
            check_importConfig.Location = new Point(21, 72);
            check_importConfig.Margin = new Padding(4, 4, 4, 4);
            check_importConfig.Name = "check_importConfig";
            check_importConfig.Size = new Size(164, 24);
            check_importConfig.TabIndex = 2;
            check_importConfig.Text = "导入MGestures选项";
            check_importConfig.UseVisualStyleBackColor = true;
            check_importConfig.CheckedChanged += check_importConfig_CheckedChanged;
            // 
            // check_importGestures
            // 
            check_importGestures.AutoSize = true;
            check_importGestures.Location = new Point(21, 34);
            check_importGestures.Margin = new Padding(4, 4, 4, 4);
            check_importGestures.Name = "check_importGestures";
            check_importGestures.Size = new Size(95, 24);
            check_importGestures.TabIndex = 2;
            check_importGestures.Text = "导入手势";
            check_importGestures.UseVisualStyleBackColor = true;
            check_importGestures.CheckedChanged += check_importGestures_CheckedChanged;
            // 
            // combo_importGesturesOption
            // 
            combo_importGesturesOption.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_importGesturesOption.Enabled = false;
            combo_importGesturesOption.FormattingEnabled = true;
            combo_importGesturesOption.Items.AddRange(new object[] { "合并现有手势", "替换现有手势" });
            combo_importGesturesOption.Location = new Point(119, 31);
            combo_importGesturesOption.Margin = new Padding(4, 4, 4, 4);
            combo_importGesturesOption.Name = "combo_importGesturesOption";
            combo_importGesturesOption.Size = new Size(189, 28);
            combo_importGesturesOption.TabIndex = 1;
            combo_importGesturesOption.SelectedIndexChanged += combo_importGesturesOption_SelectedIndexChanged;
            // 
            // flowAlert
            // 
            flowAlert.AutoSize = true;
            flowAlert.Controls.Add(pictureBox1);
            flowAlert.Controls.Add(lb_errMsg);
            flowAlert.Location = new Point(14, 199);
            flowAlert.Margin = new Padding(4, 10, 4, 4);
            flowAlert.Name = "flowAlert";
            flowAlert.Size = new Size(77, 28);
            flowAlert.TabIndex = 8;
            flowAlert.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Alert;
            pictureBox1.Location = new Point(4, 4);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 19);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // lb_errMsg
            // 
            lb_errMsg.AutoSize = true;
            lb_errMsg.ForeColor = Color.Red;
            lb_errMsg.ImageAlign = ContentAlignment.MiddleLeft;
            lb_errMsg.Location = new Point(32, 4);
            lb_errMsg.Margin = new Padding(4, 4, 4, 4);
            lb_errMsg.Name = "lb_errMsg";
            lb_errMsg.Size = new Size(41, 20);
            lb_errMsg.TabIndex = 4;
            lb_errMsg.Text = "错误";
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.BackColor = Color.WhiteSmoke;
            flowLayoutPanel4.Controls.Add(btnCancel);
            flowLayoutPanel4.Controls.Add(btnOk);
            flowLayoutPanel4.Dock = DockStyle.Bottom;
            flowLayoutPanel4.Location = new Point(0, 247);
            flowLayoutPanel4.Margin = new Padding(10, 22, 10, 10);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new Padding(8, 8, 8, 8);
            flowLayoutPanel4.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel4.Size = new Size(378, 52);
            flowLayoutPanel4.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(270, 10);
            btnCancel.Margin = new Padding(2, 2, 12, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 32);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Enabled = false;
            btnOk.Location = new Point(166, 10);
            btnOk.Margin = new Padding(2);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(90, 32);
            btnOk.TabIndex = 4;
            btnOk.Text = "导入";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // openFile_wgb
            // 
            openFile_wgb.DefaultExt = "wgb";
            openFile_wgb.Filter = "MGestures备份文件 (*.wgb)|*.wgb|MGestures 1.2手势文件|*.json";
            openFile_wgb.Title = "选择要导入的文件";
            // 
            // ImportForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(378, 299);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel4);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ImportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "导入到MGestures";
            Load += ImportForm_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            group_importOptions.ResumeLayout(false);
            group_importOptions.PerformLayout();
            flowAlert.ResumeLayout(false);
            flowAlert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_filePath;
        private System.Windows.Forms.Button btn_selectWgb;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox group_importOptions;
        private System.Windows.Forms.ComboBox combo_importGesturesOption;
        private System.Windows.Forms.CheckBox check_importConfig;
        private System.Windows.Forms.CheckBox check_importGestures;
        private System.Windows.Forms.OpenFileDialog openFile_wgb;
        private System.Windows.Forms.FlowLayoutPanel flowAlert;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_errMsg;
    }
}