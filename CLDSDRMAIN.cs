using System;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Collections;
using System.Drawing;

namespace downloadmanager
{
    public partial class downloadmanager : Form
    {
        public downloadmanager()
        {
            InitializeComponent();
        }
        int count = 7;
        private void timer1_Tick(object sender, EventArgs e)
        {
            re:
            if (count > 0)
            {
                pictureBox1.Image = imageList1.Images[count];
                count--;
            }
            else
            {
                count = 7;
                goto re;
            }
        }
        WebClient mainWebClient1 = new WebClient();
        WebClient mainWebClient2 = new WebClient();
        WebClient mainWebClient3 = new WebClient();
        WebClient mainWebClient4 = new WebClient();
        string reallink1 = "https://onedrive.live.com/download?cid=3DD03CC92A52BF65&resid=3DD03CC92A52BF65%2153131&authkey=AO_2UM3oAOsuvjg";
        string reallink2 = "https://onedrive.live.com/download?cid=3DD03CC92A52BF65&resid=3DD03CC92A52BF65%2153132&authkey=AFDqxAyDxcRHY7Q";
        string reallink3 = "https://onedrive.live.com/download?cid=3DD03CC92A52BF65&resid=3DD03CC92A52BF65%2153134&authkey=AHM73LRu0pN2qYk";
        string reallink4 = "https://onedrive.live.com/download?cid=3DD03CC92A52BF65&resid=3DD03CC92A52BF65%2153135&authkey=ALCP9tvN9D2OeX4";
        string infofile = "https://drive.google.com/uc?export=download&id=1OIdAD7PdpgyY1bE22nRaaY9fQETx4ImW";
        string cracklink = "https://drive.google.com/uc?export=download&id=1wa91bIy1BqBk7LB3YVbgq6Qf3OhtLPAB";
        private void downloadmanager_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var nic in nics)
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    macList = nic.GetPhysicalAddress().ToString();
                }
            }
            //
            try
            {
                FileStream checkfile = new FileStream("APPMANIFEST.DLL", FileMode.CreateNew);
                checkfile.Close();
            }
            catch (Exception) { }
            try
            {
                FileInfo a = new FileInfo(@"Resources\cracker.bat");
                a.Delete();
            }
            catch (Exception) { }
            try
            {
                FileInfo b1 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part1.exe");
                b1.Delete();
                FileInfo b2 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part2.rar");
                b2.Delete();
                FileInfo b3 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part3.rar");
                b3.Delete();
                FileInfo b4 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part4.rar");
                b4.Delete();
            }
            catch (Exception) { }
            try
            {
                WebClient infofilesync = new WebClient();
                infofilesync.DownloadFileCompleted += new AsyncCompletedEventHandler(infosynced);
                infofilesync.DownloadFileAsync(new Uri(infofile), "local.tmmp");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x2.png";
                label1.Text = "無法檢查資訊";
                label2.Text = "請確認網路連接是否中斷。";
                Btn_http.Text = "請重新開啟本程式";
                MessageBox.Show("檢查資訊發生錯誤，請重新啟動本程式。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label3.Text= string.Format("版本{0}R-[Cyberlink_Productions]",Assembly.GetExecutingAssembly().GetName().Version.ToString());
            label1.Text = "正在檢查資訊";
        }
        string code = "";//電腦識別碼2(除了mac address 之外的)
        public static bool a;
        private void infosynced (object sender, AsyncCompletedEventArgs e)
        {
            ArrayList list = new ArrayList();
            string[] urls= { "" };
            try
            {
                StreamReader urlread = new StreamReader("local.tmmp");
                string temp1 = "";
                while (temp1 != "<6A3C556A-9F4E-487C-AE78-B584C605ED1D>")
                {
                    temp1 = urlread.ReadLine();
                }
                string temp2 = "";
                while (temp2 != "</6A3C556A-9F4E-487C-AE78-B584C605ED1D>")
                {
                    temp2 = urlread.ReadLine();
                    if (temp2 != "</6A3C556A-9F4E-487C-AE78-B584C605ED1D>")
                    {
                        list.Add(temp2);
                    }
                }
                urls = new string[list.Count];
                list.CopyTo(urls);
                urlread.Close();
                FileInfo k = new FileInfo("local.tmmp");
                k.Delete();
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x2.png";
                label1.Text = "無法檢查資訊";
                label2.Text = "請確認網路連接是否中斷。";
                MessageBox.Show("檢查資訊發生錯誤，請重新啟動本程式。若問題持續發生，請回報給我們，謝謝。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                for (int i = 0; i <= ((list.Count) - 1); i++)
                {
                    if (macList == urls[i])
                    {
                        code = urls[i + 1];
                        string chkinfotmp = "";
                        recheck:
                        StreamReader checkfile = new StreamReader("APPMANIFEST.DLL");
                        chkinfotmp = checkfile.ReadLine();
                        checkfile.Close();
                        if (chkinfotmp == code)
                        {
                            label1.Text = "  已獲得授權";
                            label2.Text = "      您可以安裝正版軟體";
                            Btn_http.Enabled = true;
                            groupBox1.Visible = true;
                            groupBox1.Enabled = true;
                            button4.Visible = false;
                            timer1.Enabled = false;
                            textBox1.BackColor = Color.Green;
                            label3.ForeColor = Color.White;
                            label3.BackColor = Color.Green;
                            pictureBox1.ImageLocation = @"Resources\x3.png";
                            break;
                        }
                        else
                        {
                            MessageBox.Show("您的電腦已成功被註冊，但您可能尚未輸入認證碼或是有輸入錯誤，請嘗試正確輸入您收到的認證碼，以完成驗證。若有問題請聯絡我們。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            codeinput fs = new codeinput();
                            fs.ShowDialog();
                            if (a == true)
                            {
                                goto recheck;
                            }
                            button4.Visible = true;
                            button3.Visible = true;
                            timer1.Enabled = false;
                            pictureBox1.ImageLocation = @"Resources\x1.png";
                            label1.Text = "  未獲得授權";
                            label2.Text = "     您已可以使用試用版";
                            MessageBox.Show("您的電腦尚未註冊使用正版，若要安裝這個應用程式的正版，請按下「顯示MCode」按鈕，並將此碼以郵寄或其他方式傳送給 trusaidlin@gmail.com 開發人員。若申請成功，我們將會通知您。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                    else if (i == ((list.Count) - 1))
                    {
                        button4.Visible = true;
                        button3.Visible = true;
                        timer1.Enabled = false;
                        pictureBox1.ImageLocation = @"Resources\x1.png";
                        label1.Text = "  未獲得授權";
                        label2.Text = "     您已可以使用試用版";
                        MessageBox.Show("您的電腦尚未註冊使用正版，若要安裝這個應用程式的正版，請按下「顯示MCode」按鈕，並將此碼以郵寄或其他方式傳送給 trusaidlin@gmail.com 開發人員。若申請成功，我們將會通知您。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x2.png";
                label1.Text = "無法檢查資訊";
                label2.Text = "無法正確的辨識您的身分。";
                MessageBox.Show("檢查資訊發生錯誤，請重新啟動本程式。若問題持續發生，請回報給我們，謝謝。代碼(K)", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_http_Click(object sender, EventArgs e)
        {
            Btn_http.Enabled = false;
            groupBox1.Enabled = false;
            Btn_http.Text = "請稍候...";
            label1.Text = "正在下載檔案";
            label2.Text = "      您可以繼續使用電腦";
            timer1.Enabled = true;
            try
            {
                label5.Text = "進度 0/4";
                mainWebClient1.DownloadFileCompleted += new AsyncCompletedEventHandler(Http_Completed1);
                mainWebClient1.DownloadFileAsync(new Uri(reallink1), @"Resources\PACK-CLDS\CLDS6_pack.part1.exe");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                label1.Text = "安裝遭到終止";
                label2.Text = "  發生A錯誤導致安裝中斷。";
                Btn_http.Text = "請重新開啟本程式";
                pictureBox1.ImageLocation = @"Resources\x2.png";
            }
        }
        private void Http_Completed1(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                label5.Text = "進度 1/4";
                mainWebClient2.DownloadFileCompleted += new AsyncCompletedEventHandler(Http_Completed2);
                mainWebClient2.DownloadFileAsync(new Uri(reallink2), @"Resources\PACK-CLDS\CLDS6_pack.part2.rar");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                label1.Text = "安裝遭到終止";
                label2.Text = "  發生A錯誤導致安裝中斷。";
                Btn_http.Text = "請重新開啟本程式";
                pictureBox1.ImageLocation = @"Resources\x2.png";
            }

        }
        private void Http_Completed2(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                label5.Text = "進度 2/4";
                mainWebClient3.DownloadFileCompleted += new AsyncCompletedEventHandler(Http_Completed3);
                mainWebClient3.DownloadFileAsync(new Uri(reallink3), @"Resources\PACK-CLDS\CLDS6_pack.part3.rar");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                label1.Text = "安裝遭到終止";
                label2.Text = "  發生A錯誤導致安裝中斷。";
                Btn_http.Text = "請重新開啟本程式";
                pictureBox1.ImageLocation = @"Resources\x2.png";
            }

        }
        private void Http_Completed3(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                label5.Text = "進度 3/4";
                mainWebClient4.DownloadFileCompleted += new AsyncCompletedEventHandler(Http_Completed4);
                mainWebClient4.DownloadFileAsync(new Uri(reallink4), @"Resources\PACK-CLDS\CLDS6_pack.part4.rar");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                label1.Text = "安裝遭到終止";
                label2.Text = "  發生A錯誤導致安裝中斷。";
                Btn_http.Text = "請重新開啟本程式";
                pictureBox1.ImageLocation = @"Resources\x2.png";
            }
        }
        private void Http_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
        }
        private void Http_Completed4(object sender, AsyncCompletedEventArgs e)
        {
            timer1.Enabled = false;
            Btn_http.Enabled = false;
            groupBox1.Enabled = false;
            try
            {
                label5.Text = "";
                Process p = new Process();
                p.StartInfo.FileName = (@"Resources\PACK-CLDS\CLDS6_pack.part1.exe");
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x3.png";
                p.Close();
                label1.Text = "  已獲得授權";
                label2.Text = "      您可以安裝正版軟體";
                //Btn_http.Enabled = true;
                groupBox1.Enabled = true;
                Btn_http.Text = "已下載完成";
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x2.png";
                Btn_http.Text = "請重新開啟本程式";
                label1.Text = "安裝遭到終止";
                label2.Text = "  發生F錯誤導致安裝中斷。";
                MessageBox.Show("執行套件時發生錯誤，請再試一次。若問題持續發生，請回報給我們，謝謝。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string macList = "";
        private void button4_Click(object sender, EventArgs e)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var nic in nics)
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    macList = nic.GetPhysicalAddress().ToString();
                }
            }
            MessageBox.Show("您的MCode是:  "+ "CLDS6DR-"+macList + "MCSDD-G"+"  ，請將此碼傳送至trusaidlin@gmail.com以獲得認證，謝謝。","MCode");
        }
        WebClient crackdl = new WebClient();
        private void crackdl_Completed(object sender, AsyncCompletedEventArgs e)
        {
            timer1.Enabled = false;
            button1.Enabled = false;
            try
            {

                ProcessStartInfo Info2 = new ProcessStartInfo();
                Info2.FileName = "cracker.bat";//執行的檔案名稱
                Info2.WorkingDirectory = @"Resources\";//檔案所在的目錄
                Process.Start(Info2);//
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x3.png";
                label1.Text = "  已獲得授權";
                label2.Text = "      您可以安裝正版軟體";
                button1.Enabled = true;
                Btn_http.Enabled = true;
                button1.Text = "破解";
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                pictureBox1.ImageLocation = @"Resources\x2.png";
                Btn_http.Text = "請重新開啟本程式";
                label1.Text = "破解遭到終止";
                label2.Text = "  發生H錯誤導致破解中斷。";
                MessageBox.Show("執行套件時發生錯誤，請再試一次。若問題持續發生，請回報給我們，謝謝。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Btn_http.Enabled = false;
            button1.Text = "請稍候...";
            label1.Text = "正在啟動破解";
            label2.Text = "      正在連結到破解程序";
            timer1.Enabled = true;
            try
            {
                crackdl.DownloadFileCompleted += new AsyncCompletedEventHandler(crackdl_Completed);
                crackdl.DownloadFileAsync(new Uri(cracklink), @"Resources\cracker.bat");
            }
            catch (Exception)
            {
                timer1.Enabled = false;
                label1.Text = "破解遭到終止";
                label2.Text = "  發生G錯誤導致破解中斷。";
                Btn_http.Text = "請重新開啟本程式";
                pictureBox1.ImageLocation = @"Resources\x2.png";
            }
        }

        private void downloadmanager_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FileInfo a = new FileInfo(@"Resources\cracker.bat");
                a.Delete();
            }
            catch (Exception) { }
            try
            {
                FileInfo b1 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part1.exe");
                b1.Delete();
                FileInfo b2 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part2.rar");
                b2.Delete();
                FileInfo b3 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part3.rar");
                b3.Delete();
                FileInfo b4 = new FileInfo(@"Resources\PACK-CLDS\CLDS6_pack.part4.rar");
                b4.Delete();
            }
            catch (Exception) { }
            try
            {
                FileInfo c = new FileInfo("local.tmmp");
                c.Delete();
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo Info2 = new ProcessStartInfo();
                Info2.FileName = "CLDS6TRail.exe";//執行的檔案名稱
                Info2.WorkingDirectory = @"Resources\";//檔案所在的目錄
                Process.Start(Info2);
            }
            catch (Exception)
            {
                MessageBox.Show("安裝封包損毀，請重新安裝本程式。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            activationkey a = new activationkey();
            a.ShowDialog();
        }
    }
}