using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;

namespace Vanguard_Csharp_Example
{
    public partial class LoginWindowWHMCS : Form
    {
        public LoginWindowWHMCS()
        {
            InitializeComponent();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            
            Functions.InitializeApplication(VLAuthentication.PUBLIC_KEY);
           // MessageBox.Show("Iniciado.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            LoginWorker.RunWorkerAsync();
        }

        private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Functions.LoginWithLicense(textBox1.Text, s =>
            {
                e.Result = s;
            });
        }

        private void LoginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string result = (string)e.Result;
            if (result == "true")
            {
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(result, "Vanguard Loaders");
            }

            button1.Enabled = true;
        }
    }
}
