namespace ToshibaPosSinkronizimi
{
    partial class SinkronizimiForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SinkronizimiForm));
            this.lblMesazhi = new System.Windows.Forms.Label();
            this.tmrCdoImportimi = new System.Windows.Forms.Timer(this.components);
            this.lblKohaDerguar = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.grbKonfigurimet = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDergimiSekonda = new System.Windows.Forms.Label();
            this.lblImportimiSekonda = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDergimiTik = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblImpotimiTik = new System.Windows.Forms.Label();
            this.tmrCdoSekondDergimi = new System.Windows.Forms.Timer(this.components);
            this.btnImporto = new System.Windows.Forms.Button();
            this.btnFZ = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.grbKonfigurimet.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMesazhi
            // 
            this.lblMesazhi.AutoSize = true;
            this.lblMesazhi.Location = new System.Drawing.Point(73, 93);
            this.lblMesazhi.Name = "lblMesazhi";
            this.lblMesazhi.Size = new System.Drawing.Size(54, 13);
            this.lblMesazhi.TabIndex = 17;
            this.lblMesazhi.Text = "Importimi :";
            // 
            // tmrCdoImportimi
            // 
            this.tmrCdoImportimi.Interval = 1000;
            this.tmrCdoImportimi.Tick += new System.EventHandler(this.tmrCdoSekond_Tick);
            // 
            // lblKohaDerguar
            // 
            this.lblKohaDerguar.AutoSize = true;
            this.lblKohaDerguar.Location = new System.Drawing.Point(73, 134);
            this.lblKohaDerguar.Name = "lblKohaDerguar";
            this.lblKohaDerguar.Size = new System.Drawing.Size(42, 13);
            this.lblKohaDerguar.TabIndex = 18;
            this.lblKohaDerguar.Text = "Dergimi";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Kubit POS Sinkronizimi";
            this.notifyIcon.BalloonTipTitle = "Sinkronizimi";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Kubit POS Sinkronizimi";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(76, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(269, 39);
            this.button1.TabIndex = 19;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grbKonfigurimet
            // 
            this.grbKonfigurimet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbKonfigurimet.Controls.Add(this.label2);
            this.grbKonfigurimet.Controls.Add(this.lblDergimiSekonda);
            this.grbKonfigurimet.Controls.Add(this.lblImportimiSekonda);
            this.grbKonfigurimet.Controls.Add(this.label1);
            this.grbKonfigurimet.Controls.Add(this.label4);
            this.grbKonfigurimet.Controls.Add(this.lblDergimiTik);
            this.grbKonfigurimet.Controls.Add(this.label3);
            this.grbKonfigurimet.Controls.Add(this.lblImpotimiTik);
            this.grbKonfigurimet.Location = new System.Drawing.Point(76, 242);
            this.grbKonfigurimet.Name = "grbKonfigurimet";
            this.grbKonfigurimet.Size = new System.Drawing.Size(269, 83);
            this.grbKonfigurimet.TabIndex = 20;
            this.grbKonfigurimet.TabStop = false;
            this.grbKonfigurimet.Text = "Konfigurimet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Koha dergimit sekonda :";
            // 
            // lblDergimiSekonda
            // 
            this.lblDergimiSekonda.AutoSize = true;
            this.lblDergimiSekonda.Location = new System.Drawing.Point(189, 51);
            this.lblDergimiSekonda.Name = "lblDergimiSekonda";
            this.lblDergimiSekonda.Size = new System.Drawing.Size(42, 13);
            this.lblDergimiSekonda.TabIndex = 0;
            this.lblDergimiSekonda.Text = "Dergimi";
            // 
            // lblImportimiSekonda
            // 
            this.lblImportimiSekonda.AutoSize = true;
            this.lblImportimiSekonda.Location = new System.Drawing.Point(189, 24);
            this.lblImportimiSekonda.Name = "lblImportimiSekonda";
            this.lblImportimiSekonda.Size = new System.Drawing.Size(48, 13);
            this.lblImportimiSekonda.TabIndex = 0;
            this.lblImportimiSekonda.Text = "Importimi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Koha importimit sekonda :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(171, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "/";
            // 
            // lblDergimiTik
            // 
            this.lblDergimiTik.AutoSize = true;
            this.lblDergimiTik.BackColor = System.Drawing.Color.Transparent;
            this.lblDergimiTik.Location = new System.Drawing.Point(141, 51);
            this.lblDergimiTik.Name = "lblDergimiTik";
            this.lblDergimiTik.Size = new System.Drawing.Size(24, 13);
            this.lblDergimiTik.TabIndex = 15;
            this.lblDergimiTik.Text = "0 %";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(171, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "/";
            // 
            // lblImpotimiTik
            // 
            this.lblImpotimiTik.AutoSize = true;
            this.lblImpotimiTik.BackColor = System.Drawing.Color.Transparent;
            this.lblImpotimiTik.Location = new System.Drawing.Point(141, 24);
            this.lblImpotimiTik.Name = "lblImpotimiTik";
            this.lblImpotimiTik.Size = new System.Drawing.Size(24, 13);
            this.lblImpotimiTik.TabIndex = 15;
            this.lblImpotimiTik.Text = "0 %";
            // 
            // tmrCdoSekondDergimi
            // 
            this.tmrCdoSekondDergimi.Interval = 1000;
            this.tmrCdoSekondDergimi.Tick += new System.EventHandler(this.tmrCdoSekondDergimi_Tick);
            // 
            // btnImporto
            // 
            this.btnImporto.Location = new System.Drawing.Point(16, 86);
            this.btnImporto.Name = "btnImporto";
            this.btnImporto.Size = new System.Drawing.Size(51, 24);
            this.btnImporto.TabIndex = 21;
            this.btnImporto.Text = "Importo";
            this.btnImporto.UseVisualStyleBackColor = true;
            this.btnImporto.Click += new System.EventHandler(this.btnImporto_Click);
            // 
            // btnFZ
            // 
            this.btnFZ.Location = new System.Drawing.Point(309, 212);
            this.btnFZ.Name = "btnFZ";
            this.btnFZ.Size = new System.Drawing.Size(30, 24);
            this.btnFZ.TabIndex = 22;
            this.btnFZ.Text = "FZ";
            this.btnFZ.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(76, 51);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(269, 28);
            this.txtInfo.TabIndex = 23;
            // 
            // SinkronizimiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 380);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnFZ);
            this.Controls.Add(this.btnImporto);
            this.Controls.Add(this.grbKonfigurimet);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblKohaDerguar);
            this.Controls.Add(this.lblMesazhi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SinkronizimiForm";
            this.Text = "Sinkronizimi shenimeve";
            this.MaximizedBoundsChanged += new System.EventHandler(this.SinkronizimiForm_MaximizedBoundsChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SinkronizimiForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SinkronizimiForm_FormClosed);
            this.Load += new System.EventHandler(this.SinkronizimiForm_Load);
            this.Resize += new System.EventHandler(this.SinkronizimiForm_Resize);
            this.grbKonfigurimet.ResumeLayout(false);
            this.grbKonfigurimet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMesazhi;
        private System.Windows.Forms.Timer tmrCdoImportimi;
        private System.Windows.Forms.Label lblKohaDerguar;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grbKonfigurimet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDergimiSekonda;
        private System.Windows.Forms.Label lblImportimiSekonda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblImpotimiTik;
        private System.Windows.Forms.Label lblDergimiTik;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer tmrCdoSekondDergimi;
        private System.Windows.Forms.Button btnImporto;
        private System.Windows.Forms.Button btnFZ;
        private System.Windows.Forms.TextBox txtInfo;
    }
}

