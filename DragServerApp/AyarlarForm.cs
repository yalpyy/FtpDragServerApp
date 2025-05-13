using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace DragServerApp
{
    public partial class AyarlarForm : Form
    {
        private bool kilitAcik = false; // Form kilit durumu
        private ToolTip toolTip; // Tooltip nesnesi

        public AyarlarForm()
        {
            InitializeComponent();
            LoadSettings();
            SetControlsEnabled(false); // Form ilk açıldığında kilitli olsun
            toolTip = new ToolTip();

            // PictureBox üzerine fare ile gelindiğinde tooltip gösterme
            toolTip.SetToolTip(pictureBox1, "Örnek IP Girişi:\n127.0.0.1\nÖrnek Sub IP Girişi:\n127.0.0.1/10");

            // Tooltip süresi ve diğer ayarları isteğe göre özelleştirebilirsiniz
            toolTip.AutoPopDelay = 5000;  // Tooltip'in 5 saniye boyunca gösterilmesini sağlar
            toolTip.InitialDelay = 100;  // Tooltip'in görünmeden önceki gecikme (ms)
            toolTip.ReshowDelay = 500;    // Tooltip'in tekrar görünmeden önceki gecikme (ms)
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists("ftpconfig.txt"))
                {
                    string[] lines = File.ReadAllLines("ftpconfig.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(new[] { ':' }, 2);
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim().ToLower();
                            string value = parts[1].Trim();

                            if (key.Contains("ftp"))
                                txtHost.Text = value;
                            else if (key.Contains("kullanıcı"))
                                txtUsername.Text = value;
                            else if (key.Contains("şifre"))
                            {
                                try
                                {
                                    byte[] data = Convert.FromBase64String(value);
                                    txtPassword.Text = Encoding.UTF8.GetString(data);
                                }
                                catch
                                {
                                    txtPassword.Text = ""; // Hatalı şifre çözümü
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Ayarları yüklerken hata oluştu: " + ex.Message);
            }
        }



        private void SetControlsEnabled(bool enabled)
        {
            txtHost.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnKaydet.Enabled = enabled;
        }

        private void btnKilit_Click(object sender, EventArgs e)
        {
            kilitAcik = !kilitAcik; // Durumu tersine çevir
            SetControlsEnabled(kilitAcik); // Kontrolleri aktif/pasif yap
            btnKilit.Text = kilitAcik ? "🔐 Kilidi Kapat" : "🔒 Kilidi Aç"; // Buton metni değiştir
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                string host = txtHost.Text.Trim();

                // ftp:// yoksa başa ekle
                if (!host.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                    host = "ftp://" + host;

                // Sonunda / yoksa sona ekle
                if (!host.EndsWith("/"))
                    host += "/";

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Şifreyi base64 formatında kodla
                string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                string[] configLines =
                {
            $"FtpAdresi:{host}",
            $"KullanıcıAdı:{username}",
            $"Şifre:{encodedPassword}"
        };

                File.WriteAllLines("ftpconfig.txt", configLines);

                MessageBox.Show("✅ FTP bilgileri kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Kaydetme sırasında hata oluştu: " + ex.Message);
            }
        }


    }
}
