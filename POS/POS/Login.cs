using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Login : Form
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

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Read username and password from text boxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate user
            if (ValidateUser(username, password))
            {
                // If valid, open the HomeForm and hide the login form
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                // If invalid, display an error message
                MessageBox.Show("Invalid username or password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            // Adjust this to your actual connection string
            string connectionString = @"Server=MSI\SQLEXPRESS;Database=pos;Integrated Security=True;";

            // Query to check if a record with matching username/password exists
            string query = @"SELECT COUNT(1) 
                             FROM [dbo].[Users] 
                             WHERE Username = @Username AND Password = @Password";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use parameterized queries to avoid SQL injection
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        con.Open();

                        // ExecuteScalar returns the first column of the first row
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        isValid = (count == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception in a real application
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }

            return isValid;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Read username and password from text boxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate user
            if (ValidateUser(username, password))
            {
                // If valid, open the HomeForm and hide the login form
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                // If invalid, display an error message
                MessageBox.Show("Invalid username or password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Read username and password from text boxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate user
            if (ValidateUser(username, password))
            {
                // If valid, open the HomeForm and hide the login form
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                // If invalid, display an error message
                MessageBox.Show("Invalid username or password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Close This Application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Read username and password from text boxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate user
            if (ValidateUser(username, password))
            {
                // If valid, open the HomeForm and hide the login form
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide();
            }
            else
            {
                // If invalid, display an error message
                MessageBox.Show("Invalid username or password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Close This Application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
    }
}
