using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics; // En üste ekle

namespace DragServerApp
{
    public partial class Form1 : Form
    {
        private List<(string filePath, string fileName)> uploadQueue = new List<(string, string)>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dropPanel.AllowDrop = true;
            dropPanel.DragEnter += new DragEventHandler(Form1_DragEnter);
            dropPanel.DragDrop += new DragEventHandler(Form1_DragDrop);

            btnOnayla.Visible = false;
            btnOnayla.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Boş bırakılabilir
        }

        private void sunucuBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AyarlarForm form = new AyarlarForm();
            form.ShowDialog();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private string PromptForFilenameChange(string originalName)
        {
            string extension = Path.GetExtension(originalName);
            string originalNameWithoutExtension = Path.GetFileNameWithoutExtension(originalName);

            DialogResult result = MessageBox.Show(
                $"'{originalName}' dosya adını değiştirmek ister misiniz?",
                "Dosya Adı", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string input = Interaction.InputBox(
                    $"Yeni dosya adını giriniz (.{extension.TrimStart('.')} uzantısı sabittir):",
                    "Dosya Adı Değiştir", originalNameWithoutExtension);

                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Uzantıyı koruyarak yeni dosya adını oluştur
                    return input + extension;
                }
            }

            return originalName;
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            uploadQueue.Clear();

            foreach (string file in files)
            {
                if (file.EndsWith(".mov", StringComparison.OrdinalIgnoreCase) ||
                    file.EndsWith(".mxf", StringComparison.OrdinalIgnoreCase))
                {
                    string newName = PromptForFilenameChange(Path.GetFileName(file));
                    uploadQueue.Add((file, newName));
                    txtLogs.AppendText($"📂 '{newName}' sıraya eklendi.\r\n");
               
                    txtname.AppendText($"▶️ Dosya İsmi : '{newName}' ...\r\n");
                }
                else
                {
                    txtLogs.AppendText("❌ Sadece .mov veya .mxf uzantılı dosyalar yüklenebilir.\r\n");
                }
            }

            if (uploadQueue.Count > 0)
            {
                btnOnayla.Text = "Yüklemeyi Başlat";
                btnOnayla.Visible = true;
                btnOnayla.Enabled = true;
            }
        }

        private async void btnOnayla_Click(object sender, EventArgs e)
        {
            btnOnayla.Enabled = false;
            btnOnayla.Text = "Yükleniyor...";

            foreach (var item in uploadQueue)
            {
                await UploadFileToFTP(item.filePath, item.fileName);
            }

            btnOnayla.Text = "Yükleme Tamamlandı";
            uploadQueue.Clear();
        }

        int remainingSeconds = 0; // Form seviyesinde tanımlanmalı


        private async Task UploadFileToFTP(string filePath, string fileName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string ftpHost = "ftp://";
            string ftpUser = "";
            string ftpPassword = "";

            try
            {
                if (File.Exists("ftpconfig.txt"))
                {
                    string[] lines = File.ReadAllLines("ftpconfig.txt");
                    if (lines.Length >= 3)
                    {
                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(new[] { ':' }, 2);
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim().ToLower();
                                string value = parts[1].Trim();

                                if (key.Contains("adres") || key.Contains("host"))
                                    ftpHost = value;
                                else if (key.Contains("kullanıcı") || key.Contains("user"))
                                    ftpUser = value;
                                else if (key.Contains("şifre") || key.Contains("password"))
                                {
                                    try
                                    {
                                        byte[] decodedBytes = Convert.FromBase64String(value);
                                        ftpPassword = Encoding.UTF8.GetString(decodedBytes);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Şifreyi çözme sırasında hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ftpconfig.txt dosyası eksik veya hatalı. Varsayılan bilgiler kullanılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("ftpconfig.txt dosyası bulunamadı. Varsayılan bilgiler kullanılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayar dosyası okunurken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ftpUrl = ftpHost.EndsWith("/") ? ftpHost + fileName : ftpHost + "/" + fileName;

            try
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DefaultConnectionLimit = 10;

                // Aynı isimde dosya kontrolü
                FtpWebRequest checkRequest = (FtpWebRequest)WebRequest.Create(ftpUrl);
                checkRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                checkRequest.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                checkRequest.UsePassive = true;
                checkRequest.UseBinary = true;
                checkRequest.KeepAlive = false;

                try
                {
                    using (FtpWebResponse response = (FtpWebResponse)await checkRequest.GetResponseAsync())
                    {
                        MessageBox.Show("FTP sunucusunda aynı isimde bir dosya zaten mevcut!", "Dosya Zaten Var", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLogs.AppendText($"⚠️ '{fileName}' zaten sunucuda mevcut. Yükleme iptal edildi.\r\n");
                        return;
                    }
                }
                catch (WebException ex)
                {
                    if (((FtpWebResponse)ex.Response)?.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        MessageBox.Show($"FTP kontrol sırasında hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                FileInfo fileInfo = new FileInfo(filePath);
                long totalLength = fileInfo.Length;
                int bufferSize = 1024 * 1024 * 4; // 4 MB
                long testLimit = 1024L * 1024L * 300L; // 300 MB

                txtLogs.AppendText($"📂 Dosya İsmi : '{fileName}' \r\n");
                txtLogs.AppendText($"▶️ '{fileName}' yükleniyor...\r\n");

                using (FileStream fileStream = File.OpenRead(filePath))
                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    long totalRead = 0;
                    long testRead = 0;
                    Stopwatch testWatch = new Stopwatch();
                    bool testCompleted = false;

                    testWatch.Start();
                    int lastProgress = 0;

                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await requestStream.WriteAsync(buffer, 0, bytesRead);
                        totalRead += bytesRead;

                        // Hız testi ve süre tahmini
                        if (!testCompleted)
                        {
                            testRead += bytesRead;
                            if (testRead >= testLimit)
                            {
                                testWatch.Stop();
                                double testSeconds = testWatch.Elapsed.TotalSeconds;
                                double speedMBps = (testRead / (1024.0 * 1024.0)) / testSeconds;
                                double estimatedTotalSeconds = (totalLength / (1024.0 * 1024.0)) / speedMBps;

                                int estMin = (int)(estimatedTotalSeconds / 60);
                                int estSec = (int)(estimatedTotalSeconds % 60);

                                txtname.AppendText($"🧠 Tahmini süre: {estMin} dakika {estSec} saniye\r\n");

                                remainingSeconds = (int)Math.Round(estimatedTotalSeconds);
                                estimatedTotalSecondsGlobal = remainingSeconds;
                                uploadStopwatch.Restart(); // ⏱ başlat
                                timer1.Interval = 1000;
                                timer1.Start();
                                label1.Text = $"⏳ Kalan süre: {estMin:D2}:{estSec:D2}";

                                testCompleted = true;
                            }
                        }

                        int progress = (int)(totalRead * 100 / totalLength);
                        progressBar1.Value = Math.Min(progress, 100);

                        if (progress >= lastProgress + 10)
                        {
                            txtLogs.AppendText($"📦 %{progress} tamamlandı...\r\n");
                            lastProgress = progress;
                        }
                    }
                }

                stopwatch.Stop();
                timer1.Stop();
                label1.Text = "✅ Yükleme tamamlandı.";

                double totalSeconds = stopwatch.Elapsed.TotalSeconds;
                double transferredMB = new FileInfo(filePath).Length / (1024.0 * 1024.0);
                double avgSpeed = transferredMB / totalSeconds;

                txtLogs.AppendText($"✅ Aktarım tamamlandı. Süre: {totalSeconds:F2} saniye\r\n");
            }
            catch (Exception ex)
            {
                timer1.Stop();
                label1.Text = "❌ Yükleme başarısız.";
                MessageBox.Show($"Dosya yüklenirken bir hata oluştu:\n{ex.Message}", "Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogs.AppendText("❌ Yükleme başarısız.\r\n");
            }
        }
       
        private int estimatedTotalSecondsGlobal = 0;
        private Stopwatch uploadStopwatch = new Stopwatch();

        private void timer1_Tick(object sender, EventArgs e)
        {
            int elapsed = (int)uploadStopwatch.Elapsed.TotalSeconds;
            int remaining = estimatedTotalSecondsGlobal - elapsed;

            if (remaining >= 0)
            {
                int minutes = remaining / 60;
                int seconds = remaining % 60;
                label1.Text = $"⏳ Kalan süre: {minutes:D2}:{seconds:D2}";
            }
            else
            {
                timer1.Stop();
                label1.Text = "✅ Yükleme tamamlandı.";
            }
        }



        private async void dosyaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Yüklenecek dosyayı seçin";
                openFileDialog.Filter = "Video Dosyaları (*.mov;*.mxf)|*.mov;*.mxf";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    // Uzantıyı kontrol etmek için ekstra güvenlik önlemi (filtreye rağmen)
                    string extension = Path.GetExtension(filePath).ToLower();
                    if (extension != ".mov" && extension != ".mxf")
                    {
                        MessageBox.Show("Sadece .mov veya .mxf uzantılı dosyalar yüklenebilir.", "Geçersiz Dosya", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await UploadFileToFTP(filePath, fileName);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
