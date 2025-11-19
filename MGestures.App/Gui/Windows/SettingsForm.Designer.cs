using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using MGestures.App.Gui.Windows.Controls;
using MGestures.App.Properties;
using MGestures.Common.OsSpecific.Windows;

namespace MGestures.App.Gui.Windows
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

            if (disposing)
            {
                Icon.Dispose();
                Icon = null;

                //循环引用会造成内存泄漏
                Controller = null;
                imglistAppIcons = null;
                dummyImgLstForLstViewHeightFix = null;


                if (_versionChecker != null)
                {
                    _versionChecker.Dispose();
                    _versionChecker = null;
                }
                //Resources.ResourceManager.ReleaseAllResources();

                //GC.Collect();

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
            components = new Container();
            tabControl = new TabControl();
            tabPage_general = new TabPage();
            groupBox2 = new GroupBox();
            check_preferCursorWindow = new CheckBox();
            settingsFormControllerBindingSource = new BindingSource(components);
            check_enable8DirGesture = new CheckBox();
            label3 = new Label();
            lineLabel2 = new LineLabel();
            check_disableOnFullscreen = new CheckBox();
            num_pathTrackerInitialStayTimeoutMillis = new InstantNumericUpDown();
            check_pathTrackerInitialStayTimeout = new CheckBox();
            label9 = new Label();
            colorMiddle = new ColorButton();
            colorBtn_recogonized = new ColorButton();
            colorBtn_x = new ColorButton();
            colorBtn_unrecogonized = new ColorButton();
            numPathTrackerStayTimeoutMillis = new InstantNumericUpDown();
            numPathTrackerInitialValidMove = new InstantNumericUpDown();
            checkBox1 = new CheckBox();
            check_gestBtn_X = new CheckBox();
            checkGestureView_fadeOut = new CheckBox();
            check_gestBtn_Middle = new CheckBox();
            checkGestureViewShowCommandName = new CheckBox();
            checkPathTrackerStayTimeout = new CheckBox();
            check_gestBtn_Right = new CheckBox();
            checkGestureViewShowPath = new CheckBox();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            groupBox1 = new GroupBox();
            shortcutRec_pause = new ShortcutRecordButton();
            flowLayoutPanel3 = new FlowLayoutPanel();
            label4 = new Label();
            lb_Version = new Label();
            check_autoStart = new CheckBox();
            btn_checkUpdateNow = new Button();
            check_autoCheckUpdate = new CheckBox();
            lb_pause_shortcut = new Label();
            label14 = new Label();
            tabPage2 = new TabPage();
            flowLayoutPanel2 = new FlowLayoutPanel();
            panel1 = new Panel();
            checkInheritGlobal = new CheckBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            check_gesturingDisabled = new CheckBox();
            pictureSelectedApp = new PictureBox();
            label15 = new Label();
            label7 = new Label();
            listGestureIntents = new AlwaysSelectedListView();
            colGestureName = new ColumnHeader();
            colGestureDirs = new ColumnHeader();
            operation = new ColumnHeader();
            dummyImgLstForLstViewHeightFix = new ImageList(components);
            panel_intentListOperations = new Panel();
            btn_RemoveGesture = new MetroButton();
            btn_modifyGesture = new MetroButton();
            btnAddGesture = new MetroButton();
            group_Command = new GroupBox();
            flowLayoutPanel6 = new FlowLayoutPanel();
            panel3 = new Panel();
            label8 = new Label();
            combo_CommandTypes = new ComboBox();
            check_executeOnMouseWheeling = new CheckBox();
            lineLabel1 = new LineLabel();
            panel_commandView = new Panel();
            btnEditApp = new MetroButton();
            btnAppRemove = new MetroButton();
            btnAddApp = new MetroButton();
            listApps = new AlwaysSelectedListView();
            colListAppDummy = new ColumnHeader();
            imglistAppIcons = new ImageList(components);
            tab_hotCorners = new TabPage();
            panel_hotcornerSettings = new Panel();
            radio_edge_0 = new RadioButton();
            radio_edge_2 = new RadioButton();
            radio_edge_1 = new RadioButton();
            radio_edge_3 = new RadioButton();
            radio_corner_1 = new RadioButton();
            label11 = new Label();
            panel2 = new Panel();
            label13 = new Label();
            combo_hotcornerCmdTypes = new ComboBox();
            panel_cornorCmdView = new Panel();
            radio_corner_0 = new RadioButton();
            radio_corner_2 = new RadioButton();
            radio_corner_3 = new RadioButton();
            check_enableRubEdge = new CheckBox();
            check_enableHotCorners = new CheckBox();
            tabPage1 = new TabPage();
            tb_updateLog = new TextBox();
            picture_logo = new PictureBox();
            flowLayoutPanel4 = new FlowLayoutPanel();
            lb_info = new Label();
            tip = new ToolTip(components);
            pic_menuBtn = new PictureBox();
            ctx_gesturesMenu = new ContextMenuStrip(components);
            menuItem_import = new ToolStripMenuItem();
            menuItem_export = new ToolStripMenuItem();
            menuItem_resetGestures = new ToolStripMenuItem();
            errorProvider = new ErrorProvider(components);
            tabControl.SuspendLayout();
            tabPage_general.SuspendLayout();
            groupBox2.SuspendLayout();
            ((ISupportInitialize)settingsFormControllerBindingSource).BeginInit();
            ((ISupportInitialize)num_pathTrackerInitialStayTimeoutMillis).BeginInit();
            ((ISupportInitialize)numPathTrackerStayTimeoutMillis).BeginInit();
            ((ISupportInitialize)numPathTrackerInitialValidMove).BeginInit();
            groupBox1.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tabPage2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((ISupportInitialize)pictureSelectedApp).BeginInit();
            panel_intentListOperations.SuspendLayout();
            group_Command.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            panel3.SuspendLayout();
            tab_hotCorners.SuspendLayout();
            panel_hotcornerSettings.SuspendLayout();
            panel2.SuspendLayout();
            tabPage1.SuspendLayout();
            ((ISupportInitialize)picture_logo).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            ((ISupportInitialize)pic_menuBtn).BeginInit();
            ctx_gesturesMenu.SuspendLayout();
            ((ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage_general);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tab_hotCorners);
            tabControl.Controls.Add(tabPage1);
            tabControl.HotTrack = true;
            tabControl.ItemSize = new Size(250, 28);
            tabControl.Location = new Point(15, 15);
            tabControl.Margin = new Padding(12);
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(20, 3);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(837, 756);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabPage_general
            // 
            tabPage_general.BackColor = Color.White;
            tabPage_general.Controls.Add(groupBox2);
            tabPage_general.Controls.Add(groupBox1);
            tabPage_general.Location = new Point(4, 32);
            tabPage_general.Margin = new Padding(0);
            tabPage_general.Name = "tabPage_general";
            tabPage_general.Size = new Size(829, 720);
            tabPage_general.TabIndex = 0;
            tabPage_general.Tag = "general";
            tabPage_general.Text = "选 项";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(check_preferCursorWindow);
            groupBox2.Controls.Add(check_enable8DirGesture);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(lineLabel2);
            groupBox2.Controls.Add(check_disableOnFullscreen);
            groupBox2.Controls.Add(num_pathTrackerInitialStayTimeoutMillis);
            groupBox2.Controls.Add(check_pathTrackerInitialStayTimeout);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(colorMiddle);
            groupBox2.Controls.Add(colorBtn_recogonized);
            groupBox2.Controls.Add(colorBtn_x);
            groupBox2.Controls.Add(colorBtn_unrecogonized);
            groupBox2.Controls.Add(numPathTrackerStayTimeoutMillis);
            groupBox2.Controls.Add(numPathTrackerInitialValidMove);
            groupBox2.Controls.Add(checkBox1);
            groupBox2.Controls.Add(check_gestBtn_X);
            groupBox2.Controls.Add(checkGestureView_fadeOut);
            groupBox2.Controls.Add(check_gestBtn_Middle);
            groupBox2.Controls.Add(checkGestureViewShowCommandName);
            groupBox2.Controls.Add(checkPathTrackerStayTimeout);
            groupBox2.Controls.Add(check_gestBtn_Right);
            groupBox2.Controls.Add(checkGestureViewShowPath);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label1);
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.Location = new Point(21, 256);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(789, 408);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "参数";
            // 
            // check_preferCursorWindow
            // 
            check_preferCursorWindow.AutoSize = true;
            check_preferCursorWindow.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "PathTrackerPreferCursorWindow", true, DataSourceUpdateMode.OnPropertyChanged));
            check_preferCursorWindow.Location = new Point(450, 171);
            check_preferCursorWindow.Margin = new Padding(6);
            check_preferCursorWindow.Name = "check_preferCursorWindow";
            check_preferCursorWindow.Size = new Size(211, 24);
            check_preferCursorWindow.TabIndex = 18;
            check_preferCursorWindow.Text = "总是作用于指针下方的窗口";
            tip.SetToolTip(check_preferCursorWindow, "使手势总是作用于鼠标指针下方窗口，而不是当前活动程序");
            check_preferCursorWindow.UseVisualStyleBackColor = true;
            // 
            // settingsFormControllerBindingSource
            // 
            settingsFormControllerBindingSource.DataSource = typeof(SettingsFormController);
            // 
            // check_enable8DirGesture
            // 
            check_enable8DirGesture.AutoSize = true;
            check_enable8DirGesture.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureParserEnable8DirGesture", true, DataSourceUpdateMode.OnPropertyChanged));
            check_enable8DirGesture.Location = new Point(450, 126);
            check_enable8DirGesture.Margin = new Padding(6);
            check_enable8DirGesture.Name = "check_enable8DirGesture";
            check_enable8DirGesture.Size = new Size(151, 24);
            check_enable8DirGesture.TabIndex = 17;
            check_enable8DirGesture.Text = "允许使用斜线手势";
            tip.SetToolTip(check_enable8DirGesture, "是否允许使用”↖↙↗↘“手势");
            check_enable8DirGesture.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(440, 48);
            label3.Margin = new Padding(6);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 16;
            label3.Text = "手势键:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            tip.SetToolTip(label3, "允许哪个鼠标按钮触发手势？");
            // 
            // lineLabel2
            // 
            lineLabel2.BackColor = Color.Transparent;
            lineLabel2.CausesValidation = false;
            lineLabel2.ForeColor = Color.Gainsboro;
            lineLabel2.ImeMode = ImeMode.NoControl;
            lineLabel2.IsVertical = true;
            lineLabel2.Location = new Point(400, 45);
            lineLabel2.Margin = new Padding(6);
            lineLabel2.Name = "lineLabel2";
            lineLabel2.Size = new Size(27, 164);
            lineLabel2.TabIndex = 13;
            // 
            // check_disableOnFullscreen
            // 
            check_disableOnFullscreen.AutoSize = true;
            check_disableOnFullscreen.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "PathTrackerDisableInFullScreen", true, DataSourceUpdateMode.OnPropertyChanged));
            check_disableOnFullscreen.ImeMode = ImeMode.NoControl;
            check_disableOnFullscreen.Location = new Point(126, 176);
            check_disableOnFullscreen.Margin = new Padding(6);
            check_disableOnFullscreen.Name = "check_disableOnFullscreen";
            check_disableOnFullscreen.Size = new Size(166, 24);
            check_disableOnFullscreen.TabIndex = 12;
            check_disableOnFullscreen.Text = "全屏时自动禁用手势";
            check_disableOnFullscreen.UseVisualStyleBackColor = true;
            // 
            // num_pathTrackerInitialStayTimeoutMillis
            // 
            num_pathTrackerInitialStayTimeoutMillis.BorderStyle = BorderStyle.FixedSingle;
            num_pathTrackerInitialStayTimeoutMillis.DataBindings.Add(new Binding("Value", settingsFormControllerBindingSource, "PathTrackerInitalStayTimeoutMillis", true, DataSourceUpdateMode.OnPropertyChanged));
            num_pathTrackerInitialStayTimeoutMillis.DataBindings.Add(new Binding("Enabled", settingsFormControllerBindingSource, "PathTrackerInitialStayTimeout", true, DataSourceUpdateMode.OnPropertyChanged));
            num_pathTrackerInitialStayTimeoutMillis.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            num_pathTrackerInitialStayTimeoutMillis.Location = new Point(314, 86);
            num_pathTrackerInitialStayTimeoutMillis.Margin = new Padding(6);
            num_pathTrackerInitialStayTimeoutMillis.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            num_pathTrackerInitialStayTimeoutMillis.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            num_pathTrackerInitialStayTimeoutMillis.Name = "num_pathTrackerInitialStayTimeoutMillis";
            num_pathTrackerInitialStayTimeoutMillis.Size = new Size(75, 27);
            num_pathTrackerInitialStayTimeoutMillis.TabIndex = 11;
            tip.SetToolTip(num_pathTrackerInitialStayTimeoutMillis, "若按下右键后超过此时间未移动，则执行正常右键拖拽操作");
            num_pathTrackerInitialStayTimeoutMillis.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // check_pathTrackerInitialStayTimeout
            // 
            check_pathTrackerInitialStayTimeout.AutoSize = true;
            check_pathTrackerInitialStayTimeout.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "PathTrackerInitialStayTimeout", true, DataSourceUpdateMode.OnPropertyChanged));
            check_pathTrackerInitialStayTimeout.ImeMode = ImeMode.NoControl;
            check_pathTrackerInitialStayTimeout.Location = new Point(126, 86);
            check_pathTrackerInitialStayTimeout.Margin = new Padding(6);
            check_pathTrackerInitialStayTimeout.Name = "check_pathTrackerInitialStayTimeout";
            check_pathTrackerInitialStayTimeout.Size = new Size(135, 24);
            check_pathTrackerInitialStayTimeout.TabIndex = 10;
            check_pathTrackerInitialStayTimeout.Text = "起始超时 (毫秒)";
            tip.SetToolTip(check_pathTrackerInitialStayTimeout, "若按下右键后超过此时间未移动，则执行正常右键拖拽操作");
            check_pathTrackerInitialStayTimeout.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.FlatStyle = FlatStyle.Flat;
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(122, 288);
            label9.Margin = new Padding(6);
            label9.Name = "label9";
            label9.Size = new Size(73, 20);
            label9.TabIndex = 9;
            label9.Text = "轨迹风格:";
            // 
            // colorMiddle
            // 
            colorMiddle.BackColor = Color.White;
            colorMiddle.Color = Color.YellowGreen;
            colorMiddle.DataBindings.Add(new Binding("Color", settingsFormControllerBindingSource, "GestureViewMiddleBtnMainColor", true, DataSourceUpdateMode.OnPropertyChanged));
            colorMiddle.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            colorMiddle.FlatStyle = FlatStyle.Flat;
            colorMiddle.ImeMode = ImeMode.NoControl;
            colorMiddle.Location = new Point(228, 327);
            colorMiddle.Margin = new Padding(6);
            colorMiddle.Name = "colorMiddle";
            colorMiddle.Size = new Size(90, 52);
            colorMiddle.TabIndex = 8;
            colorMiddle.Text = "中键";
            colorMiddle.UseVisualStyleBackColor = false;
            // 
            // colorBtn_recogonized
            // 
            colorBtn_recogonized.BackColor = Color.White;
            colorBtn_recogonized.Color = Color.MediumTurquoise;
            colorBtn_recogonized.DataBindings.Add(new Binding("Color", settingsFormControllerBindingSource, "GestureViewMainPathColor", true, DataSourceUpdateMode.OnPropertyChanged));
            colorBtn_recogonized.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            colorBtn_recogonized.FlatStyle = FlatStyle.Flat;
            colorBtn_recogonized.ImeMode = ImeMode.NoControl;
            colorBtn_recogonized.Location = new Point(126, 327);
            colorBtn_recogonized.Margin = new Padding(6);
            colorBtn_recogonized.Name = "colorBtn_recogonized";
            colorBtn_recogonized.Size = new Size(90, 52);
            colorBtn_recogonized.TabIndex = 8;
            colorBtn_recogonized.Text = "右键";
            tip.SetToolTip(colorBtn_recogonized, "手势被识别时，轨迹的颜色");
            colorBtn_recogonized.UseVisualStyleBackColor = false;
            // 
            // colorBtn_x
            // 
            colorBtn_x.BackColor = Color.White;
            colorBtn_x.Color = Color.FromArgb(255, 128, 0);
            colorBtn_x.DataBindings.Add(new Binding("Color", settingsFormControllerBindingSource, "GestureVieXBtnMainColor", true, DataSourceUpdateMode.OnPropertyChanged));
            colorBtn_x.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            colorBtn_x.FlatStyle = FlatStyle.Flat;
            colorBtn_x.ImeMode = ImeMode.NoControl;
            colorBtn_x.Location = new Point(330, 327);
            colorBtn_x.Margin = new Padding(6);
            colorBtn_x.Name = "colorBtn_x";
            colorBtn_x.Size = new Size(90, 52);
            colorBtn_x.TabIndex = 8;
            colorBtn_x.Text = "X键";
            colorBtn_x.UseVisualStyleBackColor = false;
            // 
            // colorBtn_unrecogonized
            // 
            colorBtn_unrecogonized.BackColor = Color.White;
            colorBtn_unrecogonized.Color = Color.Gray;
            colorBtn_unrecogonized.DataBindings.Add(new Binding("Color", settingsFormControllerBindingSource, "GestureViewAlternativePathColor", true, DataSourceUpdateMode.OnPropertyChanged));
            colorBtn_unrecogonized.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            colorBtn_unrecogonized.FlatStyle = FlatStyle.Flat;
            colorBtn_unrecogonized.ImeMode = ImeMode.NoControl;
            colorBtn_unrecogonized.Location = new Point(441, 327);
            colorBtn_unrecogonized.Margin = new Padding(6);
            colorBtn_unrecogonized.Name = "colorBtn_unrecogonized";
            colorBtn_unrecogonized.Size = new Size(90, 52);
            colorBtn_unrecogonized.TabIndex = 8;
            colorBtn_unrecogonized.Text = "未识别";
            tip.SetToolTip(colorBtn_unrecogonized, "手势未被识别时，轨迹的颜色");
            colorBtn_unrecogonized.UseVisualStyleBackColor = false;
            // 
            // numPathTrackerStayTimeoutMillis
            // 
            numPathTrackerStayTimeoutMillis.BorderStyle = BorderStyle.FixedSingle;
            numPathTrackerStayTimeoutMillis.DataBindings.Add(new Binding("Value", settingsFormControllerBindingSource, "PathTrackerStayTimeoutMillis", true, DataSourceUpdateMode.OnPropertyChanged));
            numPathTrackerStayTimeoutMillis.DataBindings.Add(new Binding("Enabled", settingsFormControllerBindingSource, "PathTrackerStayTimeout", true, DataSourceUpdateMode.OnPropertyChanged));
            numPathTrackerStayTimeoutMillis.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numPathTrackerStayTimeoutMillis.Location = new Point(314, 130);
            numPathTrackerStayTimeoutMillis.Margin = new Padding(6);
            numPathTrackerStayTimeoutMillis.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numPathTrackerStayTimeoutMillis.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numPathTrackerStayTimeoutMillis.Name = "numPathTrackerStayTimeoutMillis";
            numPathTrackerStayTimeoutMillis.Size = new Size(75, 27);
            numPathTrackerStayTimeoutMillis.TabIndex = 7;
            tip.SetToolTip(numPathTrackerStayTimeoutMillis, "若鼠标停止移动超过此时间，已画出的手势将被取消");
            numPathTrackerStayTimeoutMillis.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // numPathTrackerInitialValidMove
            // 
            numPathTrackerInitialValidMove.BorderStyle = BorderStyle.FixedSingle;
            numPathTrackerInitialValidMove.DataBindings.Add(new Binding("Value", settingsFormControllerBindingSource, "PathTrackerInitialValidMove", true, DataSourceUpdateMode.OnPropertyChanged));
            numPathTrackerInitialValidMove.Location = new Point(314, 45);
            numPathTrackerInitialValidMove.Margin = new Padding(6);
            numPathTrackerInitialValidMove.Name = "numPathTrackerInitialValidMove";
            numPathTrackerInitialValidMove.Size = new Size(75, 27);
            numPathTrackerInitialValidMove.TabIndex = 7;
            tip.SetToolTip(numPathTrackerInitialValidMove, "只有移动超过此距离，才开始识别手势");
            numPathTrackerInitialValidMove.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "PathTrackerEnableWinKeyGesturing", true, DataSourceUpdateMode.OnPropertyChanged));
            checkBox1.ImeMode = ImeMode.NoControl;
            checkBox1.Location = new Point(450, 83);
            checkBox1.Margin = new Padding(6);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(262, 24);
            checkBox1.TabIndex = 1;
            checkBox1.Tag = "12";
            checkBox1.Text = "启用Windows键触发 (等价于右键)";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // check_gestBtn_X
            // 
            check_gestBtn_X.AutoSize = true;
            check_gestBtn_X.ImeMode = ImeMode.NoControl;
            check_gestBtn_X.Location = new Point(699, 46);
            check_gestBtn_X.Margin = new Padding(6);
            check_gestBtn_X.Name = "check_gestBtn_X";
            check_gestBtn_X.Size = new Size(56, 24);
            check_gestBtn_X.TabIndex = 1;
            check_gestBtn_X.Tag = "12";
            check_gestBtn_X.Text = "X键";
            check_gestBtn_X.UseVisualStyleBackColor = true;
            check_gestBtn_X.CheckedChanged += check_gestBtns_checkedChanged;
            // 
            // checkGestureView_fadeOut
            // 
            checkGestureView_fadeOut.AutoSize = true;
            checkGestureView_fadeOut.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureViewFadeOut", true, DataSourceUpdateMode.OnPropertyChanged));
            checkGestureView_fadeOut.ImeMode = ImeMode.NoControl;
            checkGestureView_fadeOut.Location = new Point(405, 243);
            checkGestureView_fadeOut.Margin = new Padding(6);
            checkGestureView_fadeOut.Name = "checkGestureView_fadeOut";
            checkGestureView_fadeOut.Size = new Size(106, 24);
            checkGestureView_fadeOut.TabIndex = 1;
            checkGestureView_fadeOut.Text = "执行后淡出";
            tip.SetToolTip(checkGestureView_fadeOut, "手势执行后图形逐渐消失(而非突然消失)");
            checkGestureView_fadeOut.UseVisualStyleBackColor = true;
            // 
            // check_gestBtn_Middle
            // 
            check_gestBtn_Middle.AutoSize = true;
            check_gestBtn_Middle.ImeMode = ImeMode.NoControl;
            check_gestBtn_Middle.Location = new Point(610, 46);
            check_gestBtn_Middle.Margin = new Padding(6);
            check_gestBtn_Middle.Name = "check_gestBtn_Middle";
            check_gestBtn_Middle.Size = new Size(61, 24);
            check_gestBtn_Middle.TabIndex = 1;
            check_gestBtn_Middle.Tag = "2";
            check_gestBtn_Middle.Text = "中键";
            check_gestBtn_Middle.UseVisualStyleBackColor = true;
            check_gestBtn_Middle.CheckedChanged += check_gestBtns_checkedChanged;
            // 
            // checkGestureViewShowCommandName
            // 
            checkGestureViewShowCommandName.AutoSize = true;
            checkGestureViewShowCommandName.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureViewShowCommandName", true, DataSourceUpdateMode.OnPropertyChanged));
            checkGestureViewShowCommandName.ImeMode = ImeMode.NoControl;
            checkGestureViewShowCommandName.Location = new Point(246, 243);
            checkGestureViewShowCommandName.Margin = new Padding(6);
            checkGestureViewShowCommandName.Name = "checkGestureViewShowCommandName";
            checkGestureViewShowCommandName.Size = new Size(91, 24);
            checkGestureViewShowCommandName.TabIndex = 1;
            checkGestureViewShowCommandName.Text = "手势名称";
            checkGestureViewShowCommandName.UseVisualStyleBackColor = true;
            // 
            // checkPathTrackerStayTimeout
            // 
            checkPathTrackerStayTimeout.AutoSize = true;
            checkPathTrackerStayTimeout.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "PathTrackerStayTimeout", true, DataSourceUpdateMode.OnPropertyChanged));
            checkPathTrackerStayTimeout.ImeMode = ImeMode.NoControl;
            checkPathTrackerStayTimeout.Location = new Point(126, 130);
            checkPathTrackerStayTimeout.Margin = new Padding(6);
            checkPathTrackerStayTimeout.Name = "checkPathTrackerStayTimeout";
            checkPathTrackerStayTimeout.Size = new Size(135, 24);
            checkPathTrackerStayTimeout.TabIndex = 0;
            checkPathTrackerStayTimeout.Text = "停留超时 (毫秒)";
            tip.SetToolTip(checkPathTrackerStayTimeout, "若鼠标停止移动超过此时间，已画出的手势将被取消");
            checkPathTrackerStayTimeout.UseVisualStyleBackColor = true;
            // 
            // check_gestBtn_Right
            // 
            check_gestBtn_Right.AutoSize = true;
            check_gestBtn_Right.ImeMode = ImeMode.NoControl;
            check_gestBtn_Right.Location = new Point(522, 46);
            check_gestBtn_Right.Margin = new Padding(6);
            check_gestBtn_Right.Name = "check_gestBtn_Right";
            check_gestBtn_Right.Size = new Size(61, 24);
            check_gestBtn_Right.TabIndex = 0;
            check_gestBtn_Right.Tag = "1";
            check_gestBtn_Right.Text = "右键";
            check_gestBtn_Right.UseVisualStyleBackColor = true;
            check_gestBtn_Right.CheckedChanged += check_gestBtns_checkedChanged;
            // 
            // checkGestureViewShowPath
            // 
            checkGestureViewShowPath.AutoSize = true;
            checkGestureViewShowPath.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureViewShowPath", true, DataSourceUpdateMode.OnPropertyChanged));
            checkGestureViewShowPath.ImeMode = ImeMode.NoControl;
            checkGestureViewShowPath.Location = new Point(126, 243);
            checkGestureViewShowPath.Margin = new Padding(6);
            checkGestureViewShowPath.Name = "checkGestureViewShowPath";
            checkGestureViewShowPath.Size = new Size(61, 24);
            checkGestureViewShowPath.TabIndex = 0;
            checkGestureViewShowPath.Text = "轨迹";
            checkGestureViewShowPath.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(122, 48);
            label6.Margin = new Padding(6);
            label6.Name = "label6";
            label6.Size = new Size(139, 20);
            label6.TabIndex = 2;
            label6.Text = "起始移动距离(像素)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            tip.SetToolTip(label6, "只有移动超过此距离，才开始识别手势");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.FlatStyle = FlatStyle.Flat;
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(39, 48);
            label5.Margin = new Padding(6);
            label5.Name = "label5";
            label5.Size = new Size(58, 20);
            label5.TabIndex = 2;
            label5.Text = "有效性:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(39, 246);
            label1.Margin = new Padding(6);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 2;
            label1.Text = "显   示:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(shortcutRec_pause);
            groupBox1.Controls.Add(flowLayoutPanel3);
            groupBox1.Controls.Add(check_autoStart);
            groupBox1.Controls.Add(btn_checkUpdateNow);
            groupBox1.Controls.Add(check_autoCheckUpdate);
            groupBox1.Controls.Add(lb_pause_shortcut);
            groupBox1.Controls.Add(label14);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Location = new Point(21, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(789, 190);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "通用";
            // 
            // shortcutRec_pause
            // 
            shortcutRec_pause.Location = new Point(214, 130);
            shortcutRec_pause.Margin = new Padding(6);
            shortcutRec_pause.Name = "shortcutRec_pause";
            shortcutRec_pause.Size = new Size(132, 34);
            shortcutRec_pause.TabIndex = 6;
            shortcutRec_pause.Text = "录入快捷键";
            shortcutRec_pause.UseVisualStyleBackColor = true;
            shortcutRec_pause.EndRecord += shortcutRec_pause_EndRecord;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(label4);
            flowLayoutPanel3.Controls.Add(lb_Version);
            flowLayoutPanel3.Location = new Point(348, 81);
            flowLayoutPanel3.Margin = new Padding(6);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(417, 34);
            flowLayoutPanel3.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.FlatStyle = FlatStyle.Flat;
            label4.ForeColor = Color.Gray;
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(3, 9);
            label4.Margin = new Padding(3, 9, 3, 3);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 2;
            label4.Text = "当前版本:";
            // 
            // lb_Version
            // 
            lb_Version.AutoSize = true;
            lb_Version.FlatStyle = FlatStyle.Flat;
            lb_Version.ForeColor = Color.Gray;
            lb_Version.ImeMode = ImeMode.NoControl;
            lb_Version.Location = new Point(82, 9);
            lb_Version.Margin = new Padding(3, 9, 3, 3);
            lb_Version.Name = "lb_Version";
            lb_Version.Size = new Size(62, 20);
            lb_Version.TabIndex = 3;
            lb_Version.Text = "version";
            // 
            // check_autoStart
            // 
            check_autoStart.AutoSize = true;
            check_autoStart.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "AutoStart", true, DataSourceUpdateMode.OnPropertyChanged));
            check_autoStart.ImeMode = ImeMode.NoControl;
            check_autoStart.Location = new Point(45, 45);
            check_autoStart.Margin = new Padding(6);
            check_autoStart.Name = "check_autoStart";
            check_autoStart.Size = new Size(121, 24);
            check_autoStart.TabIndex = 0;
            check_autoStart.Text = "开机自动运行";
            check_autoStart.UseVisualStyleBackColor = true;
            // 
            // btn_checkUpdateNow
            // 
            btn_checkUpdateNow.BackColor = SystemColors.Control;
            btn_checkUpdateNow.FlatStyle = FlatStyle.System;
            btn_checkUpdateNow.ImeMode = ImeMode.NoControl;
            btn_checkUpdateNow.Location = new Point(214, 86);
            btn_checkUpdateNow.Margin = new Padding(6);
            btn_checkUpdateNow.Name = "btn_checkUpdateNow";
            btn_checkUpdateNow.Size = new Size(111, 34);
            btn_checkUpdateNow.TabIndex = 2;
            btn_checkUpdateNow.Text = "立即检查";
            btn_checkUpdateNow.UseVisualStyleBackColor = false;
            btn_checkUpdateNow.Click += btn_checkUpdateNow_Click;
            // 
            // check_autoCheckUpdate
            // 
            check_autoCheckUpdate.AutoSize = true;
            check_autoCheckUpdate.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "AutoCheckForUpdate", true, DataSourceUpdateMode.OnPropertyChanged));
            check_autoCheckUpdate.ImeMode = ImeMode.NoControl;
            check_autoCheckUpdate.Location = new Point(45, 90);
            check_autoCheckUpdate.Margin = new Padding(6);
            check_autoCheckUpdate.Name = "check_autoCheckUpdate";
            check_autoCheckUpdate.Size = new Size(121, 24);
            check_autoCheckUpdate.TabIndex = 1;
            check_autoCheckUpdate.Text = "自动检查更新";
            check_autoCheckUpdate.UseVisualStyleBackColor = true;
            // 
            // lb_pause_shortcut
            // 
            lb_pause_shortcut.AutoSize = true;
            lb_pause_shortcut.ImeMode = ImeMode.NoControl;
            lb_pause_shortcut.Location = new Point(358, 135);
            lb_pause_shortcut.Margin = new Padding(6);
            lb_pause_shortcut.Name = "lb_pause_shortcut";
            lb_pause_shortcut.Size = new Size(24, 20);
            lb_pause_shortcut.TabIndex = 2;
            lb_pause_shortcut.Text = "无";
            lb_pause_shortcut.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ImeMode = ImeMode.NoControl;
            label14.Location = new Point(40, 135);
            label14.Margin = new Padding(6);
            label14.Name = "label14";
            label14.Size = new Size(124, 20);
            label14.TabIndex = 2;
            label14.Text = "暂停/继续快捷键:";
            label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(flowLayoutPanel2);
            tabPage2.Controls.Add(btnEditApp);
            tabPage2.Controls.Add(btnAppRemove);
            tabPage2.Controls.Add(btnAddApp);
            tabPage2.Controls.Add(listApps);
            tabPage2.Location = new Point(4, 32);
            tabPage2.Margin = new Padding(0);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(829, 720);
            tabPage2.TabIndex = 1;
            tabPage2.Tag = "gestures";
            tabPage2.Text = "手 势";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(panel1);
            flowLayoutPanel2.Controls.Add(listGestureIntents);
            flowLayoutPanel2.Controls.Add(panel_intentListOperations);
            flowLayoutPanel2.Controls.Add(group_Command);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(285, 27);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(528, 662);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(checkInheritGlobal);
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(502, 30);
            panel1.TabIndex = 12;
            // 
            // checkInheritGlobal
            // 
            checkInheritGlobal.AutoSize = true;
            checkInheritGlobal.ImeMode = ImeMode.NoControl;
            checkInheritGlobal.Location = new Point(381, 3);
            checkInheritGlobal.Margin = new Padding(0);
            checkInheritGlobal.Name = "checkInheritGlobal";
            checkInheritGlobal.Size = new Size(121, 24);
            checkInheritGlobal.TabIndex = 3;
            checkInheritGlobal.Text = "继承全局手势";
            checkInheritGlobal.UseVisualStyleBackColor = true;
            checkInheritGlobal.CheckedChanged += checkInheritGlobal_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Controls.Add(check_gesturingDisabled);
            flowLayoutPanel1.Controls.Add(pictureSelectedApp);
            flowLayoutPanel1.Controls.Add(label15);
            flowLayoutPanel1.Controls.Add(label7);
            flowLayoutPanel1.ForeColor = SystemColors.ControlDark;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(296, 30);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // check_gesturingDisabled
            // 
            check_gesturingDisabled.AutoSize = true;
            check_gesturingDisabled.ForeColor = Color.Black;
            check_gesturingDisabled.ImeMode = ImeMode.NoControl;
            check_gesturingDisabled.Location = new Point(15, 3);
            check_gesturingDisabled.Margin = new Padding(15, 3, 0, 3);
            check_gesturingDisabled.Name = "check_gesturingDisabled";
            check_gesturingDisabled.Size = new Size(76, 24);
            check_gesturingDisabled.TabIndex = 5;
            check_gesturingDisabled.Text = "不要在";
            tip.SetToolTip(check_gesturingDisabled, "在该程序上禁用手势（等同于双击应用程序条目）");
            check_gesturingDisabled.UseVisualStyleBackColor = true;
            check_gesturingDisabled.CheckedChanged += check_gesturingEnabled_CheckedChanged;
            // 
            // pictureSelectedApp
            // 
            pictureSelectedApp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pictureSelectedApp.ImeMode = ImeMode.NoControl;
            pictureSelectedApp.Location = new Point(91, 0);
            pictureSelectedApp.Margin = new Padding(0, 0, 0, 3);
            pictureSelectedApp.Name = "pictureSelectedApp";
            pictureSelectedApp.Size = new Size(27, 27);
            pictureSelectedApp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureSelectedApp.TabIndex = 2;
            pictureSelectedApp.TabStop = false;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.White;
            label15.ForeColor = Color.Black;
            label15.ImeMode = ImeMode.NoControl;
            label15.Location = new Point(118, 3);
            label15.Margin = new Padding(0, 3, 0, 3);
            label15.Name = "label15";
            label15.Size = new Size(114, 20);
            label15.TabIndex = 3;
            label15.Text = "上使用任何手势";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Microsoft YaHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label7.ForeColor = SystemColors.ControlText;
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(232, 3);
            label7.Margin = new Padding(0, 3, 0, 3);
            label7.Name = "label7";
            label7.Size = new Size(64, 20);
            label7.TabIndex = 3;
            label7.Text = "(黑名单)";
            // 
            // listGestureIntents
            // 
            listGestureIntents.AllowDrop = true;
            listGestureIntents.AllowItemDrag = true;
            listGestureIntents.BorderStyle = BorderStyle.FixedSingle;
            listGestureIntents.Columns.AddRange(new ColumnHeader[] { colGestureName, colGestureDirs, operation });
            listGestureIntents.Font = new Font("Microsoft YaHei", 8.25F);
            listGestureIntents.FullRowSelect = true;
            listGestureIntents.GridLines = true;
            listGestureIntents.InsertionLineColor = Color.DeepSkyBlue;
            listGestureIntents.LabelEdit = true;
            listGestureIntents.Location = new Point(15, 33);
            listGestureIntents.Margin = new Padding(15, 3, 3, 3);
            listGestureIntents.MultiSelect = false;
            listGestureIntents.Name = "listGestureIntents";
            listGestureIntents.Size = new Size(509, 245);
            listGestureIntents.SmallImageList = dummyImgLstForLstViewHeightFix;
            listGestureIntents.TabIndex = 1;
            listGestureIntents.TileSize = new Size(255, 84);
            listGestureIntents.UseCompatibleStateImageBehavior = false;
            listGestureIntents.View = System.Windows.Forms.View.Details;
            listGestureIntents.AfterLabelEdit += listGestureIntents_AfterLabelEdit;
            listGestureIntents.ItemSelectionChanged += listGestureIntents_ItemSelectionChanged;
            listGestureIntents.DoubleClick += listGestureIntents_DoubleClick;
            listGestureIntents.MouseEnter += listGestureIntents_MouseEnter;
            listGestureIntents.MouseHover += listGestureIntents_MouseHover;
            // 
            // colGestureName
            // 
            colGestureName.Text = "名称";
            colGestureName.Width = 10;
            // 
            // colGestureDirs
            // 
            colGestureDirs.Text = "助记符";
            colGestureDirs.Width = 10;
            // 
            // operation
            // 
            operation.Text = "操作";
            // 
            // dummyImgLstForLstViewHeightFix
            // 
            dummyImgLstForLstViewHeightFix.ColorDepth = ColorDepth.Depth4Bit;
            dummyImgLstForLstViewHeightFix.ImageSize = new Size(1, 24);
            dummyImgLstForLstViewHeightFix.TransparentColor = Color.Transparent;
            // 
            // panel_intentListOperations
            // 
            panel_intentListOperations.Controls.Add(btn_RemoveGesture);
            panel_intentListOperations.Controls.Add(btn_modifyGesture);
            panel_intentListOperations.Controls.Add(btnAddGesture);
            panel_intentListOperations.Location = new Point(15, 284);
            panel_intentListOperations.Margin = new Padding(15, 3, 0, 0);
            panel_intentListOperations.Name = "panel_intentListOperations";
            panel_intentListOperations.Size = new Size(510, 33);
            panel_intentListOperations.TabIndex = 9;
            // 
            // btn_RemoveGesture
            // 
            btn_RemoveGesture.Enabled = false;
            btn_RemoveGesture.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btn_RemoveGesture.ForeColor = Color.FromArgb(64, 64, 64);
            btn_RemoveGesture.Image = Resources.remove;
            btn_RemoveGesture.ImeMode = ImeMode.NoControl;
            btn_RemoveGesture.Location = new Point(42, 0);
            btn_RemoveGesture.Margin = new Padding(0);
            btn_RemoveGesture.Name = "btn_RemoveGesture";
            btn_RemoveGesture.Size = new Size(45, 30);
            btn_RemoveGesture.TabIndex = 8;
            tip.SetToolTip(btn_RemoveGesture, "删除选中的项目");
            btn_RemoveGesture.UseVisualStyleBackColor = true;
            btn_RemoveGesture.Click += btnRemoveGesture_Click;
            // 
            // btn_modifyGesture
            // 
            btn_modifyGesture.Enabled = false;
            btn_modifyGesture.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btn_modifyGesture.ForeColor = Color.FromArgb(64, 64, 64);
            btn_modifyGesture.Image = Resources.Edit;
            btn_modifyGesture.ImeMode = ImeMode.NoControl;
            btn_modifyGesture.Location = new Point(464, 0);
            btn_modifyGesture.Margin = new Padding(0, 3, 0, 3);
            btn_modifyGesture.Name = "btn_modifyGesture";
            btn_modifyGesture.Size = new Size(45, 30);
            btn_modifyGesture.TabIndex = 8;
            btn_modifyGesture.UseVisualStyleBackColor = true;
            btn_modifyGesture.Click += btn_modifyGesture_Click;
            // 
            // btnAddGesture
            // 
            btnAddGesture.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btnAddGesture.ForeColor = Color.FromArgb(64, 64, 64);
            btnAddGesture.Image = Resources.add;
            btnAddGesture.ImeMode = ImeMode.NoControl;
            btnAddGesture.Location = new Point(0, 0);
            btnAddGesture.Margin = new Padding(0);
            btnAddGesture.Name = "btnAddGesture";
            btnAddGesture.Size = new Size(45, 30);
            btnAddGesture.TabIndex = 8;
            tip.SetToolTip(btnAddGesture, "添加手势");
            btnAddGesture.UseVisualStyleBackColor = true;
            btnAddGesture.Click += btnAddGesture_Click;
            // 
            // group_Command
            // 
            group_Command.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            group_Command.Controls.Add(flowLayoutPanel6);
            group_Command.Enabled = false;
            group_Command.Location = new Point(15, 332);
            group_Command.Margin = new Padding(15, 15, 3, 3);
            group_Command.Name = "group_Command";
            group_Command.Size = new Size(510, 327);
            group_Command.TabIndex = 10;
            group_Command.TabStop = false;
            group_Command.Text = "手势参数";
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(panel3);
            flowLayoutPanel6.Controls.Add(check_executeOnMouseWheeling);
            flowLayoutPanel6.Controls.Add(lineLabel1);
            flowLayoutPanel6.Controls.Add(panel_commandView);
            flowLayoutPanel6.Location = new Point(6, 27);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(498, 291);
            flowLayoutPanel6.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(label8);
            panel3.Controls.Add(combo_CommandTypes);
            flowLayoutPanel6.SetFlowBreak(panel3, true);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(498, 51);
            panel3.TabIndex = 2;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.FlatStyle = FlatStyle.Flat;
            label8.ImeMode = ImeMode.NoControl;
            label8.Location = new Point(3, 10);
            label8.Name = "label8";
            label8.Size = new Size(69, 20);
            label8.TabIndex = 1;
            label8.Text = "执行操作";
            // 
            // combo_CommandTypes
            // 
            combo_CommandTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_CommandTypes.FlatStyle = FlatStyle.System;
            combo_CommandTypes.Font = new Font("Microsoft YaHei", 9F);
            combo_CommandTypes.FormattingEnabled = true;
            combo_CommandTypes.ItemHeight = 20;
            combo_CommandTypes.Location = new Point(99, 6);
            combo_CommandTypes.Name = "combo_CommandTypes";
            combo_CommandTypes.Size = new Size(388, 28);
            combo_CommandTypes.TabIndex = 0;
            tip.SetToolTip(combo_CommandTypes, "手势触发后要执行的操作");
            combo_CommandTypes.SelectedIndexChanged += combo_CommandTypes_SelectedIndexChanged;
            // 
            // check_executeOnMouseWheeling
            // 
            check_executeOnMouseWheeling.AutoSize = true;
            check_executeOnMouseWheeling.Dock = DockStyle.Top;
            check_executeOnMouseWheeling.FlatStyle = FlatStyle.System;
            flowLayoutPanel6.SetFlowBreak(check_executeOnMouseWheeling, true);
            check_executeOnMouseWheeling.ImeMode = ImeMode.NoControl;
            check_executeOnMouseWheeling.Location = new Point(3, 54);
            check_executeOnMouseWheeling.Margin = new Padding(3, 3, 3, 0);
            check_executeOnMouseWheeling.Name = "check_executeOnMouseWheeling";
            check_executeOnMouseWheeling.Size = new Size(190, 25);
            check_executeOnMouseWheeling.TabIndex = 3;
            check_executeOnMouseWheeling.Text = "修饰键触发时立即执行";
            check_executeOnMouseWheeling.UseVisualStyleBackColor = true;
            check_executeOnMouseWheeling.Visible = false;
            check_executeOnMouseWheeling.CheckedChanged += check_executeOnMouseWheeling_CheckedChanged;
            // 
            // lineLabel1
            // 
            lineLabel1.Dock = DockStyle.Top;
            lineLabel1.ForeColor = Color.Gainsboro;
            lineLabel1.ImeMode = ImeMode.NoControl;
            lineLabel1.IsVertical = false;
            lineLabel1.Location = new Point(3, 79);
            lineLabel1.Name = "lineLabel1";
            lineLabel1.Size = new Size(492, 23);
            lineLabel1.TabIndex = 3;
            // 
            // panel_commandView
            // 
            panel_commandView.AutoScroll = true;
            panel_commandView.BackColor = Color.Transparent;
            panel_commandView.Dock = DockStyle.Top;
            panel_commandView.Location = new Point(0, 102);
            panel_commandView.Margin = new Padding(0);
            panel_commandView.Name = "panel_commandView";
            panel_commandView.Size = new Size(498, 180);
            panel_commandView.TabIndex = 2;
            tip.SetToolTip(panel_commandView, "操作的额外参数");
            // 
            // btnEditApp
            // 
            btnEditApp.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btnEditApp.ForeColor = Color.FromArgb(64, 64, 64);
            btnEditApp.Image = Resources.Edit;
            btnEditApp.ImeMode = ImeMode.NoControl;
            btnEditApp.Location = new Point(228, 664);
            btnEditApp.Margin = new Padding(0, 3, 0, 3);
            btnEditApp.Name = "btnEditApp";
            btnEditApp.Size = new Size(45, 30);
            btnEditApp.TabIndex = 8;
            tip.SetToolTip(btnEditApp, "修改选中项目的名称或路径");
            btnEditApp.UseVisualStyleBackColor = true;
            btnEditApp.Click += btnEditApp_Click;
            // 
            // btnAppRemove
            // 
            btnAppRemove.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btnAppRemove.ForeColor = Color.FromArgb(64, 64, 64);
            btnAppRemove.Image = Resources.remove;
            btnAppRemove.ImeMode = ImeMode.NoControl;
            btnAppRemove.Location = new Point(63, 664);
            btnAppRemove.Margin = new Padding(0, 3, 0, 3);
            btnAppRemove.Name = "btnAppRemove";
            btnAppRemove.Size = new Size(45, 30);
            btnAppRemove.TabIndex = 8;
            tip.SetToolTip(btnAppRemove, "删除选中的项目");
            btnAppRemove.UseVisualStyleBackColor = true;
            btnAppRemove.Click += btnAppRemove_Click;
            // 
            // btnAddApp
            // 
            btnAddApp.Font = new Font("Verdana", 10.125F, FontStyle.Bold);
            btnAddApp.ForeColor = Color.FromArgb(64, 64, 64);
            btnAddApp.Image = Resources.add;
            btnAddApp.ImeMode = ImeMode.NoControl;
            btnAddApp.Location = new Point(21, 664);
            btnAddApp.Margin = new Padding(0, 3, 0, 3);
            btnAddApp.Name = "btnAddApp";
            btnAddApp.Size = new Size(45, 30);
            btnAddApp.TabIndex = 8;
            tip.SetToolTip(btnAddApp, "添加应用程序");
            btnAddApp.UseVisualStyleBackColor = true;
            btnAddApp.Click += btnAddApp_Click;
            // 
            // listApps
            // 
            listApps.Alignment = ListViewAlignment.Left;
            listApps.AllowDrop = true;
            listApps.AllowItemDrag = true;
            listApps.BorderStyle = BorderStyle.FixedSingle;
            listApps.Columns.AddRange(new ColumnHeader[] { colListAppDummy });
            listApps.Font = new Font("Microsoft YaHei", 9F);
            listApps.FullRowSelect = true;
            listApps.GridLines = true;
            listApps.HeaderStyle = ColumnHeaderStyle.None;
            listApps.InsertionLineColor = Color.DeepSkyBlue;
            listApps.LabelWrap = false;
            listApps.Location = new Point(21, 30);
            listApps.MultiSelect = false;
            listApps.Name = "listApps";
            listApps.Size = new Size(251, 629);
            listApps.SmallImageList = imglistAppIcons;
            listApps.TabIndex = 0;
            listApps.TileSize = new Size(160, 42);
            listApps.UseCompatibleStateImageBehavior = false;
            listApps.View = System.Windows.Forms.View.Details;
            listApps.ItemDragDrop += listApps_ItemDragDrop;
            listApps.ItemDragging += listApps_ItemDragging;
            listApps.ItemSelectionChanged += listApps_ItemSelectionChanged;
            listApps.DragOver += listApps_DragOver;
            listApps.DoubleClick += listApps_DoubleClick;
            // 
            // colListAppDummy
            // 
            colListAppDummy.Width = 64;
            // 
            // imglistAppIcons
            // 
            imglistAppIcons.ColorDepth = ColorDepth.Depth32Bit;
            imglistAppIcons.ImageSize = new Size(32, 32);
            imglistAppIcons.TransparentColor = Color.Transparent;
            // 
            // tab_hotCorners
            // 
            tab_hotCorners.Controls.Add(panel_hotcornerSettings);
            tab_hotCorners.Controls.Add(check_enableRubEdge);
            tab_hotCorners.Controls.Add(check_enableHotCorners);
            tab_hotCorners.Location = new Point(4, 32);
            tab_hotCorners.Name = "tab_hotCorners";
            tab_hotCorners.Size = new Size(829, 720);
            tab_hotCorners.TabIndex = 3;
            tab_hotCorners.Tag = "corners";
            tab_hotCorners.Text = "触发角 & 摩擦边";
            tab_hotCorners.UseVisualStyleBackColor = true;
            // 
            // panel_hotcornerSettings
            // 
            panel_hotcornerSettings.Controls.Add(radio_edge_0);
            panel_hotcornerSettings.Controls.Add(radio_edge_2);
            panel_hotcornerSettings.Controls.Add(radio_edge_1);
            panel_hotcornerSettings.Controls.Add(radio_edge_3);
            panel_hotcornerSettings.Controls.Add(radio_corner_1);
            panel_hotcornerSettings.Controls.Add(label11);
            panel_hotcornerSettings.Controls.Add(panel2);
            panel_hotcornerSettings.Controls.Add(combo_hotcornerCmdTypes);
            panel_hotcornerSettings.Controls.Add(panel_cornorCmdView);
            panel_hotcornerSettings.Controls.Add(radio_corner_0);
            panel_hotcornerSettings.Controls.Add(radio_corner_2);
            panel_hotcornerSettings.Controls.Add(radio_corner_3);
            panel_hotcornerSettings.Location = new Point(21, 69);
            panel_hotcornerSettings.Name = "panel_hotcornerSettings";
            panel_hotcornerSettings.Size = new Size(783, 627);
            panel_hotcornerSettings.TabIndex = 10;
            // 
            // radio_edge_0
            // 
            radio_edge_0.Appearance = Appearance.Button;
            radio_edge_0.BackColor = Color.WhiteSmoke;
            radio_edge_0.FlatAppearance.BorderColor = Color.LightGray;
            radio_edge_0.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_edge_0.FlatStyle = FlatStyle.Flat;
            radio_edge_0.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_edge_0.Location = new Point(128, 144);
            radio_edge_0.Name = "radio_edge_0";
            radio_edge_0.Size = new Size(156, 39);
            radio_edge_0.TabIndex = 7;
            radio_edge_0.TabStop = true;
            radio_edge_0.Tag = "4";
            radio_edge_0.Text = "?";
            radio_edge_0.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_edge_0, "屏幕左边缘");
            radio_edge_0.UseVisualStyleBackColor = false;
            radio_edge_0.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_edge_2
            // 
            radio_edge_2.Appearance = Appearance.Button;
            radio_edge_2.BackColor = Color.WhiteSmoke;
            radio_edge_2.FlatAppearance.BorderColor = Color.LightGray;
            radio_edge_2.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_edge_2.FlatStyle = FlatStyle.Flat;
            radio_edge_2.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_edge_2.Location = new Point(501, 144);
            radio_edge_2.Name = "radio_edge_2";
            radio_edge_2.Size = new Size(156, 39);
            radio_edge_2.TabIndex = 7;
            radio_edge_2.TabStop = true;
            radio_edge_2.Tag = "6";
            radio_edge_2.Text = "?";
            radio_edge_2.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_edge_2, "屏幕右边缘");
            radio_edge_2.UseVisualStyleBackColor = false;
            radio_edge_2.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_edge_1
            // 
            radio_edge_1.Appearance = Appearance.Button;
            radio_edge_1.BackColor = Color.WhiteSmoke;
            radio_edge_1.FlatAppearance.BorderColor = Color.LightGray;
            radio_edge_1.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_edge_1.FlatStyle = FlatStyle.Flat;
            radio_edge_1.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_edge_1.Location = new Point(318, 16);
            radio_edge_1.Name = "radio_edge_1";
            radio_edge_1.Size = new Size(156, 39);
            radio_edge_1.TabIndex = 7;
            radio_edge_1.TabStop = true;
            radio_edge_1.Tag = "5";
            radio_edge_1.Text = "?";
            radio_edge_1.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_edge_1, "屏幕上边缘");
            radio_edge_1.UseVisualStyleBackColor = false;
            radio_edge_1.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_edge_3
            // 
            radio_edge_3.Appearance = Appearance.Button;
            radio_edge_3.BackColor = Color.WhiteSmoke;
            radio_edge_3.FlatAppearance.BorderColor = Color.LightGray;
            radio_edge_3.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_edge_3.FlatStyle = FlatStyle.Flat;
            radio_edge_3.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_edge_3.Location = new Point(318, 270);
            radio_edge_3.Name = "radio_edge_3";
            radio_edge_3.Size = new Size(156, 39);
            radio_edge_3.TabIndex = 7;
            radio_edge_3.TabStop = true;
            radio_edge_3.Tag = "7";
            radio_edge_3.Text = "?";
            radio_edge_3.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_edge_3, "屏幕下边缘");
            radio_edge_3.UseVisualStyleBackColor = false;
            radio_edge_3.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_corner_1
            // 
            radio_corner_1.Appearance = Appearance.Button;
            radio_corner_1.BackColor = Color.WhiteSmoke;
            radio_corner_1.FlatAppearance.BorderColor = Color.LightGray;
            radio_corner_1.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_corner_1.FlatStyle = FlatStyle.Flat;
            radio_corner_1.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_corner_1.Location = new Point(44, 36);
            radio_corner_1.Name = "radio_corner_1";
            radio_corner_1.Size = new Size(156, 39);
            radio_corner_1.TabIndex = 7;
            radio_corner_1.TabStop = true;
            radio_corner_1.Tag = "1";
            radio_corner_1.Text = "?";
            radio_corner_1.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_corner_1, "屏幕左上角");
            radio_corner_1.UseVisualStyleBackColor = false;
            radio_corner_1.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.FlatStyle = FlatStyle.Flat;
            label11.Location = new Point(100, 358);
            label11.Name = "label11";
            label11.Size = new Size(69, 20);
            label11.TabIndex = 9;
            label11.Text = "执行操作";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.BackColor = Color.AliceBlue;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label13);
            panel2.ForeColor = Color.AliceBlue;
            panel2.Location = new Point(206, 36);
            panel2.Name = "panel2";
            panel2.Size = new Size(374, 252);
            panel2.TabIndex = 1;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label13.ForeColor = Color.Silver;
            label13.Location = new Point(150, 106);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(69, 31);
            label13.TabIndex = 0;
            label13.Text = "屏 幕";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // combo_hotcornerCmdTypes
            // 
            combo_hotcornerCmdTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_hotcornerCmdTypes.FlatStyle = FlatStyle.System;
            combo_hotcornerCmdTypes.FormattingEnabled = true;
            combo_hotcornerCmdTypes.Location = new Point(190, 354);
            combo_hotcornerCmdTypes.Name = "combo_hotcornerCmdTypes";
            combo_hotcornerCmdTypes.Size = new Size(181, 28);
            combo_hotcornerCmdTypes.TabIndex = 8;
            combo_hotcornerCmdTypes.SelectedIndexChanged += combo_hotcornerCmdTypes_SelectedIndexChanged;
            combo_hotcornerCmdTypes.SelectedValueChanged += combo_hotcornerCmdTypes_SelectedValueChanged;
            // 
            // panel_cornorCmdView
            // 
            panel_cornorCmdView.BackColor = Color.Transparent;
            panel_cornorCmdView.BorderStyle = BorderStyle.FixedSingle;
            panel_cornorCmdView.Location = new Point(105, 400);
            panel_cornorCmdView.Name = "panel_cornorCmdView";
            panel_cornorCmdView.Size = new Size(587, 210);
            panel_cornorCmdView.TabIndex = 6;
            // 
            // radio_corner_0
            // 
            radio_corner_0.Appearance = Appearance.Button;
            radio_corner_0.BackColor = Color.WhiteSmoke;
            radio_corner_0.FlatAppearance.BorderColor = Color.LightGray;
            radio_corner_0.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_corner_0.FlatStyle = FlatStyle.Flat;
            radio_corner_0.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_corner_0.Location = new Point(44, 250);
            radio_corner_0.Name = "radio_corner_0";
            radio_corner_0.Size = new Size(156, 39);
            radio_corner_0.TabIndex = 7;
            radio_corner_0.TabStop = true;
            radio_corner_0.Tag = "0";
            radio_corner_0.Text = "?";
            radio_corner_0.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_corner_0, "屏幕左下角");
            radio_corner_0.UseVisualStyleBackColor = false;
            radio_corner_0.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_corner_2
            // 
            radio_corner_2.Appearance = Appearance.Button;
            radio_corner_2.BackColor = Color.WhiteSmoke;
            radio_corner_2.FlatAppearance.BorderColor = Color.LightGray;
            radio_corner_2.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_corner_2.FlatStyle = FlatStyle.Flat;
            radio_corner_2.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_corner_2.Location = new Point(586, 36);
            radio_corner_2.Name = "radio_corner_2";
            radio_corner_2.Size = new Size(156, 39);
            radio_corner_2.TabIndex = 7;
            radio_corner_2.TabStop = true;
            radio_corner_2.Tag = "2";
            radio_corner_2.Text = "?";
            radio_corner_2.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_corner_2, "屏幕右上角");
            radio_corner_2.UseVisualStyleBackColor = false;
            radio_corner_2.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // radio_corner_3
            // 
            radio_corner_3.Appearance = Appearance.Button;
            radio_corner_3.BackColor = Color.WhiteSmoke;
            radio_corner_3.FlatAppearance.BorderColor = Color.LightGray;
            radio_corner_3.FlatAppearance.CheckedBackColor = Color.PaleTurquoise;
            radio_corner_3.FlatStyle = FlatStyle.Flat;
            radio_corner_3.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radio_corner_3.Location = new Point(586, 250);
            radio_corner_3.Name = "radio_corner_3";
            radio_corner_3.Size = new Size(156, 39);
            radio_corner_3.TabIndex = 7;
            radio_corner_3.TabStop = true;
            radio_corner_3.Tag = "3";
            radio_corner_3.Text = "?";
            radio_corner_3.TextAlign = ContentAlignment.MiddleCenter;
            tip.SetToolTip(radio_corner_3, "屏幕右下角");
            radio_corner_3.UseVisualStyleBackColor = false;
            radio_corner_3.CheckedChanged += radio_corner_1_CheckedChanged;
            // 
            // check_enableRubEdge
            // 
            check_enableRubEdge.AutoSize = true;
            check_enableRubEdge.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureParserEnableRubEdges", true, DataSourceUpdateMode.OnPropertyChanged));
            check_enableRubEdge.FlatStyle = FlatStyle.System;
            check_enableRubEdge.Location = new Point(166, 30);
            check_enableRubEdge.Name = "check_enableRubEdge";
            check_enableRubEdge.Size = new Size(115, 25);
            check_enableRubEdge.TabIndex = 0;
            check_enableRubEdge.Text = "启用摩擦边";
            check_enableRubEdge.UseVisualStyleBackColor = true;
            // 
            // check_enableHotCorners
            // 
            check_enableHotCorners.AutoSize = true;
            check_enableHotCorners.DataBindings.Add(new Binding("Checked", settingsFormControllerBindingSource, "GestureParserEnableHotCorners", true, DataSourceUpdateMode.OnPropertyChanged));
            check_enableHotCorners.FlatStyle = FlatStyle.System;
            check_enableHotCorners.Location = new Point(21, 30);
            check_enableHotCorners.Name = "check_enableHotCorners";
            check_enableHotCorners.Size = new Size(115, 25);
            check_enableHotCorners.TabIndex = 0;
            check_enableHotCorners.Text = "启用触发角";
            check_enableHotCorners.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tb_updateLog);
            tabPage1.Controls.Add(picture_logo);
            tabPage1.Location = new Point(4, 32);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(829, 720);
            tabPage1.TabIndex = 2;
            tabPage1.Tag = "about";
            tabPage1.Text = "关 于";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tb_updateLog
            // 
            tb_updateLog.Font = new Font("Microsoft YaHei", 9F);
            tb_updateLog.Location = new Point(190, 33);
            tb_updateLog.Multiline = true;
            tb_updateLog.Name = "tb_updateLog";
            tb_updateLog.ReadOnly = true;
            tb_updateLog.ScrollBars = ScrollBars.Vertical;
            tb_updateLog.Size = new Size(616, 229);
            tb_updateLog.TabIndex = 3;
            // 
            // picture_logo
            // 
            picture_logo.Image = Resources._128;
            picture_logo.ImeMode = ImeMode.NoControl;
            picture_logo.Location = new Point(15, 33);
            picture_logo.Name = "picture_logo";
            picture_logo.Size = new Size(159, 147);
            picture_logo.SizeMode = PictureBoxSizeMode.Zoom;
            picture_logo.TabIndex = 0;
            picture_logo.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(lb_info);
            flowLayoutPanel4.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel4.Location = new Point(15, 783);
            flowLayoutPanel4.Margin = new Padding(0, 18, 0, 15);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(837, 24);
            flowLayoutPanel4.TabIndex = 10;
            // 
            // lb_info
            // 
            lb_info.AutoSize = true;
            lb_info.ForeColor = Color.Gray;
            lb_info.ImeMode = ImeMode.NoControl;
            lb_info.Location = new Point(638, 0);
            lb_info.Name = "lb_info";
            lb_info.Size = new Size(196, 20);
            lb_info.TabIndex = 6;
            lb_info.Text = "*改动将自动保存并立即生效";
            // 
            // tip
            // 
            tip.AutomaticDelay = 80000;
            tip.AutoPopDelay = 144640;
            tip.InitialDelay = 120;
            tip.ReshowDelay = 2892;
            // 
            // pic_menuBtn
            // 
            pic_menuBtn.BackColor = Color.Transparent;
            pic_menuBtn.Image = Resources.menuBtn;
            pic_menuBtn.ImeMode = ImeMode.NoControl;
            pic_menuBtn.Location = new Point(813, 15);
            pic_menuBtn.Name = "pic_menuBtn";
            pic_menuBtn.Size = new Size(36, 36);
            pic_menuBtn.SizeMode = PictureBoxSizeMode.Zoom;
            pic_menuBtn.TabIndex = 11;
            pic_menuBtn.TabStop = false;
            tip.SetToolTip(pic_menuBtn, "菜单");
            pic_menuBtn.Click += pic_menuBtn_Click;
            pic_menuBtn.MouseDown += pic_menuBtn_MouseDown;
            // 
            // ctx_gesturesMenu
            // 
            ctx_gesturesMenu.ImageScalingSize = new Size(24, 24);
            ctx_gesturesMenu.Items.AddRange(new ToolStripItem[] { menuItem_import, menuItem_export, menuItem_resetGestures });
            ctx_gesturesMenu.Name = "contextMenuStrip1";
            ctx_gesturesMenu.RenderMode = ToolStripRenderMode.System;
            ctx_gesturesMenu.Size = new Size(152, 76);
            ctx_gesturesMenu.Closed += ctx_gesturesMenu_Closed;
            // 
            // menuItem_import
            // 
            menuItem_import.Name = "menuItem_import";
            menuItem_import.Size = new Size(151, 24);
            menuItem_import.Text = "导入...";
            menuItem_import.Click += menuItem_imxport_Click;
            // 
            // menuItem_export
            // 
            menuItem_export.Name = "menuItem_export";
            menuItem_export.Size = new Size(151, 24);
            menuItem_export.Text = "导出...";
            menuItem_export.Click += menuItem_export_Click;
            // 
            // menuItem_resetGestures
            // 
            menuItem_resetGestures.Name = "menuItem_resetGestures";
            menuItem_resetGestures.Size = new Size(151, 24);
            menuItem_resetGestures.Text = "恢复默认...";
            menuItem_resetGestures.Click += menuItem_resetGestures_Click;
            // 
            // errorProvider
            // 
            errorProvider.BlinkRate = 300;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(867, 830);
            Controls.Add(pic_menuBtn);
            Controls.Add(flowLayoutPanel4);
            Controls.Add(tabControl);
            Font = new Font("Microsoft YaHei", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MGestures设置";
            FormClosing += SettingsForm_FormClosing;
            Shown += SettingsForm_Shown;
            tabControl.ResumeLayout(false);
            tabPage_general.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((ISupportInitialize)settingsFormControllerBindingSource).EndInit();
            ((ISupportInitialize)num_pathTrackerInitialStayTimeoutMillis).EndInit();
            ((ISupportInitialize)numPathTrackerStayTimeoutMillis).EndInit();
            ((ISupportInitialize)numPathTrackerInitialValidMove).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((ISupportInitialize)pictureSelectedApp).EndInit();
            panel_intentListOperations.ResumeLayout(false);
            group_Command.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tab_hotCorners.ResumeLayout(false);
            tab_hotCorners.PerformLayout();
            panel_hotcornerSettings.ResumeLayout(false);
            panel_hotcornerSettings.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((ISupportInitialize)picture_logo).EndInit();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            ((ISupportInitialize)pic_menuBtn).EndInit();
            ctx_gesturesMenu.ResumeLayout(false);
            ((ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage_general;
        private TabPage tabPage2;
        private CheckBox check_autoStart;
        private TabPage tabPage1;
        private CheckBox check_autoCheckUpdate;
        private Button btn_checkUpdateNow;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private CheckBox checkGestureViewShowPath;
        private Label label1;
        private CheckBox checkGestureViewShowCommandName;
        private Label label4;
        private Label label5;
        private Label label6;
        private InstantNumericUpDown numPathTrackerInitialValidMove;
        private CheckBox checkPathTrackerStayTimeout;
        private InstantNumericUpDown numPathTrackerStayTimeoutMillis;
        private AlwaysSelectedListView listApps;
        private Label lb_Version;
        private ImageList imglistAppIcons;
        private FlowLayoutPanel flowLayoutPanel1;
        private AlwaysSelectedListView listGestureIntents;
        private ColumnHeader colGestureName;
        private ColumnHeader colGestureDirs;
        private CheckBox check_gesturingDisabled;
        private ColumnHeader colListAppDummy;
        private PictureBox pictureSelectedApp;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label7;
        private MetroButton btnAddApp;
        private MetroButton btnAppRemove;
        private MetroButton btnEditApp;
        private Panel panel_intentListOperations;
        private MetroButton btn_RemoveGesture;
        private MetroButton btnAddGesture;
        private GroupBox group_Command;
        private Label label8;
        private ComboBox combo_CommandTypes;
        private Panel panel_commandView;
        private Windows.Controls.ColorButton colorBtn_unrecogonized;
        private Windows.Controls.ColorButton colorBtn_recogonized;
        private Label label9;
        private ColumnHeader operation;
        private ImageList dummyImgLstForLstViewHeightFix;
        private ToolTip tip;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label lb_info;
        private PictureBox picture_logo;
        private CheckBox checkGestureView_fadeOut;
        private MetroButton btn_modifyGesture;
        private FlowLayoutPanel flowLayoutPanel3;
        private ErrorProvider errorProvider;
        private TextBox tb_updateLog;
        private LineLabel lineLabel1;
        private FlowLayoutPanel flowLayoutPanel6;
        private Panel panel3;
        private CheckBox check_executeOnMouseWheeling;
        private InstantNumericUpDown num_pathTrackerInitialStayTimeoutMillis;
        private CheckBox check_pathTrackerInitialStayTimeout;
        private PictureBox pic_menuBtn;
        private ContextMenuStrip ctx_gesturesMenu;
        private ToolStripMenuItem menuItem_import;
        private ToolStripMenuItem menuItem_export;
        private CheckBox check_disableOnFullscreen;
        private BindingSource settingsFormControllerBindingSource;
        private ColorButton colorMiddle;
        private Panel panel1;
        private CheckBox checkInheritGlobal;
        private LineLabel lineLabel2;
        private Label label3;
        private TabPage tab_hotCorners;
        private CheckBox check_enableHotCorners;
        private Panel panel2;
        private CheckBox check_enable8DirGesture;
        private CheckBox check_preferCursorWindow;
        private ToolStripMenuItem menuItem_resetGestures;
        private Panel panel_cornorCmdView;
        private RadioButton radio_corner_2;
        private RadioButton radio_corner_3;
        private RadioButton radio_corner_0;
        private RadioButton radio_corner_1;
        private ComboBox combo_hotcornerCmdTypes;
        private Label label11;
        private Panel panel_hotcornerSettings;
        private RadioButton radio_edge_3;
        private RadioButton radio_edge_0;
        private RadioButton radio_edge_1;
        private RadioButton radio_edge_2;
        private CheckBox check_enableRubEdge;
        private Label label13;
        private Label label14;
        private ShortcutRecordButton shortcutRec_pause;
        private Label lb_pause_shortcut;
        private Label label15;
        private CheckBox check_gestBtn_X;
        private CheckBox check_gestBtn_Middle;
        private CheckBox check_gestBtn_Right;
        private ColorButton colorBtn_x;
        private CheckBox checkBox1;
    }
}