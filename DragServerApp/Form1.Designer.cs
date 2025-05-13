namespace DragServerApp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnOnayla = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sunucuBilgileriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtname = new System.Windows.Forms.TextBox();
            this.dropPanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.SpringGreen;
            this.progressBar1.Location = new System.Drawing.Point(2, 541);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(427, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // txtLogs
            // 
            this.txtLogs.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogs.Location = new System.Drawing.Point(2, 424);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(427, 111);
            this.txtLogs.TabIndex = 2;
            // 
            // btnOnayla
            // 
            this.btnOnayla.Enabled = false;
            this.btnOnayla.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnayla.Location = new System.Drawing.Point(136, 281);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(151, 29);
            this.btnOnayla.TabIndex = 3;
            this.btnOnayla.Text = "Yüklemeyi Başlat";
            this.btnOnayla.UseVisualStyleBackColor = true;
            this.btnOnayla.Click += new System.EventHandler(this.btnOnayla_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sunucuBilgileriToolStripMenuItem,
            this.dosyaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(435, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sunucuBilgileriToolStripMenuItem
            // 
            this.sunucuBilgileriToolStripMenuItem.Name = "sunucuBilgileriToolStripMenuItem";
            this.sunucuBilgileriToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.sunucuBilgileriToolStripMenuItem.Text = "Sunucu Bilgileri";
            this.sunucuBilgileriToolStripMenuItem.Click += new System.EventHandler(this.sunucuBilgileriToolStripMenuItem_Click);
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya İçeri Aktar";
            this.dosyaToolStripMenuItem.Click += new System.EventHandler(this.dosyaToolStripMenuItem_Click);
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.Location = new System.Drawing.Point(0, 341);
            this.txtname.Multiline = true;
            this.txtname.Name = "txtname";
            this.txtname.ReadOnly = true;
            this.txtname.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtname.Size = new System.Drawing.Size(427, 77);
            this.txtname.TabIndex = 5;
            // 
            // dropPanel
            // 
            this.dropPanel.AllowDrop = true;
            this.dropPanel.BackColor = System.Drawing.SystemColors.Info;
            this.dropPanel.BackgroundImage = global::DragServerApp.Properties.Resources.eeeeee;
            this.dropPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dropPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dropPanel.Location = new System.Drawing.Point(26, 107);
            this.dropPanel.Name = "dropPanel";
            this.dropPanel.Size = new System.Drawing.Size(373, 168);
            this.dropPanel.TabIndex = 0;
            this.dropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.dropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.dropPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.dropPanel.DoubleClick += new System.EventHandler(this.dosyaToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "Süre:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DragServerApp.Properties.Resources.image__1_;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Cursor = System.Windows.Forms.Cursors.No;
            this.panel1.Location = new System.Drawing.Point(77, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 65);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.btnOnayla);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dropPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FTP_Playout_Uploader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel dropPanel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Button btnOnayla;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sunucuBilgileriToolStripMenuItem;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}

