using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Home : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
     (
         int nLeftRect,     // x-coordinate of upper-left corner
         int nTopRect,      // y-coordinate of upper-left corner
         int nRightRect,    // x-coordinate of lower-right corner
         int nBottomRect,   // y-coordinate of lower-right corner
         int nWidthEllipse, // width of ellipse
         int nHeightEllipse // height of ellipse
     );

        public Home()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void btnViewGRN_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Close This Application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            dashboard c1 = new dashboard();
            panel3.Controls.Add(c1);
            c1.BringToFront();
            c1.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            GRNController c1 = new GRNController();
            panel3.Controls.Add(c1);
            c1.BringToFront();
            c1.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            repIssue c1 = new repIssue();
            panel3.Controls.Add(c1);
            c1.BringToFront();
            c1.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           

            invoice c1 = new invoice();
            panel3.Controls.Add(c1);
            c1.BringToFront();
            c1.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Logout This Application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
    }
}
