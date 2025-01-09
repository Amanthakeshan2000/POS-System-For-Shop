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
    public partial class SalesInvoice : Form
    {

        public string connectionString = @"Server=MSI\SQLEXPRESS;Database=pos;Integrated Security=True;";

        public SalesInvoice()
        {
            InitializeComponent();
        }

        private void SalesInvoice_Load(object sender, EventArgs e)
        {
            // 1. Generate or fetch a new InvoiceNumber
            txtInvoiceNumber.Text = GenerateInvoiceNumber();

            // 2. Set default date
            dtpSaleDate.Value = DateTime.Now;

            // 3. Initialize DataGridView columns if you haven't already done so in the Designer
            InitializeInvoiceDataGridView();

            // 4. Load product codes from GRN table (for the combo box in the grid)
            LoadProductCodesIntoCombo();
        }

        private string GenerateInvoiceNumber()
        {
            // For simplicity, generate something like "INV-" + date/time
            // In production, you might use a sequence in the DB or a more robust approach
            return "INV-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        private void InitializeInvoiceDataGridView()
        {
            // If columns are not set via Designer, do it here.
            // Example using a ComboBox column for ProductCode:
            if (dgvInvoiceItems.Columns.Count == 0)
            {
                DataGridViewComboBoxColumn productCodeCol = new DataGridViewComboBoxColumn
                {
                    Name = "ProductCode",
                    HeaderText = "Product Code",
                    DataPropertyName = "ProductCode",  // if you’re binding
                    Width = 120
                };

                // Description column (read-only)
                DataGridViewTextBoxColumn descCol = new DataGridViewTextBoxColumn
                {
                    Name = "Description",
                    HeaderText = "Description",
                    ReadOnly = true,
                    Width = 200
                };

                // UnitPrice column (read-only)
                DataGridViewTextBoxColumn unitPriceCol = new DataGridViewTextBoxColumn
                {
                    Name = "UnitPrice",
                    HeaderText = "Unit Price",
                    ReadOnly = true
                };

                // QuantitySold column (editable)
                DataGridViewTextBoxColumn qtyCol = new DataGridViewTextBoxColumn
                {
                    Name = "QuantitySold",
                    HeaderText = "Quantity",
                };

                // TotalPrice column (read-only or calculated)
                DataGridViewTextBoxColumn totalPriceCol = new DataGridViewTextBoxColumn
                {
                    Name = "TotalPrice",
                    HeaderText = "Total Price",
                    ReadOnly = true
                };

                dgvInvoiceItems.Columns.Add(productCodeCol);
                dgvInvoiceItems.Columns.Add(descCol);
                dgvInvoiceItems.Columns.Add(unitPriceCol);
                dgvInvoiceItems.Columns.Add(qtyCol);
                dgvInvoiceItems.Columns.Add(totalPriceCol);
            }

            // Event handlers for cell changes (e.g., product code selected, quantity changed)
            dgvInvoiceItems.CellValueChanged += DgvInvoiceItems_CellValueChanged;
            dgvInvoiceItems.EditingControlShowing += dgvInvoiceItems_EditingControlShowing;
        }

        // This will load product codes from GRN and put them in the DataGridViewComboBoxColumn's items
        private void LoadProductCodesIntoCombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductCode FROM GRN WHERE QuantityReceived > 0"; // only products in stock
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Find the combo column
                            var productCodeCol = dgvInvoiceItems.Columns["ProductCode"] as DataGridViewComboBoxColumn;
                            if (productCodeCol != null)
                            {
                                productCodeCol.Items.Clear();
                                while (reader.Read())
                                {
                                    productCodeCol.Items.Add(reader["ProductCode"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product codes: " + ex.Message);
            }
        }

        // Handle event when user changes a cell value
        private void DgvInvoiceItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvInvoiceItems.Columns[e.ColumnIndex].Name;

                if (columnName == "ProductCode")
                {
                    // User selected a product code, so populate Description and UnitPrice
                    string productCode = dgvInvoiceItems.Rows[e.RowIndex].Cells["ProductCode"].Value?.ToString();
                    if (!string.IsNullOrEmpty(productCode))
                    {
                        FillProductDetails(e.RowIndex, productCode);
                    }
                }
                else if (columnName == "QuantitySold")
                {
                    // Recalculate total for that row
                    CalculateRowTotal(e.RowIndex);
                }
            }
        }

        // Because DataGridViewComboBox changes require hooking into EditingControlShowing
        private void dgvInvoiceItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvInvoiceItems.CurrentCell.OwningColumn.Name == "ProductCode" && e.Control is ComboBox combo)
            {
                combo.SelectedIndexChanged -= Combo_SelectedIndexChanged;
                combo.SelectedIndexChanged += Combo_SelectedIndexChanged;
            }
        }

        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null && dgvInvoiceItems.CurrentCell != null)
            {
                dgvInvoiceItems.CurrentCell.Value = combo.Text;
            }
        }

        private void FillProductDetails(int rowIndex, string productCode)
        {
            // Query GRN table to get UnitPrice, Description, etc.
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            ProductDescription,
                            PricePerUnit
                        FROM GRN
                        WHERE ProductCode = @Code
                    ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Code", productCode);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dgvInvoiceItems.Rows[rowIndex].Cells["Description"].Value = reader["ProductDescription"].ToString();
                                dgvInvoiceItems.Rows[rowIndex].Cells["UnitPrice"].Value = reader["PricePerUnit"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product details: " + ex.Message);
            }
        }

        private void CalculateRowTotal(int rowIndex)
        {
            DataGridViewRow row = dgvInvoiceItems.Rows[rowIndex];
            if (!row.IsNewRow)
            {
                if (decimal.TryParse(row.Cells["UnitPrice"].Value?.ToString(), out decimal unitPrice) &&
                    int.TryParse(row.Cells["QuantitySold"].Value?.ToString(), out int qty))
                {
                    decimal total = unitPrice * qty;
                    row.Cells["TotalPrice"].Value = total.ToString("F2");
                }
                else
                {
                    row.Cells["TotalPrice"].Value = "0.00";
                }
            }

            // Optionally recalc the invoice totals (below)
            CalculateInvoiceTotals();
        }

        private void CalculateInvoiceTotals()
        {
            decimal totalBeforeDisc = 0;
            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                if (!row.IsNewRow &&
                    decimal.TryParse(row.Cells["TotalPrice"].Value?.ToString(), out decimal rowTotal))
                {
                    totalBeforeDisc += rowTotal;
                }
            }

            lblTotalBeforeDisc.Text = totalBeforeDisc.ToString("F2");

            // Subtract discount if any
            if (decimal.TryParse(txtDiscount.Text, out decimal discount))
            {
                decimal totalAfterDisc = totalBeforeDisc - discount;
                lblTotalAfterDisc.Text = totalAfterDisc.ToString("F2");
            }
            else
            {
                lblTotalAfterDisc.Text = totalBeforeDisc.ToString("F2");
            }
        }

        private void ClearForm()
        {
            txtInvoiceNumber.Text = GenerateInvoiceNumber();
            txtCustomerName.Clear();
            dtpSaleDate.Value = DateTime.Now;
            txtDiscount.Text = "0.00";
            lblTotalBeforeDisc.Text = "0.00";
            lblTotalAfterDisc.Text = "0.00";
            dgvInvoiceItems.Rows.Clear();
        }

        private void btnSaveInvoice_Click_1(object sender, EventArgs e)
        {
            // 1. Gather header info
            string invoiceNumber = txtInvoiceNumber.Text.Trim();  // auto-generated
            string customerName = txtCustomerName.Text.Trim();
            DateTime dateOfSale = dtpSaleDate.Value;

            if (decimal.TryParse(lblTotalBeforeDisc.Text, out decimal totalBeforeDisc) == false)
                totalBeforeDisc = 0;
            if (decimal.TryParse(txtDiscount.Text, out decimal discount) == false)
                discount = 0;
            if (decimal.TryParse(lblTotalAfterDisc.Text, out decimal totalAfterDisc) == false)
                totalAfterDisc = 0;

            // Basic validation
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter a customer name.");
                return;
            }

            // 2. Prepare to insert data into SalesInvoiceHeader + SalesInvoiceDetails + update GRN stock
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert header
                    string insertHeaderSql = @"
                        INSERT INTO SalesInvoiceHeader
                        (InvoiceNumber, CustomerName, DateOfSale, Discount, TotalBeforeDisc, TotalAfterDisc)
                        OUTPUT INSERTED.InvoiceID
                        VALUES
                        (@InvoiceNumber, @CustomerName, @DateOfSale, @Discount, @TotalBeforeDisc, @TotalAfterDisc)
                    ";

                    int newInvoiceID;
                    using (SqlCommand cmdHeader = new SqlCommand(insertHeaderSql, conn, transaction))
                    {
                        cmdHeader.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
                        cmdHeader.Parameters.AddWithValue("@CustomerName", customerName);
                        cmdHeader.Parameters.AddWithValue("@DateOfSale", dateOfSale);
                        cmdHeader.Parameters.AddWithValue("@Discount", discount);
                        cmdHeader.Parameters.AddWithValue("@TotalBeforeDisc", totalBeforeDisc);
                        cmdHeader.Parameters.AddWithValue("@TotalAfterDisc", totalAfterDisc);

                        newInvoiceID = (int)cmdHeader.ExecuteScalar();
                    }

                    // Insert details
                    foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // Gather line item data
                        string productCode = row.Cells["ProductCode"].Value?.ToString();
                        string description = row.Cells["Description"].Value?.ToString();
                        if (!int.TryParse(row.Cells["QuantitySold"].Value?.ToString(), out int qtySold))
                            qtySold = 0;
                        if (!decimal.TryParse(row.Cells["UnitPrice"].Value?.ToString(), out decimal unitPrice))
                            unitPrice = 0;
                        if (!decimal.TryParse(row.Cells["TotalPrice"].Value?.ToString(), out decimal lineTotal))
                            lineTotal = 0;

                        // Insert into details table
                        string insertDetailSql = @"
                            INSERT INTO SalesInvoiceDetails
                            (InvoiceID, ProductCode, Description, QuantitySold, UnitPrice, TotalPrice)
                            VALUES
                            (@InvoiceID, @ProductCode, @Description, @QuantitySold, @UnitPrice, @TotalPrice)
                        ";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, conn, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@InvoiceID", newInvoiceID);
                            cmdDetail.Parameters.AddWithValue("@ProductCode", productCode ?? "");
                            cmdDetail.Parameters.AddWithValue("@Description", description ?? "");
                            cmdDetail.Parameters.AddWithValue("@QuantitySold", qtySold);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", unitPrice);
                            cmdDetail.Parameters.AddWithValue("@TotalPrice", lineTotal);
                            cmdDetail.ExecuteNonQuery();
                        }

                        // 3. Deduct stock from GRN
                        // Assuming GRN.CurrentStock is the field that stores how many are currently in stock
                        string updateGrnStockSql = @"
                            UPDATE GRN
                            SET QuantityReceived = QuantityReceived - @QtySold
                            WHERE ProductCode = @ProductCode
                        ";

                        using (SqlCommand cmdUpdate = new SqlCommand(updateGrnStockSql, conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@QtySold", qtySold);
                            cmdUpdate.Parameters.AddWithValue("@ProductCode", productCode ?? "");
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    // Commit transaction
                    transaction.Commit();

                    MessageBox.Show("Invoice saved successfully!");

                    // Optionally clear the form for a new invoice
                    ClearForm();
                }
                catch (Exception ex)
                {
                    // Rollback on error
                    transaction.Rollback();
                    MessageBox.Show("Error saving invoice: " + ex.Message);
                }
            }
        }

        private void txtDiscount_TextChanged_1(object sender, EventArgs e)
        {
            // Recalculate total after discount whenever discount changes
            CalculateInvoiceTotals();
        }

        private void dgvInvoiceItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
