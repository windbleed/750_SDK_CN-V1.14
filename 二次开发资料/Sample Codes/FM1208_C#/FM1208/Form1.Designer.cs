namespace Mifare1K
{
    partial class FM1208
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM1208));
            this.tsbtnConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMifare1K = new System.Windows.Forms.ToolStrip();
            this.textCSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textResetInf = new System.Windows.Forms.TextBox();
            this.textCommand = new System.Windows.Forms.TextBox();
            this.textResponse = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.tsMifare1K.SuspendLayout();
            this.SuspendLayout();
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
            this.tsMifare1K.Size = new System.Drawing.Size(696, 25);
            this.tsMifare1K.TabIndex = 33;
            this.tsMifare1K.Text = "Mifare1K";
            // 
            // textCSN
            // 
            this.textCSN.Location = new System.Drawing.Point(120, 59);
            this.textCSN.Name = "textCSN";
            this.textCSN.Size = new System.Drawing.Size(522, 21);
            this.textCSN.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "CSN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "ResetInf:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "Command:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Response:";
            // 
            // textResetInf
            // 
            this.textResetInf.Location = new System.Drawing.Point(120, 114);
            this.textResetInf.Name = "textResetInf";
            this.textResetInf.Size = new System.Drawing.Size(522, 21);
            this.textResetInf.TabIndex = 39;
            // 
            // textCommand
            // 
            this.textCommand.Location = new System.Drawing.Point(120, 169);
            this.textCommand.Name = "textCommand";
            this.textCommand.Size = new System.Drawing.Size(522, 21);
            this.textCommand.TabIndex = 40;
            // 
            // textResponse
            // 
            this.textResponse.Location = new System.Drawing.Point(120, 224);
            this.textResponse.Multiline = true;
            this.textResponse.Name = "textResponse";
            this.textResponse.Size = new System.Drawing.Size(522, 107);
            this.textResponse.TabIndex = 41;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(187, 382);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 23);
            this.btnReset.TabIndex = 42;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(400, 382);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(111, 23);
            this.btnSendCommand.TabIndex = 43;
            this.btnSendCommand.Text = "Send Command";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // FM1208
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(696, 457);
            this.Controls.Add(this.btnSendCommand);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.textResponse);
            this.Controls.Add(this.textCommand);
            this.Controls.Add(this.textResetInf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCSN);
            this.Controls.Add(this.tsMifare1K);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FM1208";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FM1208";
            this.Load += new System.EventHandler(this.Mifare_1K_Load);
            this.tsMifare1K.ResumeLayout(false);
            this.tsMifare1K.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbtnConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip tsMifare1K;
        private System.Windows.Forms.TextBox textCSN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textResetInf;
        private System.Windows.Forms.TextBox textCommand;
        private System.Windows.Forms.TextBox textResponse;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSendCommand;
    }
}

