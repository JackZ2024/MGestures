using System;

namespace MGestures.App.Gui.Windows
{
    partial class QuickStartGuideForm
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
            if (web_container != null)
            {
                web_container.Dispose();
                web_container = null;
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
            web_container = new WebBrowser();
            SuspendLayout();
            // 
            // web_container
            // 
            web_container.AllowNavigation = false;
            web_container.AllowWebBrowserDrop = false;
            web_container.Dock = DockStyle.Fill;
            web_container.IsWebBrowserContextMenuEnabled = false;
            web_container.Location = new Point(0, 0);
            web_container.Margin = new Padding(0);
            web_container.MinimumSize = new Size(16, 16);
            web_container.Name = "web_container";
            web_container.ScriptErrorsSuppressed = true;
            web_container.ScrollBarsEnabled = false;
            web_container.Size = new Size(1150, 620);
            web_container.TabIndex = 0;
            web_container.WebBrowserShortcutsEnabled = false;
            web_container.PreviewKeyDown += web_container_PreviewKeyDown;
            // 
            // QuickStartGuideForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1150, 620);
            Controls.Add(web_container);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QuickStartGuideForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MGestures快速入门";
            Load += QuickStartGuidForm_Load;
            MouseEnter += QuickStartGuideForm_MouseEnter;
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser web_container;

    }
}