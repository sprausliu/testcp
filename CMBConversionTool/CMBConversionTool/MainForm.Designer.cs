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
            this.btnSelFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listMsg = new System.Windows.Forms.ListBox();
            this.btnGenerateFile = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(359, 43);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(27, 23);
            this.btnSelFile.TabIndex = 16;
            this.btnSelFile.Text = "...";
            this.btnSelFile.UseVisualStyleBackColor = false;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(20, 45);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(333, 20);
            this.txtFilePath.TabIndex = 15;
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
            // btnGenerateFile
            // 
            this.btnGenerateFile.Location = new System.Drawing.Point(20, 102);
            this.btnGenerateFile.Name = "btnGenerateFile";
            this.btnGenerateFile.Size = new System.Drawing.Size(120, 23);
            this.btnGenerateFile.TabIndex = 19;
            this.btnGenerateFile.Text = "Generate File";
            this.btnGenerateFile.UseVisualStyleBackColor = true;
            this.btnGenerateFile.Click += new System.EventHandler(this.btnGenerateFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 313);
            this.Controls.Add(this.btnGenerateFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSelFile);
            this.Controls.Add(this.txtFilePath);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CMB Conversion Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listMsg;
        private System.Windows.Forms.Button btnGenerateFile;
    }
}

