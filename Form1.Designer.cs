namespace SimAutomation
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("PWS");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Locus");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Error Codes");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Support and folder Links");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Documentation", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("version number");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("version information");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Version Info", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            this.PB_Tab1 = new System.Windows.Forms.ProgressBar();
            this.label_StartButton = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip_technicalhelp = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_CustomRateEnter = new System.Windows.Forms.TextBox();
            this.textBox_tab1_browse = new System.Windows.Forms.TextBox();
            this.checkBox_customRateEnter = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_browse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_CheckStatusALL = new System.Windows.Forms.CheckBox();
            this.CB_t1_viewTechDetails = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_tab1_IMEI_ChechStatus = new System.Windows.Forms.TextBox();
            this.button_tab1_checkStatus = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1_RawResp = new System.Windows.Forms.CheckBox();
            this.label_tab1_SIMID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_tab1_IMEI = new System.Windows.Forms.TextBox();
            this.textBox_tab1_SimCardID = new System.Windows.Forms.TextBox();
            this.button_tab1_ActivateSim = new System.Windows.Forms.Button();
            this.checkBox_Help = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar_FinalFile = new System.Windows.Forms.ProgressBar();
            this.progressBar_StartButton = new System.Windows.Forms.ProgressBar();
            this.progressBar_initialFile = new System.Windows.Forms.ProgressBar();
            this.groupBox2_3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3_finalFile = new System.Windows.Forms.TextBox();
            this.button_FinalFileUpload = new System.Windows.Forms.Button();
            this.groupBox2_2 = new System.Windows.Forms.GroupBox();
            this.button_StartProcessLocus = new System.Windows.Forms.Button();
            this.groupBox_2_1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TextBox_InitialFileUpload = new System.Windows.Forms.TextBox();
            this.button_InitialFileUpload = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.RTB1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.label_3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2_3.SuspendLayout();
            this.groupBox2_2.SuspendLayout();
            this.groupBox_2_1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PB_Tab1
            // 
            this.PB_Tab1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PB_Tab1.Location = new System.Drawing.Point(13, 412);
            this.PB_Tab1.Name = "PB_Tab1";
            this.PB_Tab1.Size = new System.Drawing.Size(292, 34);
            this.PB_Tab1.TabIndex = 4;
            // 
            // label_StartButton
            // 
            this.label_StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_StartButton.AutoSize = true;
            this.label_StartButton.BackColor = System.Drawing.Color.Transparent;
            this.label_StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_StartButton.Location = new System.Drawing.Point(22, 385);
            this.label_StartButton.Name = "label_StartButton";
            this.label_StartButton.Size = new System.Drawing.Size(95, 13);
            this.label_StartButton.TabIndex = 7;
            this.label_StartButton.Text = "The button Pop";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(311, 377);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(151, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.PB_Tab1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label_StartButton);
            this.panel1.Location = new System.Drawing.Point(546, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 462);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 367);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBox_CustomRateEnter);
            this.tabPage1.Controls.Add(this.textBox_tab1_browse);
            this.tabPage1.Controls.Add(this.checkBox_customRateEnter);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button_start);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button_browse);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.checkBox_Help);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 341);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PWS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(118, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "You have pressed the Key";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBox_CustomRateEnter
            // 
            this.textBox_CustomRateEnter.Location = new System.Drawing.Point(26, 275);
            this.textBox_CustomRateEnter.Name = "textBox_CustomRateEnter";
            this.textBox_CustomRateEnter.Size = new System.Drawing.Size(199, 20);
            this.textBox_CustomRateEnter.TabIndex = 19;
            // 
            // textBox_tab1_browse
            // 
            this.textBox_tab1_browse.Location = new System.Drawing.Point(9, 248);
            this.textBox_tab1_browse.Name = "textBox_tab1_browse";
            this.textBox_tab1_browse.Size = new System.Drawing.Size(390, 20);
            this.textBox_tab1_browse.TabIndex = 3;
            // 
            // checkBox_customRateEnter
            // 
            this.checkBox_customRateEnter.AutoSize = true;
            this.checkBox_customRateEnter.Location = new System.Drawing.Point(9, 277);
            this.checkBox_customRateEnter.Name = "checkBox_customRateEnter";
            this.checkBox_customRateEnter.Size = new System.Drawing.Size(111, 17);
            this.checkBox_customRateEnter.TabIndex = 18;
            this.checkBox_customRateEnter.Text = "Custom Rate Plan";
            this.checkBox_customRateEnter.UseVisualStyleBackColor = true;
            this.checkBox_customRateEnter.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(6, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "activation throught \"Start Button\" process.";
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.Color.Silver;
            this.button_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start.Location = new System.Drawing.Point(9, 300);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(423, 35);
            this.button_start.TabIndex = 1;
            this.button_start.Text = "< < < < < < < < Start > > > > > > > >";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_tab1_start_Click);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(6, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Only use to check status, It does not request activation if it has not already re" +
    "quested";
            // 
            // button_browse
            // 
            this.button_browse.BackColor = System.Drawing.Color.Transparent;
            this.button_browse.Location = new System.Drawing.Point(405, 248);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(27, 20);
            this.button_browse.TabIndex = 2;
            this.button_browse.Text = "...";
            this.button_browse.UseVisualStyleBackColor = false;
            this.button_browse.Click += new System.EventHandler(this.button_tab1_browse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_CheckStatusALL);
            this.groupBox2.Controls.Add(this.CB_t1_viewTechDetails);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_tab1_IMEI_ChechStatus);
            this.groupBox2.Controls.Add(this.button_tab1_checkStatus);
            this.groupBox2.Location = new System.Drawing.Point(9, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 84);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Check Status of Individual Sim";
            // 
            // checkBox_CheckStatusALL
            // 
            this.checkBox_CheckStatusALL.AutoSize = true;
            this.checkBox_CheckStatusALL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_CheckStatusALL.Location = new System.Drawing.Point(144, 60);
            this.checkBox_CheckStatusALL.Name = "checkBox_CheckStatusALL";
            this.checkBox_CheckStatusALL.Size = new System.Drawing.Size(109, 17);
            this.checkBox_CheckStatusALL.TabIndex = 16;
            this.checkBox_CheckStatusALL.Text = "view Status for all";
            this.checkBox_CheckStatusALL.UseVisualStyleBackColor = true;
            this.checkBox_CheckStatusALL.CheckStateChanged += new System.EventHandler(this.checkBox_tab1_CheckStatusALL_CheckStateChanged);
            // 
            // CB_t1_viewTechDetails
            // 
            this.CB_t1_viewTechDetails.AutoSize = true;
            this.CB_t1_viewTechDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_t1_viewTechDetails.Location = new System.Drawing.Point(7, 60);
            this.CB_t1_viewTechDetails.Name = "CB_t1_viewTechDetails";
            this.CB_t1_viewTechDetails.Size = new System.Drawing.Size(131, 17);
            this.CB_t1_viewTechDetails.TabIndex = 15;
            this.CB_t1_viewTechDetails.Text = "view Technical details";
            this.CB_t1_viewTechDetails.UseVisualStyleBackColor = true;
            this.CB_t1_viewTechDetails.CheckStateChanged += new System.EventHandler(this.checkBox_tab1_CheckStatus_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(156, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Type or Paste 20 digit SimCard ID";
            // 
            // textBox_tab1_IMEI_ChechStatus
            // 
            this.textBox_tab1_IMEI_ChechStatus.Location = new System.Drawing.Point(6, 19);
            this.textBox_tab1_IMEI_ChechStatus.Name = "textBox_tab1_IMEI_ChechStatus";
            this.textBox_tab1_IMEI_ChechStatus.Size = new System.Drawing.Size(292, 20);
            this.textBox_tab1_IMEI_ChechStatus.TabIndex = 7;
            this.textBox_tab1_IMEI_ChechStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_tab1_IMEI_ChechStatus_KeyPress);
            // 
            // button_tab1_checkStatus
            // 
            this.button_tab1_checkStatus.Location = new System.Drawing.Point(304, 12);
            this.button_tab1_checkStatus.Name = "button_tab1_checkStatus";
            this.button_tab1_checkStatus.Size = new System.Drawing.Size(113, 65);
            this.button_tab1_checkStatus.TabIndex = 8;
            this.button_tab1_checkStatus.Text = "Check Status";
            this.button_tab1_checkStatus.UseVisualStyleBackColor = true;
            this.button_tab1_checkStatus.Click += new System.EventHandler(this.button_tab1_checkStatus_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1_RawResp);
            this.groupBox1.Controls.Add(this.label_tab1_SIMID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_tab1_IMEI);
            this.groupBox1.Controls.Add(this.textBox_tab1_SimCardID);
            this.groupBox1.Controls.Add(this.button_tab1_ActivateSim);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 111);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activate Individual Sim";
            // 
            // checkBox1_RawResp
            // 
            this.checkBox1_RawResp.AutoSize = true;
            this.checkBox1_RawResp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1_RawResp.Location = new System.Drawing.Point(6, 88);
            this.checkBox1_RawResp.Name = "checkBox1_RawResp";
            this.checkBox1_RawResp.Size = new System.Drawing.Size(126, 17);
            this.checkBox1_RawResp.TabIndex = 16;
            this.checkBox1_RawResp.Text = "view RAW response.";
            this.checkBox1_RawResp.UseVisualStyleBackColor = true;
            // 
            // label_tab1_SIMID
            // 
            this.label_tab1_SIMID.AutoSize = true;
            this.label_tab1_SIMID.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tab1_SIMID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_tab1_SIMID.Location = new System.Drawing.Point(156, 42);
            this.label_tab1_SIMID.Name = "label_tab1_SIMID";
            this.label_tab1_SIMID.Size = new System.Drawing.Size(142, 12);
            this.label_tab1_SIMID.TabIndex = 13;
            this.label_tab1_SIMID.Text = "Type or Paste 20 digit SimCard ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(183, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "Type or Paste 15 digit IMEI";
            // 
            // textBox_tab1_IMEI
            // 
            this.textBox_tab1_IMEI.Location = new System.Drawing.Point(6, 62);
            this.textBox_tab1_IMEI.Name = "textBox_tab1_IMEI";
            this.textBox_tab1_IMEI.Size = new System.Drawing.Size(292, 20);
            this.textBox_tab1_IMEI.TabIndex = 1;
            // 
            // textBox_tab1_SimCardID
            // 
            this.textBox_tab1_SimCardID.Location = new System.Drawing.Point(6, 19);
            this.textBox_tab1_SimCardID.Name = "textBox_tab1_SimCardID";
            this.textBox_tab1_SimCardID.Size = new System.Drawing.Size(292, 20);
            this.textBox_tab1_SimCardID.TabIndex = 0;
            // 
            // button_tab1_ActivateSim
            // 
            this.button_tab1_ActivateSim.Location = new System.Drawing.Point(304, 12);
            this.button_tab1_ActivateSim.Name = "button_tab1_ActivateSim";
            this.button_tab1_ActivateSim.Size = new System.Drawing.Size(113, 85);
            this.button_tab1_ActivateSim.TabIndex = 6;
            this.button_tab1_ActivateSim.Text = "Activate Sim";
            this.button_tab1_ActivateSim.UseVisualStyleBackColor = true;
            this.button_tab1_ActivateSim.Click += new System.EventHandler(this.button_tab1_ActivateSim_Click);
            // 
            // checkBox_Help
            // 
            this.checkBox_Help.AutoSize = true;
            this.checkBox_Help.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_Help.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Help.Location = new System.Drawing.Point(335, 277);
            this.checkBox_Help.Name = "checkBox_Help";
            this.checkBox_Help.Size = new System.Drawing.Size(97, 17);
            this.checkBox_Help.TabIndex = 17;
            this.checkBox_Help.Text = "view Help Text";
            this.checkBox_Help.UseVisualStyleBackColor = false;
            this.checkBox_Help.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label_3);
            this.tabPage3.Controls.Add(this.label_2);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.progressBar_FinalFile);
            this.tabPage3.Controls.Add(this.progressBar_StartButton);
            this.tabPage3.Controls.Add(this.progressBar_initialFile);
            this.tabPage3.Controls.Add(this.groupBox2_3);
            this.tabPage3.Controls.Add(this.groupBox2_2);
            this.tabPage3.Controls.Add(this.groupBox_2_1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(441, 341);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Locus";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(423, 282);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(347, 315);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Final File";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(160, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Processing the Files>>>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 315);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Initial File>>>>>>";
            // 
            // progressBar_FinalFile
            // 
            this.progressBar_FinalFile.Location = new System.Drawing.Point(296, 302);
            this.progressBar_FinalFile.Name = "progressBar_FinalFile";
            this.progressBar_FinalFile.Size = new System.Drawing.Size(143, 10);
            this.progressBar_FinalFile.TabIndex = 13;
            // 
            // progressBar_StartButton
            // 
            this.progressBar_StartButton.Location = new System.Drawing.Point(149, 302);
            this.progressBar_StartButton.Name = "progressBar_StartButton";
            this.progressBar_StartButton.Size = new System.Drawing.Size(143, 10);
            this.progressBar_StartButton.TabIndex = 9;
            // 
            // progressBar_initialFile
            // 
            this.progressBar_initialFile.Location = new System.Drawing.Point(2, 302);
            this.progressBar_initialFile.Name = "progressBar_initialFile";
            this.progressBar_initialFile.Size = new System.Drawing.Size(143, 10);
            this.progressBar_initialFile.TabIndex = 12;
            // 
            // groupBox2_3
            // 
            this.groupBox2_3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.groupBox2_3.Controls.Add(this.label10);
            this.groupBox2_3.Controls.Add(this.textBox3_finalFile);
            this.groupBox2_3.Controls.Add(this.button_FinalFileUpload);
            this.groupBox2_3.Location = new System.Drawing.Point(23, 172);
            this.groupBox2_3.Name = "groupBox2_3";
            this.groupBox2_3.Size = new System.Drawing.Size(415, 69);
            this.groupBox2_3.TabIndex = 11;
            this.groupBox2_3.TabStop = false;
            this.groupBox2_3.Text = "Final File. make/Upload";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "-----";
            // 
            // textBox3_finalFile
            // 
            this.textBox3_finalFile.Location = new System.Drawing.Point(9, 19);
            this.textBox3_finalFile.Name = "textBox3_finalFile";
            this.textBox3_finalFile.Size = new System.Drawing.Size(420, 20);
            this.textBox3_finalFile.TabIndex = 8;
            this.textBox3_finalFile.TextChanged += new System.EventHandler(this.textBox3_finalFile_TextChanged);
            // 
            // button_FinalFileUpload
            // 
            this.button_FinalFileUpload.Location = new System.Drawing.Point(296, 40);
            this.button_FinalFileUpload.Name = "button_FinalFileUpload";
            this.button_FinalFileUpload.Size = new System.Drawing.Size(134, 23);
            this.button_FinalFileUpload.TabIndex = 5;
            this.button_FinalFileUpload.Text = "Final File Upload";
            this.button_FinalFileUpload.UseVisualStyleBackColor = true;
            this.button_FinalFileUpload.Click += new System.EventHandler(this.button_FinalFileUpload_Click);
            // 
            // groupBox2_2
            // 
            this.groupBox2_2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.groupBox2_2.Controls.Add(this.button_StartProcessLocus);
            this.groupBox2_2.Location = new System.Drawing.Point(23, 97);
            this.groupBox2_2.Name = "groupBox2_2";
            this.groupBox2_2.Size = new System.Drawing.Size(415, 69);
            this.groupBox2_2.TabIndex = 11;
            this.groupBox2_2.TabStop = false;
            this.groupBox2_2.Text = "Verification Process";
            // 
            // button_StartProcessLocus
            // 
            this.button_StartProcessLocus.Location = new System.Drawing.Point(151, 21);
            this.button_StartProcessLocus.Name = "button_StartProcessLocus";
            this.button_StartProcessLocus.Size = new System.Drawing.Size(134, 32);
            this.button_StartProcessLocus.TabIndex = 3;
            this.button_StartProcessLocus.Text = "Start";
            this.button_StartProcessLocus.UseVisualStyleBackColor = true;
            this.button_StartProcessLocus.Click += new System.EventHandler(this.button_Tab2_Start);
            // 
            // groupBox_2_1
            // 
            this.groupBox_2_1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.groupBox_2_1.Controls.Add(this.label11);
            this.groupBox_2_1.Controls.Add(this.TextBox_InitialFileUpload);
            this.groupBox_2_1.Controls.Add(this.button_InitialFileUpload);
            this.groupBox_2_1.Location = new System.Drawing.Point(23, 22);
            this.groupBox_2_1.Name = "groupBox_2_1";
            this.groupBox_2_1.Size = new System.Drawing.Size(415, 69);
            this.groupBox_2_1.TabIndex = 10;
            this.groupBox_2_1.TabStop = false;
            this.groupBox_2_1.Text = "Initial File. Make/Upload";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(148, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "-----";
            // 
            // TextBox_InitialFileUpload
            // 
            this.TextBox_InitialFileUpload.Location = new System.Drawing.Point(9, 19);
            this.TextBox_InitialFileUpload.Name = "TextBox_InitialFileUpload";
            this.TextBox_InitialFileUpload.Size = new System.Drawing.Size(420, 20);
            this.TextBox_InitialFileUpload.TabIndex = 6;
            // 
            // button_InitialFileUpload
            // 
            this.button_InitialFileUpload.Location = new System.Drawing.Point(8, 40);
            this.button_InitialFileUpload.Name = "button_InitialFileUpload";
            this.button_InitialFileUpload.Size = new System.Drawing.Size(134, 23);
            this.button_InitialFileUpload.TabIndex = 4;
            this.button_InitialFileUpload.Text = "Initial File Upload";
            this.button_InitialFileUpload.UseVisualStyleBackColor = true;
            this.button_InitialFileUpload.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.treeView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(441, 341);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Documentation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Location = new System.Drawing.Point(6, 6);
            this.treeView1.Name = "treeView1";
            treeNode9.Name = "Node3";
            treeNode9.Text = "PWS";
            treeNode10.Name = "Node4";
            treeNode10.Text = "Locus";
            treeNode11.Name = "Node5";
            treeNode11.Text = "Error Codes";
            treeNode12.Name = "Node6";
            treeNode12.Text = "Support and folder Links";
            treeNode13.Name = "Node2";
            treeNode13.Text = "Documentation";
            treeNode14.Name = "Node1";
            treeNode14.Text = "version number";
            treeNode15.Name = "Node3";
            treeNode15.Text = "version information";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Version Info";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode16});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(427, 329);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseEnter += new System.EventHandler(this.treeView1_MouseEnter);
            // 
            // RTB1
            // 
            this.RTB1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB1.BackColor = System.Drawing.Color.White;
            this.RTB1.DetectUrls = false;
            this.RTB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTB1.Location = new System.Drawing.Point(12, 8);
            this.RTB1.Name = "RTB1";
            this.RTB1.ReadOnly = true;
            this.RTB1.Size = new System.Drawing.Size(528, 462);
            this.RTB1.TabIndex = 0;
            this.RTB1.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "PDF document address";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "Copy Address to Clipboard";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "1.";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(5, 108);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(16, 13);
            this.label_2.TabIndex = 19;
            this.label_2.Text = "2.";
            this.label_2.Visible = false;
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Location = new System.Drawing.Point(5, 181);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(16, 13);
            this.label_3.TabIndex = 20;
            this.label_3.Text = "3.";
            this.label_3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1064, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RTB1);
            this.Name = "Form1";
            this.Text = "Sim Automation Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2_3.ResumeLayout(false);
            this.groupBox2_3.PerformLayout();
            this.groupBox2_2.ResumeLayout(false);
            this.groupBox_2_1.ResumeLayout(false);
            this.groupBox_2_1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar PB_Tab1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_StartButton;
        private System.Windows.Forms.ToolTip toolTip_technicalhelp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_CustomRateEnter;
        private System.Windows.Forms.TextBox textBox_tab1_browse;
        private System.Windows.Forms.CheckBox checkBox_customRateEnter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_CheckStatusALL;
        private System.Windows.Forms.CheckBox CB_t1_viewTechDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_tab1_IMEI_ChechStatus;
        private System.Windows.Forms.Button button_tab1_checkStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1_RawResp;
        private System.Windows.Forms.Label label_tab1_SIMID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_tab1_IMEI;
        private System.Windows.Forms.TextBox textBox_tab1_SimCardID;
        private System.Windows.Forms.Button button_tab1_ActivateSim;
        private System.Windows.Forms.CheckBox checkBox_Help;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar_FinalFile;
        private System.Windows.Forms.ProgressBar progressBar_StartButton;
        private System.Windows.Forms.ProgressBar progressBar_initialFile;
        private System.Windows.Forms.GroupBox groupBox2_3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox3_finalFile;
        private System.Windows.Forms.Button button_FinalFileUpload;
        private System.Windows.Forms.GroupBox groupBox2_2;
        private System.Windows.Forms.Button button_StartProcessLocus;
        private System.Windows.Forms.GroupBox groupBox_2_1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TextBox_InitialFileUpload;
        private System.Windows.Forms.Button button_InitialFileUpload;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox RTB1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Label label12;
    }
}

