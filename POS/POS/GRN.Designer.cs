namespace POS
{
    partial class GRN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGRNNumber = new System.Windows.Forms.TextBox();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.btnCalculateTotal = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpDateReceived = new System.Windows.Forms.DateTimePicker();
            this.dgvGRN = new System.Windows.Forms.DataGridView();
            this.posDataSet = new POS.posDataSet();
            this.productsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.productsTableAdapter = new POS.posDataSetTableAdapters.ProductsTableAdapter();
            this.txtProductDescription = new System.Windows.Forms.TextBox();
            this.txtQuantityReceived = new System.Windows.Forms.TextBox();
            this.txtPricePerUnit = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add GRNs";
            // 
            // txtGRNNumber
            // 
            this.txtGRNNumber.Location = new System.Drawing.Point(185, 50);
            this.txtGRNNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGRNNumber.Name = "txtGRNNumber";
            this.txtGRNNumber.Size = new System.Drawing.Size(76, 20);
            this.txtGRNNumber.TabIndex = 1;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(320, 50);
            this.txtSupplierName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(76, 20);
            this.txtSupplierName.TabIndex = 2;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(185, 84);
            this.txtProductCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(76, 20);
            this.txtProductCode.TabIndex = 3;
            // 
            // btnCalculateTotal
            // 
            this.btnCalculateTotal.Location = new System.Drawing.Point(288, 208);
            this.btnCalculateTotal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCalculateTotal.Name = "btnCalculateTotal";
            this.btnCalculateTotal.Size = new System.Drawing.Size(124, 19);
            this.btnCalculateTotal.TabIndex = 4;
            this.btnCalculateTotal.Text = "Calculate Total";
            this.btnCalculateTotal.UseVisualStyleBackColor = true;
            this.btnCalculateTotal.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(157, 208);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 19);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save GRN";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveGRN_Click);
            // 
            // dtpDateReceived
            // 
            this.dtpDateReceived.Location = new System.Drawing.Point(110, 160);
            this.dtpDateReceived.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpDateReceived.Name = "dtpDateReceived";
            this.dtpDateReceived.Size = new System.Drawing.Size(151, 20);
            this.dtpDateReceived.TabIndex = 6;
            // 
            // dgvGRN
            // 
            this.dgvGRN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGRN.Location = new System.Drawing.Point(92, 262);
            this.dgvGRN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvGRN.Name = "dgvGRN";
            this.dgvGRN.RowTemplate.Height = 24;
            this.dgvGRN.Size = new System.Drawing.Size(448, 139);
            this.dgvGRN.TabIndex = 7;
            this.dgvGRN.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGRN_CellContentClick);
            // 
            // posDataSet
            // 
            this.posDataSet.DataSetName = "posDataSet";
            this.posDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productsBindingSource
            // 
            this.productsBindingSource.DataMember = "Products";
            this.productsBindingSource.DataSource = this.posDataSet;
            // 
            // productsTableAdapter
            // 
            this.productsTableAdapter.ClearBeforeFill = true;
            // 
            // txtProductDescription
            // 
            this.txtProductDescription.Location = new System.Drawing.Point(320, 84);
            this.txtProductDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProductDescription.Name = "txtProductDescription";
            this.txtProductDescription.Size = new System.Drawing.Size(76, 20);
            this.txtProductDescription.TabIndex = 8;
            // 
            // txtQuantityReceived
            // 
            this.txtQuantityReceived.Location = new System.Drawing.Point(185, 121);
            this.txtQuantityReceived.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtQuantityReceived.Name = "txtQuantityReceived";
            this.txtQuantityReceived.Size = new System.Drawing.Size(76, 20);
            this.txtQuantityReceived.TabIndex = 9;
            // 
            // txtPricePerUnit
            // 
            this.txtPricePerUnit.Location = new System.Drawing.Point(320, 121);
            this.txtPricePerUnit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPricePerUnit.Name = "txtPricePerUnit";
            this.txtPricePerUnit.Size = new System.Drawing.Size(76, 20);
            this.txtPricePerUnit.TabIndex = 10;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(320, 160);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(76, 20);
            this.txtTotalAmount.TabIndex = 12;
            // 
            // GRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 455);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtPricePerUnit);
            this.Controls.Add(this.txtQuantityReceived);
            this.Controls.Add(this.txtProductDescription);
            this.Controls.Add(this.dgvGRN);
            this.Controls.Add(this.dtpDateReceived);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCalculateTotal);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.txtSupplierName);
            this.Controls.Add(this.txtGRNNumber);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GRN";
            this.Text = "GRN";
            this.Load += new System.EventHandler(this.GRN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGRNNumber;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Button btnCalculateTotal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDateReceived;
        private System.Windows.Forms.DataGridView dgvGRN;
        private posDataSet posDataSet;
        private System.Windows.Forms.BindingSource productsBindingSource;
        private posDataSetTableAdapters.ProductsTableAdapter productsTableAdapter;
        private System.Windows.Forms.TextBox txtProductDescription;
        private System.Windows.Forms.TextBox txtQuantityReceived;
        private System.Windows.Forms.TextBox txtPricePerUnit;
        private System.Windows.Forms.TextBox txtTotalAmount;
    }
}