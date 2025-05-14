namespace RM.Visualize
{
    partial class frmChooseDate
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
            this.label1 = new System.Windows.Forms.Label();
            this.sDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.eDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnVisual = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Date:";
            // 
            // sDate
            // 
            this.sDate.Checked = true;
            this.sDate.FillColor = System.Drawing.Color.White;
            this.sDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.sDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.sDate.Location = new System.Drawing.Point(292, 80);
            this.sDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.sDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.sDate.Name = "sDate";
            this.sDate.Size = new System.Drawing.Size(200, 36);
            this.sDate.TabIndex = 1;
            this.sDate.Value = new System.DateTime(2025, 5, 13, 11, 40, 0, 455);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "End Date";
            // 
            // eDate
            // 
            this.eDate.Checked = true;
            this.eDate.FillColor = System.Drawing.Color.White;
            this.eDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.eDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.eDate.Location = new System.Drawing.Point(292, 167);
            this.eDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.eDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.eDate.Name = "eDate";
            this.eDate.Size = new System.Drawing.Size(200, 36);
            this.eDate.TabIndex = 1;
            this.eDate.Value = new System.DateTime(2025, 5, 13, 11, 40, 0, 455);
            // 
            // btnVisual
            // 
            this.btnVisual.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVisual.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVisual.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVisual.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVisual.FillColor = System.Drawing.Color.Black;
            this.btnVisual.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnVisual.ForeColor = System.Drawing.Color.White;
            this.btnVisual.Location = new System.Drawing.Point(84, 280);
            this.btnVisual.Name = "btnVisual";
            this.btnVisual.Size = new System.Drawing.Size(180, 45);
            this.btnVisual.TabIndex = 2;
            this.btnVisual.Text = "Visualize";
            this.btnVisual.Click += new System.EventHandler(this.btnVisual_Click);
            // 
            // frmChooseDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 380);
            this.Controls.Add(this.btnVisual);
            this.Controls.Add(this.eDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sDate);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmChooseDate";
            this.Text = "frmChooseDate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker sDate;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2DateTimePicker eDate;
        private Guna.UI2.WinForms.Guna2Button btnVisual;
    }
}