using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KisilerKura
{
    public partial class Form1 : Form
    {
        List<string> kisiler;

        // "Ali" , "Burak" , "Hande" , "Ömer" , "Aysegül" , "Ezgi" , "Seda" , "Ugurcan" , "Kudret" , "Arzu" , "Koray" , "Bilge" , "Bilgehan" , "Serkan" , "Emre" , "Tugba"
        public Form1()
        {
            InitializeComponent();
            VerileriOku();//başlarken çıkışta kaydettiği .txt den verileri çektik
            KisileriListele();//mevcut liste vasra .txt içinde onları göstermesi için
        }

        private void VerileriOku()
        {
            try
            {
                string[] dizi = File.ReadAllLines("veri.txt");//.txt dosyasından listeyi çekmesi için
                kisiler = dizi.ToList();//kisiler.AddRange(dizi); dosyadan gelen diziyi listeye attık
            }
            catch (Exception)
            {

                kisiler = new List<string>();//olurda .txt dosyası silinmişse diye boş liste oluşturup bunu kullandırdık
            }
        }

        private void KisileriListele()
        {
            lstKisiler.Items.Clear();//alt alta eklemesin diye temizleyip listeyi tekrar yazdırıyoruz
            foreach (string kisi in kisiler)
            {
                lstKisiler.Items.Add(kisi);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();

            if (ad == string.Empty)//eğer kutu boşsa bir şey yapma eventi bitir demek için
                return;
            kisiler.Add(ad);
            txtAd.Clear();//ekleme bitince kutuyu temizlemesi için
            KisileriListele();//gösterilen listeyi anlık ekrana yansıtmak için yeniledik
            txtAd.Focus();//mouse u bu controlün üstüne çeker(tekrar yazınca direk algılaması için)
        }

        private void btnSirala_Click(object sender, EventArgs e)
        {
            //lstKisiler.Sorted = true;

            kisiler.Sort();
            KisileriListele();
        }

        private void btnKaristir_Click(object sender, EventArgs e)
        {
            //ListBox.ObjectCollection list = lstKisiler.Items;
            //Random rng = new Random();
            //int n = list.Count;
            //while (n > 1)
            //{
            //    n--;
            //    int k = rnd.Next(n + 1);
            //    string value = (string)list[k];
            //    list[k] = list[n];
            //    list[n] = value;
            //}


            Random rnd = new Random();
            int talihli;
            string temp;
            //random bir indexi, 0. indekse yerleştirdik ve böyle devam ederek random indexleri alıp 0,1,2,3,4.. şeklinde indexkere koyarak yenidedn dizdik(burda alttaki rnd sınırlarını belirlemek asıl olay)
            //Bu sınırları da örneğin 0. indekse atama yapacaksak 0. indeks harici bir index numarası oluşturarak yaptık
            for (int i = 0; i < kisiler.Count - 1; i++)
            {
                talihli = rnd.Next(i + 1, kisiler.Count);//(i+1 den eleman adedi kadar çünkü 0. index hedef index onu almadık eleman.Count çünkü random son sayısı almaz böylece max indexe kadar sayi üretir)
                temp = kisiler[i];
                kisiler[i] = kisiler[talihli];
                kisiler[talihli] = temp;
            }
            KisileriListele();


            lblSansli.Text = kisiler[0];

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Form kapanırken çalışan event (Closed eventi ise kapandıktan sonra çalışır)
        {
            File.WriteAllLines("veri.txt", kisiler);//projenin bulunduğu klasör => bin => debug klasörü altına kaydeder -- listedeki elemanları .txt formatlı içine yazdırdık
        }

        private void txtAd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;//tuşa basınca çıkan sesi kapatır(dlılıng sesi)
                btnEkle.PerformClick();//tuşa basma eventini çalıştırır
            }
        }

        private void lstKisiler_KeyDown(object sender, KeyEventArgs e)
        {
            int sid = lstKisiler.SelectedIndex;

            if (e.KeyCode == Keys.Delete && sid > -1)//hiçbiri seçili değilken hata vermemesi için(hiç biri seçili değilken index -1 olur)
            {
                kisiler.RemoveAt(sid);//ListBox.SelectedIndex seçili elemanın indexini int olarak döndürür, RemoveAt(int) bununla da bu indexi siliyoruz
                KisileriListele();//silinen halini tekrar listeletip ekrandan kaybolmasını sağladık

                if (sid < kisiler.Count)
                    lstKisiler.SelectedIndex = sid;
                else
                    lstKisiler.SelectedIndex = sid - 1;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            int sid = lstKisiler.SelectedIndex;
            if (sid > 0)
            {
                Tasi(sid,0);
                //string temp = kisiler[sid];
                //kisiler.RemoveAt(sid);
                //kisiler.Insert(0, temp);
                //KisileriListele();
                //lstKisiler.SelectedIndex = 0;
                //lstKisiler.Focus();
            }

        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            int sid = lstKisiler.SelectedIndex;
            if (sid < kisiler.Count - 1 && sid > -1)
            {
                Tasi(sid,kisiler.Count-1);
                //string temp = kisiler[sid];
                //kisiler.RemoveAt(sid);
                //kisiler.Insert(kisiler.Count,temp);
                //KisileriListele();
                //lstKisiler.SelectedIndex = kisiler.Count - 1;
                //lstKisiler.Focus();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int sid = lstKisiler.SelectedIndex;
            if (sid > 0)
            {
                Tasi(sid,sid-1);
                //string temp = kisiler[sid];
                //kisiler.RemoveAt(sid);
                //kisiler.Insert(sid - 1, temp);
                //KisileriListele();
                //lstKisiler.SelectedIndex = sid - 1;
                //lstKisiler.Focus();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int sid = lstKisiler.SelectedIndex;
            if (sid > -1 && sid < kisiler.Count - 1)
            {
                Tasi(sid,sid+1);
                //string temp = kisiler[sid];
                //kisiler.RemoveAt(sid);
                //kisiler.Insert(sid + 1, temp);
                //KisileriListele();
                //lstKisiler.SelectedIndex = sid + 1;
                //lstKisiler.Focus();
            }
        }
        void Tasi(int kaynakIndeks,int hedefIndex)//aşağı yukarı en üst en alt methodları benzer işlem yaptığı için refactoring ile daha temiz gözükmesini sağladık
        {
            
            string temp = kisiler[kaynakIndeks];
            kisiler.RemoveAt(kaynakIndeks);
            kisiler.Insert(hedefIndex, temp);
            KisileriListele();
            lstKisiler.SelectedIndex = hedefIndex;
            lstKisiler.Focus();
        }

        
    }
}
