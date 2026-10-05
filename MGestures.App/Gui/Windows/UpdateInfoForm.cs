using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using MGestures.Common.Product;

namespace MGestures.App.Gui.Windows
{
    public partial class UpdateInfoForm : Form
    {
        private readonly VersionInfo _version;
        private readonly Action<string> _saveSkipped;
        private CancellationTokenSource? _download;
        private bool _ready;
        internal event Action? UpdateReady;

        public UpdateInfoForm(VersionInfo version, Action<string> saveSkipped)
        {
            _version = version;
            _saveSkipped = saveSkipped;
            InitializeComponent();
            lb_versions.Text = $"当前版本：{Application.ProductVersion}\r\n新版本：{version.Version}";
            tb_whatsNew.Text = version.WhatsNew;
            btn_update.Enabled = version.CanAutoUpdate;
            lb_status.Text = version.CanAutoUpdate ? "更新完成后将自动退出并重新启动软件。" : "此 Release 缺少合格的更新附件，请点击“手动下载”。";
        }

        private void btn_skip_Click(object? sender, EventArgs e)
        {
            try
            {
                _saveSkipped(_version.NormalizedVersion);
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "未能保存跳过记录：" + error.Message + "\n此版本尚未被跳过。", "MGestures 更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_later_Click(object? sender, EventArgs e)
        {
            if (_download != null) _download.Cancel();
            else Close();
        }

        private async void btn_update_Click(object? sender, EventArgs e)
        {
            if (_download != null || _ready) return;
            // Persist pending settings/gestures before preparing an update; failures must leave the current process running.
            try { Program.SaveBeforeUpdate(); }
            catch (Exception error)
            {
                MessageBox.Show(this, "保存配置失败，未开始更新：" + error.Message, "MGestures 更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _download = new CancellationTokenSource();
            btn_update.Enabled = btn_skip.Enabled = lnk_download.Enabled = false;
            btn_later.Text = "取消下载";
            try
            {
                lb_status.Text = "正在下载更新包…";
                var progress = new Progress<int>(value =>
                {
                    if (IsDisposed) return;
                    progress_download.Value = Math.Clamp(value, 0, 100);
                    lb_status.Text = value < 100 ? $"正在下载：{value}%" : "正在校验、解压并准备更新…";
                });
                await UpdateService.PrepareAsync(_version, progress, _download.Token);
                _ready = true;
                // Close the dialog first, then exit the main message loop through the existing cleanup path.
                _download.Dispose();
                _download = null;
                Close();
                UpdateReady?.Invoke();
            }
            catch (OperationCanceledException)
            {
                if (!IsDisposed) lb_status.Text = "下载已取消，当前软件继续运行。";
            }
            catch (Exception error)
            {
                if (IsDisposed) return;
                lb_status.Text = "更新失败，当前软件继续运行。";
                MessageBox.Show(this, error.Message, "MGestures 更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                _download?.Dispose();
                _download = null;
                if (!IsDisposed)
                {
                    btn_update.Enabled = _version.CanAutoUpdate;
                    btn_skip.Enabled = lnk_download.Enabled = true;
                    btn_later.Text = "稍后提醒";
                }
            }
        }

        private void lnk_download_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!Uri.TryCreate(_version.ReleaseUrl, UriKind.Absolute, out var uri) || uri.Scheme != "https" || uri.Host != "github.com")
                    throw new InvalidOperationException("Release 页面地址无效。");
                using var browser = Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });
            }
            catch (Exception error) { MessageBox.Show(this, "无法打开下载页面：" + error.Message, "MGestures 更新"); }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_download != null)
            {
                _download.Cancel();
                e.Cancel = true;
                lb_status.Text = "正在取消，请稍候…";
            }
            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == (Keys.Control | Keys.W))
            {
                btn_later.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
