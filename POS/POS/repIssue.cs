using System;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Windows.Forms;

namespace POS
{
    public partial class repIssue : UserControl
    {
        public repIssue()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save Invoice PDF";
                saveFileDialog.FileName = "Invoice.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        GeneratePdf(saveFileDialog.FileName);
                        MessageBox.Show("PDF generated and downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void GeneratePdf(string filePath)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Invoice PDF";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 10);
            XFont boldFont = new XFont("Verdana", 10);

            // Header section
            gfx.DrawString("Automodeal (Pvt) Ltd.", boldFont, XBrushes.Black, new XRect(20, 20, page.Width, 20), XStringFormats.TopLeft);
            gfx.DrawString("250/12, Makola South, Makola.", font, XBrushes.Black, new XRect(20, 40, page.Width, 20), XStringFormats.TopLeft);
            gfx.DrawString("Phone: 0703384343 / 0777384343", font, XBrushes.Black, new XRect(20, 60, page.Width, 20), XStringFormats.TopLeft);
            gfx.DrawString("Email: sugathpialitha1970@gmail.com", font, XBrushes.Black, new XRect(20, 80, page.Width, 20), XStringFormats.TopLeft);

            // Invoice details
            gfx.DrawString("Invoice No: 3761", boldFont, XBrushes.Black, new XRect(page.Width - 150, 20, 130, 20), XStringFormats.TopRight);
            gfx.DrawString("Date: 30/10/2024", font, XBrushes.Black, new XRect(page.Width - 150, 40, 130, 20), XStringFormats.TopRight);

            // Table headers
            int startX = 20;
            int startY = 120;
            int[] columnWidths = { 50, 70, 70, 100, 150, 50, 70, 70 }; // Adjust widths as per requirement
            string[] headers = { "No", "Brand", "Part No", "Model", "Description", "Qty", "Unit Price" };

            int currentX = startX;
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XPens.Black, currentX, startY, columnWidths[i], 30);
                gfx.DrawString(headers[i], boldFont, XBrushes.Black, new XRect(currentX, startY, columnWidths[i], 30), XStringFormats.Center);
                currentX += columnWidths[i];
            }

            // Sample data rows
            string[,] data = {
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
                { "1", "WELLFAR", "558W001", "RAY-0.25", "PISTON KIT", "10", "3950" },
                { "2", "WELLFAR", "558W002", "RAY-0.50", "PISTON KIT", "5", "3950" },
      
       

                // Add more rows as needed
            };

            int rowY = startY + 30;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                currentX = startX;
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    gfx.DrawRectangle(XPens.Black, currentX, rowY, columnWidths[j], 30);
                    gfx.DrawString(data[i, j], font, XBrushes.Black, new XRect(currentX, rowY, columnWidths[j], 30), XStringFormats.Center);
                    currentX += columnWidths[j];
                }
                rowY += 30;
            }

            // Footer section
            gfx.DrawString("Total: Rs. 274125.00", boldFont, XBrushes.Black, new XRect(page.Width - 150, rowY + 20, 130, 20), XStringFormats.TopRight);
            gfx.DrawString("Discount 30%: Rs. 82237.50", font, XBrushes.Black, new XRect(page.Width - 150, rowY + 40, 130, 20), XStringFormats.TopRight);
            gfx.DrawString("Net Total: Rs. 184887.50", boldFont, XBrushes.Black, new XRect(page.Width - 150, rowY + 60, 130, 20), XStringFormats.TopRight);

            // Save the document
            document.Save(filePath);

            // Open the PDF after saving (optional)
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
    }
}
