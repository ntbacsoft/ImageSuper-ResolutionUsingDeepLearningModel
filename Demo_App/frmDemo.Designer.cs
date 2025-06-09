namespace TestImage
{
    partial class frmDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDemo));
            this.btnSend = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRespond = new System.Windows.Forms.Button();
            this.ClearImages = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.psnrTxt = new System.Windows.Forms.Label();
            this.ssimTxt = new System.Windows.Forms.Label();
            this.loadingGif = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingGif)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(184, 609);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(257, 78);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(60, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 446);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(31, 25);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(400, 400);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnRespond
            // 
            this.btnRespond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRespond.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespond.Location = new System.Drawing.Point(701, 605);
            this.btnRespond.Name = "btnRespond";
            this.btnRespond.Size = new System.Drawing.Size(257, 78);
            this.btnRespond.TabIndex = 4;
            this.btnRespond.Text = "Response";
            this.btnRespond.UseVisualStyleBackColor = true;
            this.btnRespond.Click += new System.EventHandler(this.btnRespond_Click);
            // 
            // ClearImages
            // 
            this.ClearImages.Dock = System.Windows.Forms.DockStyle.Right;
            this.ClearImages.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearImages.Location = new System.Drawing.Point(1094, 0);
            this.ClearImages.Name = "ClearImages";
            this.ClearImages.Size = new System.Drawing.Size(154, 848);
            this.ClearImages.TabIndex = 4;
            this.ClearImages.Text = "Clear ";
            this.ClearImages.UseVisualStyleBackColor = true;
            this.ClearImages.Click += new System.EventHandler(this.ClearImages_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(580, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(490, 446);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(44, 27);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(400, 400);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "Image Low Resolution";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(652, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image Super Resolution";
            // 
            // psnrTxt
            // 
            this.psnrTxt.AutoSize = true;
            this.psnrTxt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.psnrTxt.Location = new System.Drawing.Point(357, 553);
            this.psnrTxt.Name = "psnrTxt";
            this.psnrTxt.Size = new System.Drawing.Size(104, 32);
            this.psnrTxt.TabIndex = 7;
            this.psnrTxt.Text = "PSNR: ";
            // 
            // ssimTxt
            // 
            this.ssimTxt.AutoSize = true;
            this.ssimTxt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssimTxt.Location = new System.Drawing.Point(652, 553);
            this.ssimTxt.Name = "ssimTxt";
            this.ssimTxt.Size = new System.Drawing.Size(98, 32);
            this.ssimTxt.TabIndex = 8;
            this.ssimTxt.Text = "SSIM: ";
            // 
            // loadingGif
            // 
            this.loadingGif.Image = global::Demo_App.Properties.Resources.spinner_8565_256;
            this.loadingGif.Location = new System.Drawing.Point(476, 204);
            this.loadingGif.Name = "loadingGif";
            this.loadingGif.Size = new System.Drawing.Size(169, 152);
            this.loadingGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingGif.TabIndex = 9;
            this.loadingGif.TabStop = false;
            this.loadingGif.Visible = false;
            // 
            // frmDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1248, 848);
            this.Controls.Add(this.loadingGif);
            this.Controls.Add(this.ssimTxt);
            this.Controls.Add(this.psnrTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRespond);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.ClearImages);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1270, 904);
            this.MinimumSize = new System.Drawing.Size(1270, 904);
            this.Name = "frmDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NGHIÊN CỨU TĂNG CƯỜNG ĐỘ PHÂN GIẢI ẢNH, SỬ DỤNG MÔ HÌNH HỌC SÂU";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingGif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnRespond;
        private System.Windows.Forms.Button ClearImages;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label psnrTxt;
        private System.Windows.Forms.Label ssimTxt;
        private System.Windows.Forms.PictureBox loadingGif;
    }
}

