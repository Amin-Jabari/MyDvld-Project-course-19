namespace DVLD.license
{
    partial class frmShowDriverLicenseHistory
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDriverLicense1 = new DVLD.license.Controls.ctrlDriverLicense();
            this.ctrlPersonCard1 = new DVLD.Controls.ctrlPersonCard();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(103, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(865, 52);
            this.lblTitle.TabIndex = 130;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(12, 126);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(220, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 131;
            this.pbPersonImage.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(946, 742);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 134;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDriverLicense1
            // 
            this.ctrlDriverLicense1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlDriverLicense1.AutoSize = true;
            this.ctrlDriverLicense1.Location = new System.Drawing.Point(12, 394);
            this.ctrlDriverLicense1.Name = "ctrlDriverLicense1";
            this.ctrlDriverLicense1.Size = new System.Drawing.Size(1074, 342);
            this.ctrlDriverLicense1.TabIndex = 133;
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.AutoSize = true;
            this.ctrlPersonCard1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonCard1.Location = new System.Drawing.Point(242, 86);
            this.ctrlPersonCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(839, 300);
            this.ctrlPersonCard1.TabIndex = 132;
            // 
            // frmDriverLicenseHistory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1098, 782);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDriverLicense1);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.lblTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDriverLicenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDriverLicenseHistory";
            this.Load += new System.EventHandler(this.frmDriverLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private DVLD.Controls.ctrlPersonCard ctrlPersonCard1;
        private Controls.ctrlDriverLicense ctrlDriverLicense1;
        private System.Windows.Forms.Button btnClose;
    }
}