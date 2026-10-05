namespace MGestures.App.Gui.Windows
{
    partial class UpdateInfoForm
    {
        private System.ComponentModel.IContainer? components;
        private Label lb_versions = null!;
        private Label lb_status = null!;
        private TextBox tb_whatsNew = null!;
        private ProgressBar progress_download = null!;
        private Button btn_update = null!;
        private Button btn_skip = null!;
        private Button btn_later = null!;
        private LinkLabel lnk_download = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _download?.Cancel();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6, Padding = new Padding(16) };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            var heading = new Label { Text = "MGestures 有新版本可用", AutoSize = true, Font = new Font("Microsoft YaHei UI", 12, FontStyle.Bold), Margin = new Padding(3, 3, 3, 12) };
            lb_versions = new Label { AutoSize = true, Margin = new Padding(3, 3, 3, 12) };
            tb_whatsNew = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            lb_status = new Label { AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(3, 10, 3, 8) };
            progress_download = new ProgressBar { Dock = DockStyle.Fill };
            var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, WrapContents = true, Margin = new Padding(0, 12, 0, 0) };
            btn_update = new Button { Text = "更新软件", AutoSize = true, Padding = new Padding(8, 4, 8, 4) };
            btn_skip = new Button { Text = "跳过此版本", AutoSize = true, Padding = new Padding(8, 4, 8, 4) };
            btn_later = new Button { Text = "稍后提醒", AutoSize = true, Padding = new Padding(8, 4, 8, 4) };
            lnk_download = new LinkLabel { Text = "手动下载", AutoSize = true, Margin = new Padding(12, 12, 3, 3) };
            btn_update.Click += btn_update_Click;
            btn_skip.Click += btn_skip_Click;
            btn_later.Click += btn_later_Click;
            lnk_download.LinkClicked += lnk_download_LinkClicked;
            actions.Controls.AddRange(new Control[] { btn_update, btn_skip, btn_later, lnk_download });
            layout.Controls.Add(heading, 0, 0);
            layout.Controls.Add(lb_versions, 0, 1);
            layout.Controls.Add(tb_whatsNew, 0, 2);
            layout.Controls.Add(lb_status, 0, 3);
            layout.Controls.Add(progress_download, 0, 4);
            layout.Controls.Add(actions, 0, 5);
            Controls.Add(layout);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScaleDimensions = new SizeF(96, 96);
            ClientSize = new Size(580, 440);
            MinimumSize = new Size(520, 380);
            Font = new Font("Microsoft YaHei UI", 9);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MGestures 更新";
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btn_update;
        }
    }
}
