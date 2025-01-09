using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class GRNController : UserControl
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConfig dbcon = new DBConfig();
        public string connectionString = @"Server=MSI\SQLEXPRESS;Database=pos;Integrated Security=True;";

        public GRNController()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadRecord();
        }

        public void LoadRecord()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM GRN ORDER BY GRNID DESC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["GRNNumber"].ToString(), dr["SupplierName"].ToString(), dr["ProductCode"].ToString(), dr["ProductDescription"].ToString(), dr["QuantityReceived"].ToString(), DateTime.Parse(dr["DateReceived"].ToString()).ToString("MM.dd.yyyy"), dr["PricePerUnit"].ToString(), dr["TotalAmount"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Gather data from form controls
            string grnNumber = textBox1.Text.Trim();
            string supplierName = textBox2.Text.Trim();
            string productCode = textBox4.Text.Trim();
            string productDescription = textBox3.Text.Trim();

            // Parse numeric fields carefully
            if (!int.TryParse(textBox6.Text, out int quantityReceived))
            {
                MessageBox.Show("Please enter a valid number for Quantity Received.");
                return;
            }

            if (!decimal.TryParse(textBox8.Text, out decimal pricePerUnit))
            {
                MessageBox.Show("Please enter a valid number for Price Per Unit.");
                return;
            }

            if (!decimal.TryParse(textBox7.Text, out decimal totalAmount))
            {
                MessageBox.Show("Please enter a valid number for Total Amount.");
                return;
            }

            DateTime dateReceived = dtpDateReceived.Value;

            // 2. Basic validation checks
            if (string.IsNullOrEmpty(grnNumber))
            {
                MessageBox.Show("GRN Number is required.");
                return;
            }
            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Supplier Name is required.");
                return;
            }
            if (string.IsNullOrEmpty(productCode))
            {
                MessageBox.Show("Product Code is required.");
                return;
            }

            // 3. Insert record into database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertSql = @"
                    INSERT INTO GRN 
                    (GRNNumber, SupplierName, ProductCode, ProductDescription, 
                     QuantityReceived, DateReceived, PricePerUnit, TotalAmount)
                    VALUES 
                    (@GRNNumber, @SupplierName, @ProductCode, @ProductDescription,
                     @QuantityReceived, @DateReceived, @PricePerUnit, @TotalAmount)
                ";

                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@GRNNumber", grnNumber);
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@ProductCode", productCode);
                    cmd.Parameters.AddWithValue("@ProductDescription", productDescription);
                    cmd.Parameters.AddWithValue("@QuantityReceived", quantityReceived);
                    cmd.Parameters.AddWithValue("@DateReceived", dateReceived);
                    cmd.Parameters.AddWithValue("@PricePerUnit", pricePerUnit);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("GRN record saved successfully.");
                        LoadRecord();
                        clearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving GRN record");
                    }
                }
            }
        }


        public void calculateTotal()
        {
            // Check if Quantity Received and Price Per Unit are valid numbers
            if (decimal.TryParse(textBox6.Text, out decimal quantityReceived) &&
                decimal.TryParse(textBox8.Text, out decimal pricePerUnit))
            {
                // Calculate total amount
                decimal totalAmount = quantityReceived * pricePerUnit;

                // Display the calculated total in textBox7 (Total Amount)
                textBox7.Text = totalAmount.ToString("0.00");
            }
            else
            {
                // If input is invalid, clear the Total Amount
                textBox7.Text = string.Empty;
            }
        }


        public void clearData()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox8.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void GRNController_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            calculateTotal();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            calculateTotal();
        }
    }
}
