using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class GRN : Form
    {
        public string connectionString = @"Server=MSI\SQLEXPRESS;Database=pos;Integrated Security=True;";

        public GRN()
        {
            InitializeComponent();
        }

        private void GRN_Load(object sender, EventArgs e)
        {
            // On form load, fetch and display existing GRN records in the DataGridView
            LoadGrnData();

        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuantityReceived.Text, out int qty) &&
                decimal.TryParse(txtPricePerUnit.Text, out decimal unitPrice))
            {
                decimal total = qty * unitPrice;
                txtTotalAmount.Text = total.ToString("F2");
            }
            else
            {
                MessageBox.Show("Please enter valid Quantity and Unit Price to calculate total.");
            }
        }

        private void btnSaveGRN_Click(object sender, EventArgs e)
        {
            // 1. Gather data from form controls
            string grnNumber = txtGRNNumber.Text.Trim();
            string supplierName = txtSupplierName.Text.Trim();
            string productCode = txtProductCode.Text.Trim();
            string productDescription = txtProductDescription.Text.Trim();

            // Parse numeric fields carefully
            if (!int.TryParse(txtQuantityReceived.Text, out int quantityReceived))
            {
                MessageBox.Show("Please enter a valid number for Quantity Received.");
                return;
            }

            if (!decimal.TryParse(txtPricePerUnit.Text, out decimal pricePerUnit))
            {
                MessageBox.Show("Please enter a valid number for Price Per Unit.");
                return;
            }

            if (!decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
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

                        // Clear input fields if needed
                        ClearFormInputs();

                        // Reload the DataGridView to show the new record
                        LoadGrnData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving GRN record: " + ex.Message);
                    }
                }
            }
        }

        private void LoadGrnData()
        {
            // Populate DataGridView with records from the GRN table
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string selectSql = "SELECT * FROM GRN ORDER BY GRNID DESC";

                using (SqlCommand cmd = new SqlCommand(selectSql, conn))
                {
                    try
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        dgvGRN.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading GRN data: " + ex.Message);
                    }
                }
            }
        }

        private void ClearFormInputs()
        {
            txtGRNNumber.Clear();
            txtSupplierName.Clear();
            txtProductCode.Clear();
            txtProductDescription.Clear();
            txtQuantityReceived.Clear();
            txtPricePerUnit.Clear();
            txtTotalAmount.Clear();
            dtpDateReceived.Value = DateTime.Now;
        }

        private void dgvGRN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
