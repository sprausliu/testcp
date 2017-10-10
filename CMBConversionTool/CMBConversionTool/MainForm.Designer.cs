namespace CMBConversionTool
{
    partial class MainForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listMsg = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGenerateFile = new System.Windows.Forms.Button();
            this.btnSelFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFilePathFCY = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listMsg);
            this.groupBox2.Location = new System.Drawing.Point(445, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 258);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message Info";
            // 
            // listMsg
            // 
            this.listMsg.FormattingEnabled = true;
            this.listMsg.Location = new System.Drawing.Point(6, 21);
            this.listMsg.Name = "listMsg";
            this.listMsg.Size = new System.Drawing.Size(384, 225);
            this.listMsg.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(410, 258);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGenerateFile);
            this.tabPage1.Controls.Add(this.btnSelFile);
            this.tabPage1.Controls.Add(this.txtFilePath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 232);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "本币支付";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGenerateFile
            // 
            this.btnGenerateFile.Location = new System.Drawing.Point(18, 84);
            this.btnGenerateFile.Name = "btnGenerateFile";
            this.btnGenerateFile.Size = new System.Drawing.Size(155, 23);
            this.btnGenerateFile.TabIndex = 22;
            this.btnGenerateFile.Text = "生成本币批量支付文件";
            this.btnGenerateFile.UseVisualStyleBackColor = true;
            this.btnGenerateFile.Click += new System.EventHandler(this.btnGenerateFile_Click);
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(357, 25);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(27, 23);
            this.btnSelFile.TabIndex = 21;
            this.btnSelFile.Text = "...";
            this.btnSelFile.UseVisualStyleBackColor = false;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(18, 27);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(333, 20);
            this.txtFilePath.TabIndex = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.txtFilePathFCY);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(402, 232);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "外币支付";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "生成外币批量支付文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(357, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFilePathFCY
            // 
            this.txtFilePathFCY.Location = new System.Drawing.Point(18, 27);
            this.txtFilePathFCY.Name = "txtFilePathFCY";
            this.txtFilePathFCY.ReadOnly = true;
            this.txtFilePathFCY.Size = new System.Drawing.Size(333, 20);
            this.txtFilePathFCY.TabIndex = 23;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 313);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CMB Conversion Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listMsg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnGenerateFile;
        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtFilePathFCY;
    }
}

