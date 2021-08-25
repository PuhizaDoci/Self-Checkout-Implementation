namespace ToshibaPOS
{
    partial class ZgjedhjetForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PershkrimitextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OKbutton = new System.Windows.Forms.Button();
            this.Anulobutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // PershkrimitextBox
            // 
            this.PershkrimitextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PershkrimitextBox.BackColor = System.Drawing.SystemColors.Info;
            this.PershkrimitextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PershkrimitextBox.Location = new System.Drawing.Point(12, 16);
            this.PershkrimitextBox.Name = "PershkrimitextBox";
            this.PershkrimitextBox.Size = new System.Drawing.Size(494, 20);
            this.PershkrimitextBox.TabIndex = 0;
            this.PershkrimitextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.PershkrimitextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PershkrimitextBox_KeyDown);
            this.PershkrimitextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PershkrimitextBox_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(494, 316);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // OKbutton
            // 
            this.OKbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKbutton.Location = new System.Drawing.Point(432, 364);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 2;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // Anulobutton
            // 
            this.Anulobutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Anulobutton.Location = new System.Drawing.Point(351, 364);
            this.Anulobutton.Name = "Anulobutton";
            this.Anulobutton.Size = new System.Drawing.Size(75, 23);
            this.Anulobutton.TabIndex = 3;
            this.Anulobutton.Text = "Anulo";
            this.Anulobutton.UseVisualStyleBackColor = true;
            this.Anulobutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ZgjedhjetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 394);
            this.ControlBox = false;
            this.Controls.Add(this.Anulobutton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.PershkrimitextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZgjedhjetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZgjedhjetForm";
            this.Load += new System.EventHandler(this.ZgjedhjetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PershkrimitextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button Anulobutton;
    }
}