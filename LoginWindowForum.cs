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
    public partial class LoginWindowForum : Form
    {
        public LoginWindowForum()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            LoginWorker.RunWorkerAsync();
        }

        private void LoginWindowForum_Load(object sender, EventArgs e)
        {
            Functions.InitializeApplication(VLAuthentication.PUBLIC_KEY);
        }

        private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Functions.Login(textBox1.Text, textBox2.Text, s =>
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
