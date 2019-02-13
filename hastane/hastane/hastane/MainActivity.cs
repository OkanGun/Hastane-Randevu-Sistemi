using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Collections.Specialized;
using Org.Json;
using System.Collections;
using BarChart;
using static Android.Provider.ContactsContract.Contacts;
using Android.Graphics;

namespace hastane
{
    [Activity(Label = "Hastane", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity        
    {
        public Button giris, kayit, kkayitol, rndval, rndvsrgla, rndviptal, ronayla, grgt1, grgt2, grgt3, anasyf;
        public EditText tcno, dgmyl, kad,ktc, ksoyad, kdgmyl, kdgmyr;
        public TextView rtc,rad,rsoyad, rdgmyl, btc, bad, bsoyad, bdgm, bplk, btarih, bdktr, bsaat;
        public Spinner plklnk,tarih, doktor,saat;
        public String rplklnk,rtarih,rdoktor,rsaat,rrdoktor;
        public ListView rdktrlst;
        String qwe, Id, ad, soyad, dogumyili, dogumyeri, tc,deger;
        ArrayList clocks = new ArrayList();       
        ArrayList doktorlist = new ArrayList();
        ArrayList saatanaliz = new ArrayList();
        BarModel[] data;
        BarModel[] data2;
        float s1, s2, s3, s4, s5, s6,p1,p2,p3,p4,p5;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            tcno = FindViewById<EditText>(Resource.Id.tcNo);
            dgmyl = FindViewById<EditText>(Resource.Id.dgmyl);
            giris = FindViewById<Button>(Resource.Id.giris);
            kayit = FindViewById<Button>(Resource.Id.kayit);
            giris.Click += Giris_Click;
            kayit.Click += Kayit_Click;
            
        }
        private void Giris_Click(object sender, System.EventArgs e)
        {
            hastagirisAsync n = new hastagirisAsync(this);
            n.Execute();
            saatanalizAsync k = new saatanalizAsync(this);
            k.Execute();
            polkilinikanalizAsync t = new polkilinikanalizAsync(this);
            t.Execute();
            //giriş kontrol sorgusu yazılacak
            if (qwe == "succes") { 
            SetContentView(Resource.Layout.anasayfa);
            Window.SetTitle("Anasayfa");
            rndval = FindViewById<Button>(Resource.Id.rndval);
            rndvsrgla = FindViewById<Button>(Resource.Id.rndvsrgla);
            rndviptal = FindViewById<Button>(Resource.Id.rndviptal);
            Toast.MakeText(ApplicationContext, "Hosgeldiniz" + " " + ad + " " + soyad, ToastLength.Short).Show();

             rndval.Click += Rndval_Click;
            rndvsrgla.Click += Rndvsrgla_Click;
            rndviptal.Click += Rndviptal_Click;

            }
        }   
        private void Rndval_Click(object sender, System.EventArgs e)
        {
            //randevu alma ekranı gelecek ve randevu alma işlemi yapılacak
            SetContentView(Resource.Layout.rndval);
            Window.SetTitle("Randevu Oluştur");
            rtc = FindViewById<TextView>(Resource.Id.rtc);
            rad = FindViewById<TextView>(Resource.Id.rad);
            rsoyad = FindViewById<TextView>(Resource.Id.rsoyad);
            rdgmyl = FindViewById<TextView>(Resource.Id.rdgmyl);
            plklnk = FindViewById<Spinner>(Resource.Id.spinner);
            tarih = FindViewById<Spinner>(Resource.Id.spinnerr);
            saat = FindViewById<Spinner>(Resource.Id.spinnerrrr);
            doktor = FindViewById<Spinner>(Resource.Id.spinnerrr);
            ronayla = FindViewById<Button>(Resource.Id.ronayla);
            rtc.Text = tc;
            rad.Text = ad;
            rsoyad.Text = soyad;
            rdgmyl.Text = dogumyili;            
            doktor.Visibility = ViewStates.Invisible;
            tarih.Visibility = ViewStates.Invisible;
            saat.Visibility = ViewStates.Invisible;
            ronayla.Visibility = ViewStates.Invisible;
            ronayla.Click += Ronayla_Click;
            
            plklnk.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Plklnk_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
               this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);        
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            plklnk.Adapter = adapter;                 
                                                
        }
        private void Ronayla_Click(object sender, EventArgs e)
        {
            ronaylaAsync s = new ronaylaAsync(this);
            s.Execute();
            SetContentView(Resource.Layout.rbilgi);
            btc = FindViewById<TextView>(Resource.Id.btc);
            bad = FindViewById<TextView>(Resource.Id.bad);
            bsoyad = FindViewById<TextView>(Resource.Id.bsoyad);
            bdgm = FindViewById<TextView>(Resource.Id.bdgmyl);
            bplk = FindViewById<TextView>(Resource.Id.bplklnk);
            btarih = FindViewById<TextView>(Resource.Id.btarih);
            bdktr = FindViewById<TextView>(Resource.Id.bdoktor);
            bsaat = FindViewById<TextView>(Resource.Id.bsaat);
            anasyf = FindViewById<Button>(Resource.Id.anasyf);
            anasyf.Click += Anasyf_Click;
            btc.Text = tc;
            bad.Text = ad;
            bsoyad.Text = soyad;
            bdgm.Text = dogumyili;
            bplk.Text = rplklnk;
            btarih.Text = rtarih;
            bdktr.Text = rdoktor;
            bsaat.Text = rsaat;
            Window.SetTitle("Randevu Bilgi");
        }
        private void Anasyf_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.anasayfa);
            Window.SetTitle("Anasayfa");
            rndval = FindViewById<Button>(Resource.Id.rndval);
            rndvsrgla = FindViewById<Button>(Resource.Id.rndvsrgla);
            rndviptal = FindViewById<Button>(Resource.Id.rndviptal);
            rndval.Click += Rndval_Click;
            rndvsrgla.Click += Rndvsrgla_Click;
            rndviptal.Click += Rndviptal_Click;
        }
        private void Saat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           
            Spinner spinner = (Spinner)sender;
            rsaat = spinner.GetItemAtPosition(e.Position).ToString();
            if (rsaat == "Saat Seçiniz")
            {
                ronayla.Visibility = ViewStates.Invisible;
            }
            else
            {
                ronayla.Visibility = ViewStates.Visible;
                
            }
        }
        private void Doktor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            rdoktor = spinner.GetItemAtPosition(e.Position).ToString();
            saatlistAsync k = new saatlistAsync(this);
            k.Execute();

            //saat seçimi
            if (rdoktor == "Doktor Seçiniz")
            {
                saat.Visibility = ViewStates.Invisible;
                ronayla.Visibility = ViewStates.Invisible;
                rdoktor = spinner.GetItemAtPosition(e.Position).ToString();
            }
            else
            {
               
                rdoktor = spinner.GetItemAtPosition(e.Position).ToString();
                
                var adapter4 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, clocks);
                adapter4.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                saat.Adapter = adapter4;
                saat.Visibility = ViewStates.Visible;
                saat.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Saat_ItemSelected);
                saatlistAsync n = new saatlistAsync(this);
                n.Execute();
            }
            rdoktor = spinner.GetItemAtPosition(e.Position).ToString();
        }
        private void Plklnk_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           
            Spinner spinner = (Spinner)sender;           
            rplklnk = spinner.GetItemAtPosition(e.Position).ToString();
            if (rplklnk == "Poliklinik Seçiniz")
            {
                doktor.Visibility = ViewStates.Invisible;
                tarih.Visibility = ViewStates.Invisible;                
                saat.Visibility = ViewStates.Invisible;
                ronayla.Visibility = ViewStates.Invisible;
            }
            else {
                
                tarih.Visibility = ViewStates.Visible;
                tarih.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Tarih_ItemSelected);
                var adapter2 = ArrayAdapter.CreateFromResource(
                   this, Resource.Array.planets_arrayy, Android.Resource.Layout.SimpleSpinnerItem);
                adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                tarih.Adapter = adapter2;
            }                   
        }
        private void Tarih_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {            
            Spinner spinner = (Spinner)sender;            
            rtarih = spinner.GetItemAtPosition(e.Position).ToString();
            if (rtarih == "Tarih Seçiniz")
            {
                doktor.Visibility = ViewStates.Invisible;
                saat.Visibility = ViewStates.Invisible;
                ronayla.Visibility = ViewStates.Invisible;
                rtarih = spinner.GetItemAtPosition(e.Position).ToString();
            }
            else
            {
                rtarih = spinner.GetItemAtPosition(e.Position).ToString();
                doktorlistAsync n = new doktorlistAsync(this);
                n.Execute();                 
                var adapter3 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, doktorlist);
                adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                doktor.Adapter = adapter3;
                doktor.Visibility = ViewStates.Visible;
                doktor.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Doktor_ItemSelected);
            
            }
            rtarih = spinner.GetItemAtPosition(e.Position).ToString();
        }
        private void Rndviptal_Click(object sender, System.EventArgs e)
        {
           SetContentView(Resource.Layout.saatanaliz);
           Window.SetTitle("Saat Analiz");
           grgt2 = FindViewById<Button>(Resource.Id.grgt2);
           grgt2.Click += Grgt2_Click;            
           BarChartView barchart = FindViewById<BarChartView>(Resource.Id.barChartView1);
            float toplam = s1 + s2 + s3 + s4 + s5 + s6;
            s1 = (s1 / toplam) * 100;
            s2 = (s2 / toplam) * 100;
            s3 = (s3 / toplam) * 100;
            s4 = (s4 / toplam) * 100;
            s5 = (s5 / toplam) * 100;
            s6 = (s6 / toplam) * 100;
            data = new BarModel[] { new BarModel() { Value = s1,Legend = "09:00",Color = Color.Blue }, new BarModel() { Value = s2,Legend = "09:30",Color = Color.Red },
                                   new BarModel() { Value = s3,Legend = "10:00",Color = Color.Green }, new BarModel() { Value = s4 ,Legend = "10:30",Color = Color.Yellow },
                                   new BarModel() { Value = s5,Legend = "11:00",Color = Color.Orange }, new BarModel() { Value = s6 ,Legend = "11:30",Color = Color.Pink}};                            
           barchart.ItemsSource = data;
           barchart.MaximumValue = 100;
           barchart.BarOffset = 50;
        }
        private void Grgt2_Click(object sender, EventArgs e)
        {
            polkilinikanalizAsync t = new polkilinikanalizAsync(this);
            t.Execute();
            SetContentView(Resource.Layout.anasayfa);
            Window.SetTitle("Anasayfa");
            rndval = FindViewById<Button>(Resource.Id.rndval);
            rndvsrgla = FindViewById<Button>(Resource.Id.rndvsrgla);
            rndviptal = FindViewById<Button>(Resource.Id.rndviptal);
            rndval.Click += Rndval_Click;
            rndvsrgla.Click += Rndvsrgla_Click;
            rndviptal.Click += Rndviptal_Click;
        }
        private void Rndvsrgla_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.analiz);
            Window.SetTitle("Poliklinik Analiz");
            grgt1 = FindViewById<Button>(Resource.Id.grgt1);
            grgt1.Click += Grgt1_Click;
            BarChartView barchart = FindViewById<BarChartView>(Resource.Id.barChartView1);
            float toplam = p1 + p2 + p3 + p4 + p5 ;
            p1 = (p1 / toplam) * 100;
            p2 = (p2 / toplam) * 100;
            p3 = (p3 / toplam) * 100;
            p4 = (p4 / toplam) * 100;
            p5 = (p5 / toplam) * 100;          
            data2 = new BarModel[] {     new BarModel() { Value = p1,Legend = "Dahiliye",Color = Color.Blue},    new BarModel() { Value = p2,Legend = "Genel Cerrah",Color = Color.Red },
                                         new BarModel() { Value = p3,Legend = "Ortopedi",Color = Color.Yellow }, new BarModel() { Value = p4 ,Legend = "  KulakBurunBoğaz",Color = Color.Green },
                                         new BarModel() { Value = p5,Legend = "Göz",Color = Color.Orange } };                      
            barchart.ItemsSource = data2;
            barchart.MaximumValue = 100;
            // barchart.BarWidth = 100;
            barchart.BarOffset = 80;
        }
        private void Grgt1_Click(object sender, EventArgs e)
        {
            polkilinikanalizAsync t = new polkilinikanalizAsync(this);
            t.Execute();
            SetContentView(Resource.Layout.anasayfa);
            Window.SetTitle("Anasayfa");
            rndval = FindViewById<Button>(Resource.Id.rndval);
            rndvsrgla = FindViewById<Button>(Resource.Id.rndvsrgla);
            rndviptal = FindViewById<Button>(Resource.Id.rndviptal);
            rndval.Click += Rndval_Click;
            rndvsrgla.Click += Rndvsrgla_Click;
            rndviptal.Click += Rndviptal_Click;
        }
        private void Kayit_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.kayit);
            Window.SetTitle("Kayıt");
            kad = FindViewById<EditText>(Resource.Id.kadi);
            ksoyad = FindViewById<EditText>(Resource.Id.ksoyad);
            ktc = FindViewById<EditText>(Resource.Id.ktc);
            kdgmyl = FindViewById<EditText>(Resource.Id.kdgmyl);
            kdgmyr = FindViewById<EditText>(Resource.Id.kdgmyr);
            kkayitol = FindViewById<Button>(Resource.Id.kkayitol);
            grgt3 = FindViewById<Button>(Resource.Id.grgt3);
            grgt3.Visibility = ViewStates.Invisible;
            grgt3.Click += Grgt3_Click;
            kkayitol.Click += Kkayitol_Click;   
        }
        private void Grgt3_Click(object sender, EventArgs e)
        {
            polkilinikanalizAsync t = new polkilinikanalizAsync(this);
            t.Execute();
            SetContentView(Resource.Layout.Main);
            tcno = FindViewById<EditText>(Resource.Id.tcNo);
            dgmyl = FindViewById<EditText>(Resource.Id.dgmyl);
            giris = FindViewById<Button>(Resource.Id.giris);
            kayit = FindViewById<Button>(Resource.Id.kayit);
            giris.Click += Giris_Click;
            kayit.Click += Kayit_Click;
        }
        private void Kkayitol_Click(object sender, System.EventArgs e)
        {
            
            hastakayitAsync n = new hastakayitAsync(this);
            n.Execute();
            if (qwe == "succes")
            {
                kkayitol.Visibility = ViewStates.Invisible;
                grgt3.Visibility = ViewStates.Visible;

             }
        }
        public class hastagirisAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity hastaGiris;

            public hastagirisAsync(MainActivity hastaGiris)
            {
                this.hastaGiris = hastaGiris;
            }

            string userTC, userdgmyl;
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                userTC = hastaGiris.tcno.Text;
                userdgmyl = hastaGiris.dgmyl.Text;
            }

            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastalogin.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("tc", userTC);
                parameters.Add("dogumyili", userdgmyl);
                var response = client.UploadValues(uri, parameters);
                var responseString = Encoding.Default.GetString(response);
                JSONObject ob = new JSONObject(responseString);

                hastaGiris.RunOnUiThread(() =>
                   hastaGiris.qwe = ob.OptString("message").ToString()
                  );

                if (ob.OptString("message").Equals("succes"))
                {
                    JSONArray jsonArray = ob.OptJSONArray("data");
                    JSONObject jsonObject = jsonArray.GetJSONObject(0);
                    
                    string addd = jsonObject.OptString("ad");
                    string soyaddd = jsonObject.OptString("soyad");
                    string tcc = jsonObject.OptString("tc");
                    string dogumyl = jsonObject.OptString("dogumyili");
                    string dogumyer = jsonObject.OptString("dogumyeri");
               

                 
                    hastaGiris.RunOnUiThread(() =>
                  hastaGiris.ad = addd.ToString()
                   );
                    hastaGiris.RunOnUiThread(() =>
                  hastaGiris.soyad = soyaddd.ToString()
                   );
                    hastaGiris.RunOnUiThread(() =>
                  hastaGiris.tc = tcc.ToString()
                   );
                    hastaGiris.RunOnUiThread(() =>
                  hastaGiris.dogumyili = dogumyl.ToString()
                   );
                    hastaGiris.RunOnUiThread(() =>
                  hastaGiris.dogumyeri = dogumyer.ToString()
                   );
                 
                }
                else if (ob.OptString("message").Equals("error"))
                {
                    hastaGiris.RunOnUiThread(() =>
                           Toast.MakeText(hastaGiris, "Hasta TC veya doğum yılı yanlış", ToastLength.Short).Show());
                }
                else if (ob.OptString("message").Equals("fields_null"))
                {
                    hastaGiris.RunOnUiThread(() =>
                           Toast.MakeText(hastaGiris, "Hasta TC veya doğum yılını boş bıraktınız", ToastLength.Short).Show());
                }
                return null;
            }
        }
        public class hastakayitAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity hastaKayit;

            public hastakayitAsync(MainActivity hastaKayit)
            {
                this.hastaKayit = hastaKayit;
            }
            string ad, soyad, tc, dogumyil, dogumyer;
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                ad = hastaKayit.kad.Text;
                soyad = hastaKayit.ksoyad.Text;
                tc = hastaKayit.ktc.Text;
                dogumyil = hastaKayit.kdgmyl.Text;
                dogumyer = hastaKayit.kdgmyr.Text;
             
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastaekle.php");
                NameValueCollection paramaters = new NameValueCollection();
                paramaters.Add("ad", ad);
                paramaters.Add("soyad", soyad);
                paramaters.Add("tc", tc);
                paramaters.Add("dogumyili", dogumyil);
                paramaters.Add("dogumyeri", dogumyer);
                client.UploadValues(uri, paramaters);
                var responseString = Encoding.Default.GetString(client.UploadValues(uri, paramaters));
                JSONObject ob = new JSONObject(responseString);
                hastaKayit.RunOnUiThread(() =>
                hastaKayit.qwe = ob.OptString("message").ToString()
                  );
                if (ob.OptString("message").Equals("succes"))
                {
                    hastaKayit.RunOnUiThread(() =>
                           Toast.MakeText(hastaKayit, "Kayıt Yapıldı", ToastLength.Short).Show());
                }
                else if (ob.OptString("message").Equals("error"))
                {
                    hastaKayit.RunOnUiThread(() =>
                           Toast.MakeText(hastaKayit, "Kayıt Yapılamadı", ToastLength.Short).Show());
                }
                else if (ob.OptString("message").Equals("bos"))
                {
                    hastaKayit.RunOnUiThread(() =>
                           Toast.MakeText(hastaKayit, "Boş yer bırakmayınız!!!", ToastLength.Short).Show());
                }

                return null;
            }
        }
        public class doktorlistAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity doktorlst;
            public doktorlistAsync(MainActivity doktorlst)
            {
                this.doktorlst = doktorlst;
            }
            string plklnk,tarih;
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                plklnk = doktorlst.rplklnk;
                tarih = doktorlst.rtarih;
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastadoktorbul.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("doktor_plk", plklnk);
                parameters.Add("tarih", tarih);
                var response = client.UploadValues(uri, parameters);
                var responseString = Encoding.Default.GetString(response);
                JSONObject ob = new JSONObject(responseString);              
                if (ob.OptString("message").Equals("succes"))
                {                  
                    JSONArray jsonArray = ob.OptJSONArray("data");
                    doktorlst.doktorlist.Clear();
                    doktorlst.doktorlist.Add("Doktor Seçiniz");
                    for (int i = 0; i < jsonArray.Length(); i++)
                    {
                        JSONObject jsonObject = jsonArray.GetJSONObject(i);
                        doktorlst.doktorlist.Add(jsonObject.OptString("doktor_ad"));
                    }
                }
                else if (ob.OptString("message").Equals("error"))
                {
                    doktorlst.RunOnUiThread(() =>
                           Toast.MakeText(doktorlst, "Doktor Bulunamadı", ToastLength.Short).Show());
                }
                else if (ob.OptString("message").Equals("fields_null"))
                {
                    doktorlst.RunOnUiThread(() =>
                           Toast.MakeText(doktorlst, "Lütfen Tarih Seçiniz!!", ToastLength.Short).Show());
                }
                return null;
            }
        }
        public class saatlistAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity saatlst;
            public saatlistAsync(MainActivity saatlst)
            {
                this.saatlst = saatlst;
            }
            string doktor,tarih,poliklinik;
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                doktor = saatlst.rdoktor;
                poliklinik = saatlst.rplklnk;
                tarih = saatlst.rtarih;
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastasaatbul.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("doktor", doktor);
                parameters.Add("tarih", tarih);
                parameters.Add("poliklinik", poliklinik);
                var response = client.UploadValues(uri, parameters);
                var responseString = Encoding.Default.GetString(response);
                JSONObject ob = new JSONObject(responseString);

                if (ob.OptString("message").Equals("succes"))
                {
                    JSONArray jsonArray = ob.OptJSONArray("data");
                    saatlst.clocks.Clear();
                    saatlst.clocks.Add("Saat Seçiniz");
                    for (int i = 0; i < jsonArray.Length(); i++)
                    {
                        JSONObject jsonObject = jsonArray.GetJSONObject(i);
                        saatlst.clocks.Add(jsonObject.OptString("saat"));
                    }
                }
                else if (ob.OptString("message").Equals("error"))
                {
                    saatlst.RunOnUiThread(() =>
                           Toast.MakeText(saatlst, "Saat Bulunamadı", ToastLength.Short).Show());
                }
                else if (ob.OptString("message").Equals("fields_null"))
                {
                    saatlst.RunOnUiThread(() =>
                           Toast.MakeText(saatlst, "Lütfen Doktor Seçiniz!!", ToastLength.Short).Show());
                }
                return null;
            }
        }
        public class ronaylaAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity ronayla;

            public ronaylaAsync(MainActivity ronayla)
            {
                this.ronayla = ronayla;
            }
            string tc,ad, soyad, dogumyili, doktor, poliklinik, saat,tarih;
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                tc = ronayla.tc;
                ad = ronayla.ad;                
                soyad = ronayla.soyad;
                dogumyili = ronayla.dogumyili;
                poliklinik = ronayla.rplklnk;
                tarih = ronayla.rtarih;
                doktor = ronayla.rdoktor;
                saat = ronayla.rsaat;
               
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastarandevu.php");
                NameValueCollection paramaters = new NameValueCollection();
                paramaters.Add("tc", tc);
                paramaters.Add("ad", ad);
                paramaters.Add("soyad", soyad);
                paramaters.Add("dogumyili", dogumyili);
                paramaters.Add("poliklinik", poliklinik);
                paramaters.Add("tarih", tarih);
                paramaters.Add("doktor", doktor);
                paramaters.Add("rsaat", saat);
                client.UploadValuesAsync(uri, paramaters);
                return null;
            }
        }
        public class saatanalizAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity saat;
            public saatanalizAsync(MainActivity saat)
            {
                this.saat = saat;
            }
            
            protected override void OnPreExecute()
            {
                base.OnPreExecute();               
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastasaatanaliz.php");
                NameValueCollection parameters = new NameValueCollection();              
                var response = client.UploadValues(uri, parameters);
                var responseString = Encoding.Default.GetString(response);
                JSONObject ob = new JSONObject(responseString);
                JSONArray jsonArray = ob.OptJSONArray("data");
                saat.s1 = float.Parse(jsonArray.GetJSONObject(0).OptString("deger"));
                saat.s2 = float.Parse(jsonArray.GetJSONObject(1).OptString("deger"));
                saat.s3 = float.Parse(jsonArray.GetJSONObject(2).OptString("deger"));
                saat.s4 = float.Parse(jsonArray.GetJSONObject(3).OptString("deger"));
                saat.s5 = float.Parse(jsonArray.GetJSONObject(4).OptString("deger"));
                saat.s6 = float.Parse(jsonArray.GetJSONObject(5).OptString("deger"));
               
                return null;
            }
        }
        public class polkilinikanalizAsync : AsyncTask<Java.Lang.Object, Java.Lang.Object, Java.Lang.Object>
        {
            MainActivity saat;
            public polkilinikanalizAsync(MainActivity saat)
            {
                this.saat = saat;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override Java.Lang.Object RunInBackground(params Java.Lang.Object[] @params)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://hasimyerli.com/burak/hastapanaliz.php");
                NameValueCollection parameters = new NameValueCollection();
                var response = client.UploadValues(uri, parameters);
                var responseString = Encoding.Default.GetString(response);
                JSONObject ob = new JSONObject(responseString);
                JSONArray jsonArray = ob.OptJSONArray("data");
                saat.p1 = float.Parse(jsonArray.GetJSONObject(0).OptString("deger"));
                saat.p2 = float.Parse(jsonArray.GetJSONObject(1).OptString("deger"));
                saat.p3 = float.Parse(jsonArray.GetJSONObject(2).OptString("deger"));
                saat.p4 = float.Parse(jsonArray.GetJSONObject(3).OptString("deger"));
                saat.p5 = float.Parse(jsonArray.GetJSONObject(4).OptString("deger"));
                

                return null;
            }
        }
    }
}

