using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectriesViewer
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public Form1 Form1
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            label2.Text += "\n By: \n Maryam Lawal And Sandeep Sakale";
            timer1.Start();
        }
       

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value==100)
            {
                timer1.Stop();
                Form1 mainform = new Form1();
                mainform.Show();
                this.Hide();
          
            }
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {

        }
    }
}
