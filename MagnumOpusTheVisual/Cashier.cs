using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace MagnumOpusTheVisual
{
    public partial class Cashier : Form
    {
        decimal total = 0;
        decimal change = 0;
        int currentRow;
        MySqlConnection conn = new MySqlConnection();
        DBClass DB = new DBClass();

        public Cashier()
        {
            InitializeComponent();
        }

        private void Cashier_Load(object sender, EventArgs e)
        {
            lblTotal.Text = total.ToString();
            lblChange.Text = change.ToString();
            conn = DB.sqlConnect("localhost", "iteminventory", "root", "root"); //EDIT CREDENTIALS HERE
            try
            {
                conn.Open();            //CONNECTS TO THE MYSQL DATABASE 
            }
            catch (MySqlException ex)   //CATCH EXCEPTIONS!
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            txtReceipt.Text = DB.ReceiptIntro();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MainSelection M = new MainSelection();
            M.Show();
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            CashHelp C = new CashHelp();
            C.ShowDialog();
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            DB.search("inventory", txtItem.Text, dgvSearch, conn);
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                total += decimal.Parse(dgvSearch.Rows[currentRow].Cells[2].Value.ToString()) * decimal.Parse(txtQ.Text);
                lblTotal.Text = total.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Select the Item and Enter an Integer for the Quantity, Please","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string txt = txtReceipt.Text + "\n";
            decimal presyo = decimal.Parse(dgvSearch.Rows[currentRow].Cells[2].Value.ToString()) * decimal.Parse(txtQ.Text);
            txtReceipt.Text = txt + "\n" + DB.InsertItem(dgvSearch.Rows[currentRow].Cells[1].Value.ToString(), presyo.ToString(), txtQ.Text);

            
            
        }

        private void dgvSearch_SelectionChanged(object sender, EventArgs e)
        {
            currentRow = dgvSearch.CurrentCell.RowIndex;    //SETS ROW INDEX WHEN CELL IS CHANGED
        }

        private void btnComp_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnPrint.Enabled = true;


        }


        
        
        
        
        
        /*
        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f = new Font("arial", 14, FontStyle.Bold);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(richTextBox1.Text,f,Brushes.Black,250,100,StringFormat.GenericTypographic);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        */

        
    }
}
