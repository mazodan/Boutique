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
    public partial class Cashout : Form
    {
        public decimal Amount { get; set; }

        public Cashout()
        {
            InitializeComponent();
        }

        private void Cashout_Load(object sender, EventArgs e)
        {
            lblAmt.Text = Amount.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtPay.Text) < Amount)
                {
                    MessageBox.Show("Insufficient payment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StateCheck.AmtPaid = (decimal.Parse(txtPay.Text));
                    StateCheck.Change = (decimal.Parse(txtPay.Text) - Amount);
                    StateCheck.VAT = (Amount * 0.12M);
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter an Integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
