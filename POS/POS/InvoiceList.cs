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
    public partial class InvoiceList : Form
    {
        public string connectionString = @"Server=MSI\SQLEXPRESS;Database=pos;Integrated Security=True;";

        public InvoiceList()
        {
            InitializeComponent();
        }

        private void InvoiceList_Load(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            // Query SalesInvoiceHeader to get top-level invoice info
            string query = @"
                SELECT 
                    InvoiceID,
                    InvoiceNumber,
                    CustomerName,
                    DateOfSale,
                    Discount,
                    TotalBeforeDisc,
                    TotalAfterDisc
                FROM SalesInvoiceHeader
                ORDER BY InvoiceID DESC
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        dgvInvoices.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading invoices: " + ex.Message);
                    }
                }
            }
        }

        private void LoadInvoiceDetails(int invoiceID)
        {
            string query = @"
                        SELECT 
                            InvoiceDetailID,
                            ProductCode,
                            Description,
                            QuantitySold,
                            UnitPrice,
                            TotalPrice
                        FROM SalesInvoiceDetails
                        WHERE InvoiceID = @InvoiceID
                            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                    try
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        dgvInvoiceDetails.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading invoice details: " + ex.Message);
                    }
                }
            }
        }

        private void dgvInvoiceDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = Convert.ToInt32(
                    dgvInvoices.SelectedRows[0].Cells["InvoiceID"].Value);

                LoadInvoiceDetails(selectedInvoiceID);
            }
        }
    }
}
