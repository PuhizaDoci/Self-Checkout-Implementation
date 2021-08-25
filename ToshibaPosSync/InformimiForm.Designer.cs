namespace ToshibaPosSinkronizimi
{
    partial class InformimiForm
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
            this.txtInformimi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInformimi
            // 
            this.txtInformimi.Location = new System.Drawing.Point(40, 34);
            this.txtInformimi.Multiline = true;
            this.txtInformimi.Name = "txtInformimi";
            this.txtInformimi.Size = new System.Drawing.Size(300, 98);
            this.txtInformimi.TabIndex = 0;
            // 
            // InformimiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 170);
            this.Controls.Add(this.txtInformimi);
            this.Name = "InformimiForm";
            this.Text = "InformimiForm";
            this.Load += new System.EventHandler(this.InformimiForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtInformimi;
    }
}