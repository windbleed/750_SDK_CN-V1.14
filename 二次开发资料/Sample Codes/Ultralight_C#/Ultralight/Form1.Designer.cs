namespace Mifare1K
{
    partial class Mifare_1K
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mifare_1K));
            this.txtSearchPurse = new System.Windows.Forms.TextBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnReqIDL = new System.Windows.Forms.Button();
            this.btnHalt = new System.Windows.Forms.Button();
            this.cbxPage = new System.Windows.Forms.ComboBox();
            this.lablePage = new System.Windows.Forms.Label();
            this.btnReadPage = new System.Windows.Forms.Button();
            this.btnWritePage = new System.Windows.Forms.Button();
            this.txtBoxData = new System.Windows.Forms.TextBox();
            this.txtBoxKey = new System.Windows.Forms.TextBox();
            this.btnAuthenticate = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnEnableVerification = new System.Windows.Forms.Button();
            this.btnDisableVerification = new System.Windows.Forms.Button();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMifare1K = new System.Windows.Forms.ToolStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tsMifare1K.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearchPurse
            // 
            this.txtSearchPurse.Location = new System.Drawing.Point(36, 50);
            this.txtSearchPurse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearchPurse.Name = "txtSearchPurse";
            this.txtSearchPurse.Size = new System.Drawing.Size(160, 21);
            this.txtSearchPurse.TabIndex = 0;
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(211, 47);
            this.btnRequest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(85, 29);
            this.btnRequest.TabIndex = 1;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // btnReqIDL
            // 
            this.btnReqIDL.Location = new System.Drawing.Point(301, 47);
            this.btnReqIDL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReqIDL.Name = "btnReqIDL";
            this.btnReqIDL.Size = new System.Drawing.Size(85, 29);
            this.btnReqIDL.TabIndex = 2;
            this.btnReqIDL.Text = "ReqIDL";
            this.btnReqIDL.UseVisualStyleBackColor = true;
            this.btnReqIDL.Click += new System.EventHandler(this.btnReqIDL_Click);
            // 
            // btnHalt
            // 
            this.btnHalt.Location = new System.Drawing.Point(391, 47);
            this.btnHalt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHalt.Name = "btnHalt";
            this.btnHalt.Size = new System.Drawing.Size(85, 29);
            this.btnHalt.TabIndex = 3;
            this.btnHalt.Text = "Halt";
            this.btnHalt.UseVisualStyleBackColor = true;
            this.btnHalt.Click += new System.EventHandler(this.btnHalt_Click);
            // 
            // cbxPage
            // 
            this.cbxPage.DropDownHeight = 170;
            this.cbxPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPage.FormattingEnabled = true;
            this.cbxPage.IntegralHeight = false;
            this.cbxPage.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43"});
            this.cbxPage.Location = new System.Drawing.Point(123, 35);
            this.cbxPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxPage.Name = "cbxPage";
            this.cbxPage.Size = new System.Drawing.Size(98, 23);
            this.cbxPage.TabIndex = 34;
            // 
            // lablePage
            // 
            this.lablePage.AutoSize = true;
            this.lablePage.Location = new System.Drawing.Point(76, 38);
            this.lablePage.Name = "lablePage";
            this.lablePage.Size = new System.Drawing.Size(36, 15);
            this.lablePage.TabIndex = 35;
            this.lablePage.Text = "Page";
            // 
            // btnReadPage
            // 
            this.btnReadPage.Location = new System.Drawing.Point(114, 125);
            this.btnReadPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReadPage.Name = "btnReadPage";
            this.btnReadPage.Size = new System.Drawing.Size(87, 29);
            this.btnReadPage.TabIndex = 36;
            this.btnReadPage.Text = "Read Page";
            this.btnReadPage.UseVisualStyleBackColor = true;
            this.btnReadPage.Click += new System.EventHandler(this.btnReadPage_Click);
            // 
            // btnWritePage
            // 
            this.btnWritePage.Location = new System.Drawing.Point(235, 125);
            this.btnWritePage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWritePage.Name = "btnWritePage";
            this.btnWritePage.Size = new System.Drawing.Size(87, 29);
            this.btnWritePage.TabIndex = 37;
            this.btnWritePage.Text = "Write Page";
            this.btnWritePage.UseVisualStyleBackColor = true;
            this.btnWritePage.Click += new System.EventHandler(this.btnWritePage_Click);
            // 
            // txtBoxData
            // 
            this.txtBoxData.Location = new System.Drawing.Point(73, 80);
            this.txtBoxData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBoxData.Name = "txtBoxData";
            this.txtBoxData.Size = new System.Drawing.Size(300, 21);
            this.txtBoxData.TabIndex = 38;
            // 
            // txtBoxKey
            // 
            this.txtBoxKey.Location = new System.Drawing.Point(72, 38);
            this.txtBoxKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBoxKey.Name = "txtBoxKey";
            this.txtBoxKey.Size = new System.Drawing.Size(300, 21);
            this.txtBoxKey.TabIndex = 39;
            // 
            // btnAuthenticate
            // 
            this.btnAuthenticate.Location = new System.Drawing.Point(72, 82);
            this.btnAuthenticate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAuthenticate.Name = "btnAuthenticate";
            this.btnAuthenticate.Size = new System.Drawing.Size(141, 29);
            this.btnAuthenticate.TabIndex = 40;
            this.btnAuthenticate.Text = "Authenticate";
            this.btnAuthenticate.UseVisualStyleBackColor = true;
            this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(231, 82);
            this.btnChange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(141, 29);
            this.btnChange.TabIndex = 41;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnEnableVerification
            // 
            this.btnEnableVerification.Location = new System.Drawing.Point(72, 125);
            this.btnEnableVerification.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEnableVerification.Name = "btnEnableVerification";
            this.btnEnableVerification.Size = new System.Drawing.Size(141, 29);
            this.btnEnableVerification.TabIndex = 42;
            this.btnEnableVerification.Text = "Enable verification";
            this.btnEnableVerification.UseVisualStyleBackColor = true;
            this.btnEnableVerification.Click += new System.EventHandler(this.btnEnableVerification_Click);
            // 
            // btnDisableVerification
            // 
            this.btnDisableVerification.Location = new System.Drawing.Point(231, 125);
            this.btnDisableVerification.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDisableVerification.Name = "btnDisableVerification";
            this.btnDisableVerification.Size = new System.Drawing.Size(141, 29);
            this.btnDisableVerification.TabIndex = 43;
            this.btnDisableVerification.Text = "Disable verification";
            this.btnDisableVerification.UseVisualStyleBackColor = true;
            this.btnDisableVerification.Click += new System.EventHandler(this.btnDisableVerification_Click);
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.BackColor = System.Drawing.SystemColors.Control;
            this.tsbtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnConnect.Image")));
            this.tsbtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Size = new System.Drawing.Size(59, 22);
            this.tsbtnConnect.Text = "Connect";
            this.tsbtnConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsMifare1K
            // 
            this.tsMifare1K.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnConnect,
            this.toolStripSeparator1});
            this.tsMifare1K.Location = new System.Drawing.Point(0, 0);
            this.tsMifare1K.Name = "tsMifare1K";
            this.tsMifare1K.Size = new System.Drawing.Size(511, 25);
            this.tsMifare1K.TabIndex = 33;
            this.tsMifare1K.Text = "Mifare1K";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxKey);
            this.groupBox1.Controls.Add(this.btnDisableVerification);
            this.groupBox1.Controls.Add(this.btnAuthenticate);
            this.groupBox1.Controls.Add(this.btnEnableVerification);
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Location = new System.Drawing.Point(31, 99);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(446, 180);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key(Only for Ultralight C)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBoxData);
            this.groupBox2.Controls.Add(this.cbxPage);
            this.groupBox2.Controls.Add(this.lablePage);
            this.groupBox2.Controls.Add(this.btnWritePage);
            this.groupBox2.Controls.Add(this.btnReadPage);
            this.groupBox2.Location = new System.Drawing.Point(31, 298);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(446, 178);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Memory operation";
            // 
            // Mifare_1K
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(511, 495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tsMifare1K);
            this.Controls.Add(this.btnHalt);
            this.Controls.Add(this.btnReqIDL);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.txtSearchPurse);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mifare_1K";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Ultralight (C)";
            this.Load += new System.EventHandler(this.Mifare_1K_Load);
            this.tsMifare1K.ResumeLayout(false);
            this.tsMifare1K.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchPurse;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnReqIDL;
        private System.Windows.Forms.Button btnHalt;
        private System.Windows.Forms.ComboBox cbxPage;
        private System.Windows.Forms.Label lablePage;
        private System.Windows.Forms.Button btnReadPage;
        private System.Windows.Forms.Button btnWritePage;
        private System.Windows.Forms.TextBox txtBoxData;
        private System.Windows.Forms.TextBox txtBoxKey;
        private System.Windows.Forms.Button btnAuthenticate;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnEnableVerification;
        private System.Windows.Forms.Button btnDisableVerification;
        private System.Windows.Forms.ToolStripButton tsbtnConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip tsMifare1K;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

