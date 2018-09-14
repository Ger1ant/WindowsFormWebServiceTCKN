using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCKDogrula.TCKDogrulaServis;

namespace TCKDogrula
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }


        //Ad-Soyad - Rakam Alamaz 
        //TC-No ve Doğum-Yılı Harf Alamaz.
        #region Harf-Sayı Kontrolü

        private void txtTCK_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                                                  && !char.IsSeparator(e.KeyChar);
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                                                  && !char.IsSeparator(e.KeyChar);
        }

        private void txtDogumYil_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            KPSPublicSoapClient tckServis=new KPSPublicSoapClient();
            while (true)
            {
                try
                {
                    bool eslesme = tckServis.TCKimlikNoDogrula(Convert.ToInt64(txtTCK.Text), txtAd.Text.ToUpper().TrimEnd(),
                        txtSoyad.Text.ToUpper().TrimEnd(), Convert.ToInt32(txtDogumYil.Text));
                    lblOnay.Text = "";
                    lblHata.Text = "";

                    if (Convert.ToInt64(txtTCK.Text) % 2 != 0)
                    {
                        lblHata.Text = "TC No Tek Sayı ile Bitemez";
                        break;
                    }
                  
                    if (eslesme)
                    {
                        lblOnay.Text = "Kimlik Eşleşmesi Sağlandı";
                        break;
                    }

                    else
                    {
                        lblHata.Text = "Hata Eşleşme Sağlanamadı";
                        break;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hata Algılandı ! Lütfen Tekrar Deneyin", "UYARI", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                   break;

                }

            }
            
            

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Çıkış yapılsın mı ?", "Bilgilendirme",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (secenek == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        
    }
}
