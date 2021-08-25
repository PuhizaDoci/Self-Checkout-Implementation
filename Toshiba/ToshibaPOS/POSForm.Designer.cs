namespace ToshibaPOS
{
    partial class POSForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtDergimiInfo = new System.Windows.Forms.TextBox();
            this.txtImportimiInfo = new System.Windows.Forms.TextBox();
            this.txtExeption = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtListenerPort = new System.Windows.Forms.TextBox();
            this.txtListenerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtListener = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKomanda = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDetalet = new System.Windows.Forms.Label();
            this.grdDetalet = new System.Windows.Forms.DataGridView();
            this.btnRuaj = new System.Windows.Forms.Button();
            this.grdCash = new System.Windows.Forms.DataGridView();
            this.btnPos = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grdDaljaMallitDetale = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaljaMallitDetale)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 584);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage1.Controls.Add(this.txtDergimiInfo);
            this.tabPage1.Controls.Add(this.txtImportimiInfo);
            this.tabPage1.Controls.Add(this.txtExeption);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtListenerPort);
            this.tabPage1.Controls.Add(this.txtListenerIP);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtListener);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtKomanda);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.txtIP);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(628, 558);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "    POS    ";
            // 
            // txtDergimiInfo
            // 
            this.txtDergimiInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(166)))), ((int)(((byte)(204)))));
            this.txtDergimiInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDergimiInfo.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtDergimiInfo.ForeColor = System.Drawing.Color.Black;
            this.txtDergimiInfo.Location = new System.Drawing.Point(70, 332);
            this.txtDergimiInfo.Multiline = true;
            this.txtDergimiInfo.Name = "txtDergimiInfo";
            this.txtDergimiInfo.ReadOnly = true;
            this.txtDergimiInfo.Size = new System.Drawing.Size(266, 20);
            this.txtDergimiInfo.TabIndex = 2;
            this.txtDergimiInfo.Text = " ";
            // 
            // txtImportimiInfo
            // 
            this.txtImportimiInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(166)))), ((int)(((byte)(204)))));
            this.txtImportimiInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImportimiInfo.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtImportimiInfo.ForeColor = System.Drawing.Color.Black;
            this.txtImportimiInfo.Location = new System.Drawing.Point(345, 332);
            this.txtImportimiInfo.Multiline = true;
            this.txtImportimiInfo.Name = "txtImportimiInfo";
            this.txtImportimiInfo.ReadOnly = true;
            this.txtImportimiInfo.Size = new System.Drawing.Size(266, 20);
            this.txtImportimiInfo.TabIndex = 47;
            this.txtImportimiInfo.Text = " ";
            // 
            // txtExeption
            // 
            this.txtExeption.BackColor = System.Drawing.SystemColors.Control;
            this.txtExeption.Location = new System.Drawing.Point(70, 464);
            this.txtExeption.Multiline = true;
            this.txtExeption.Name = "txtExeption";
            this.txtExeption.Size = new System.Drawing.Size(541, 75);
            this.txtExeption.TabIndex = 46;
            this.txtExeption.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Listener Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 384);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Listener Ip";
            // 
            // txtListenerPort
            // 
            this.txtListenerPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtListenerPort.Enabled = false;
            this.txtListenerPort.Location = new System.Drawing.Point(414, 406);
            this.txtListenerPort.Name = "txtListenerPort";
            this.txtListenerPort.Size = new System.Drawing.Size(197, 20);
            this.txtListenerPort.TabIndex = 35;
            this.txtListenerPort.Text = "6697";
            // 
            // txtListenerIP
            // 
            this.txtListenerIP.BackColor = System.Drawing.SystemColors.Control;
            this.txtListenerIP.Enabled = false;
            this.txtListenerIP.Location = new System.Drawing.Point(414, 381);
            this.txtListenerIP.Name = "txtListenerIP";
            this.txtListenerIP.Size = new System.Drawing.Size(197, 20);
            this.txtListenerIP.TabIndex = 34;
            this.txtListenerIP.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Listener";
            // 
            // txtListener
            // 
            this.txtListener.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtListener.BackColor = System.Drawing.SystemColors.Control;
            this.txtListener.Location = new System.Drawing.Point(70, 192);
            this.txtListener.Multiline = true;
            this.txtListener.Name = "txtListener";
            this.txtListener.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtListener.Size = new System.Drawing.Size(541, 101);
            this.txtListener.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Ip";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Komanda";
            // 
            // txtKomanda
            // 
            this.txtKomanda.BackColor = System.Drawing.SystemColors.Control;
            this.txtKomanda.Location = new System.Drawing.Point(70, 21);
            this.txtKomanda.Multiline = true;
            this.txtKomanda.Name = "txtKomanda";
            this.txtKomanda.Size = new System.Drawing.Size(541, 133);
            this.txtKomanda.TabIndex = 27;
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.SystemColors.Control;
            this.txtPort.Enabled = false;
            this.txtPort.Location = new System.Drawing.Point(70, 406);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(218, 20);
            this.txtPort.TabIndex = 26;
            this.txtPort.Text = "11000";
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.SystemColors.Control;
            this.txtIP.Enabled = false;
            this.txtIP.Location = new System.Drawing.Point(70, 380);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(218, 20);
            this.txtIP.TabIndex = 25;
            this.txtIP.Text = "127.0.0.1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage2.Controls.Add(this.lblDetalet);
            this.tabPage2.Controls.Add(this.grdDetalet);
            this.tabPage2.Controls.Add(this.btnRuaj);
            this.tabPage2.Controls.Add(this.grdCash);
            this.tabPage2.Controls.Add(this.btnPos);
            this.tabPage2.Controls.Add(this.btnCash);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.grdDaljaMallitDetale);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(628, 558);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "  Fatura    ";
            // 
            // lblDetalet
            // 
            this.lblDetalet.AutoSize = true;
            this.lblDetalet.Location = new System.Drawing.Point(10, 352);
            this.lblDetalet.Name = "lblDetalet";
            this.lblDetalet.Size = new System.Drawing.Size(85, 13);
            this.lblDetalet.TabIndex = 51;
            this.lblDetalet.Text = "Detalet e fatures";
            // 
            // grdDetalet
            // 
            this.grdDetalet.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdDetalet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDetalet.Location = new System.Drawing.Point(7, 368);
            this.grdDetalet.Name = "grdDetalet";
            this.grdDetalet.Size = new System.Drawing.Size(611, 173);
            this.grdDetalet.TabIndex = 50;
            // 
            // btnRuaj
            // 
            this.btnRuaj.Location = new System.Drawing.Point(525, 290);
            this.btnRuaj.Name = "btnRuaj";
            this.btnRuaj.Size = new System.Drawing.Size(93, 50);
            this.btnRuaj.TabIndex = 49;
            this.btnRuaj.Text = "Ruaj";
            this.btnRuaj.UseVisualStyleBackColor = true;
            // 
            // grdCash
            // 
            this.grdCash.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdCash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCash.Location = new System.Drawing.Point(6, 202);
            this.grdCash.Name = "grdCash";
            this.grdCash.Size = new System.Drawing.Size(380, 138);
            this.grdCash.TabIndex = 48;
            // 
            // btnPos
            // 
            this.btnPos.Location = new System.Drawing.Point(99, 168);
            this.btnPos.Name = "btnPos";
            this.btnPos.Size = new System.Drawing.Size(84, 28);
            this.btnPos.TabIndex = 47;
            this.btnPos.Text = "Credit";
            this.btnPos.UseVisualStyleBackColor = true;
            // 
            // btnCash
            // 
            this.btnCash.Location = new System.Drawing.Point(7, 168);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(86, 28);
            this.btnCash.TabIndex = 46;
            this.btnCash.Text = "Cash";
            this.btnCash.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Shifra ose Barkodi";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(103, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 20);
            this.textBox1.TabIndex = 43;
            // 
            // grdDaljaMallitDetale
            // 
            this.grdDaljaMallitDetale.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdDaljaMallitDetale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDaljaMallitDetale.Location = new System.Drawing.Point(7, 39);
            this.grdDaljaMallitDetale.Name = "grdDaljaMallitDetale";
            this.grdDaljaMallitDetale.Size = new System.Drawing.Size(612, 123);
            this.grdDaljaMallitDetale.TabIndex = 42;
            // 
            // POSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(644, 586);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "POSForm";
            this.Text = "Kubit POS";
            this.Load += new System.EventHandler(this.POSForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaljaMallitDetale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtExeption;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtListenerPort;
        private System.Windows.Forms.TextBox txtListenerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtListener;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKomanda;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblDetalet;
        private System.Windows.Forms.DataGridView grdDetalet;
        private System.Windows.Forms.Button btnRuaj;
        public System.Windows.Forms.DataGridView grdCash;
        private System.Windows.Forms.Button btnPos;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.DataGridView grdDaljaMallitDetale;
        private System.Windows.Forms.TextBox txtImportimiInfo;
        private System.Windows.Forms.TextBox txtDergimiInfo;
    }
}

