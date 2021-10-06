using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;

namespace Vanguard_Csharp_Example
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string ModuleName = "";
        string statusStr = "";

        private void MainWindow_Load(object sender, EventArgs e)
        {
            StatusWorker.RunWorkerAsync();
            UsernameRecoveryWorker.RunWorkerAsync();
            EmailRecoveryWorker.RunWorkerAsync();
            GetModulesWorker.RunWorkerAsync();
        }


        private void GetModulesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Functions.GetModules(response => {
                e.Result = response;
            });

            e.Result = JsonConvert.DeserializeObject<dynamic>((string)e.Result);
        }

        private void GetModulesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dynamic data = e.Result;
            listBox1.Items.Clear();
            foreach (dynamic item in data)
            {
                listBox1.Items.Add(item.name);
            }
        }

        private void UsernameRecoveryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Functions.UsernameRecovery(response => {
                e.Result = response;
            });
        }

        private void UsernameRecoveryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label2.Text = "Username: " + e.Result;
        }

        private void EmailRecoveryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Functions.EmailRecovery(response => {
                e.Result = response;
            });
        }

        private void EmailRecoveryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label3.Text = "Email: " + e.Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Visible = false;

            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("No Module Selected.", "Vanguard");
                button1.Enabled = true;
                return;
            }
            ModuleName = listBox1.SelectedItem.ToString();
            InjectWorker.RunWorkerAsync();
        }

        private void StatusWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Get Status from vanguard.
            Functions.Status(response => {
                statusStr = response;
                e.Result = response;
            });
        }

        private void StatusWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Apply the actual status to Label.
            statusLbl.Text = "Status: " + statusStr;

            if (statusStr == "Getting module list..." || statusStr == "Downloading resources...")
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
            
            if (statusStr == "Injection Failed." || statusStr == "Injection Success!" || statusStr == "Ready." || statusStr.Contains("not running"))
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                button1.Enabled = true;
            } 

            StatusWorker.RunWorkerAsync();
        }

        private void InjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            Functions.InjectModule(ModuleName);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void InjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            button1.Visible = true;
        }
    }
}
