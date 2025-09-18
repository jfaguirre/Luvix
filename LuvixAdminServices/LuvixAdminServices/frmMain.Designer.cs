namespace LuvixAdminServices
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlEncabezado = new Panel();
            pnlMenuLateral = new Panel();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(869, 57);
            pnlEncabezado.TabIndex = 1;
            // 
            // pnlMenuLateral
            // 
            pnlMenuLateral.BackColor = Color.FromArgb(76, 29, 149);
            pnlMenuLateral.Dock = DockStyle.Left;
            pnlMenuLateral.Location = new Point(0, 57);
            pnlMenuLateral.Name = "pnlMenuLateral";
            pnlMenuLateral.Size = new Size(189, 569);
            pnlMenuLateral.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(232, 88);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(581, 186);
            dataGridView1.TabIndex = 3;
            // 
            // frmMain
            // 
            ClientSize = new Size(770, 520);
            Name = "frmMain";
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            // 
            // frmMain
            // 

        }

        #endregion

        private Panel pnlEncabezado;
        private Panel pnlMenuLateral;
        private DataGridView dataGridView1;
    }
}
