namespace ArcheryGUI
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
            this.txtarcher = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picresult = new System.Windows.Forms.PictureBox();
            this.btngrafik = new System.Windows.Forms.Button();
            this.lbltime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picresult)).BeginInit();
            this.SuspendLayout();
            // 
            // txtarcher
            // 
            this.txtarcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtarcher.Location = new System.Drawing.Point(142, 21);
            this.txtarcher.Name = "txtarcher";
            this.txtarcher.Size = new System.Drawing.Size(153, 30);
            this.txtarcher.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sporcu İsmi: ";
            // 
            // picresult
            // 
            this.picresult.Location = new System.Drawing.Point(17, 117);
            this.picresult.Name = "picresult";
            this.picresult.Size = new System.Drawing.Size(850, 658);
            this.picresult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picresult.TabIndex = 2;
            this.picresult.TabStop = false;
            // 
            // btngrafik
            // 
            this.btngrafik.Location = new System.Drawing.Point(62, 71);
            this.btngrafik.Name = "btngrafik";
            this.btngrafik.Size = new System.Drawing.Size(211, 40);
            this.btngrafik.TabIndex = 3;
            this.btngrafik.Text = "Grafik Oluştur";
            this.btngrafik.UseVisualStyleBackColor = true;
            this.btngrafik.Click += new System.EventHandler(this.btngrafik_Click);
            // 
            // lbltime
            // 
            this.lbltime.AutoSize = true;
            this.lbltime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbltime.Location = new System.Drawing.Point(614, 24);
            this.lbltime.Name = "lbltime";
            this.lbltime.Size = new System.Drawing.Size(0, 25);
            this.lbltime.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(888, 787);
            this.Controls.Add(this.lbltime);
            this.Controls.Add(this.btngrafik);
            this.Controls.Add(this.picresult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtarcher);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archery Data";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picresult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtarcher;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picresult;
        private System.Windows.Forms.Button btngrafik;
        private System.Windows.Forms.Label lbltime;
    }
}

