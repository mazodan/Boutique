using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MagnumOpusTheVisual
{
    public partial class Cashier : Form
    {
        int total = 0;
        int change = 0;

        public Cashier()
        {
            InitializeComponent();
            txtReceipt.SelectionAlignment = HorizontalAlignment.Center;
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
