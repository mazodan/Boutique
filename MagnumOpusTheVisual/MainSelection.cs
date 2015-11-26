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
    public partial class MainSelection : Form
    {
        public MainSelection()
        {
            InitializeComponent();
        }

        private void MainSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();                 //MAKE SURE APPLICATION CLOSES IF CLOSED BY USER
        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            Login L = new Login();  //Login Object Initialize
            L.selection = "Inventory";  //LET LOGIN.CS KNOW THAT USER CLICKED INVENTORY
            L.ShowDialog();         //Show Login form
            if (StateCheck.logicSwitch == true)
            {
                StateCheck.logicSwitch = false;
                this.Hide();
            }
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            Login L = new Login();  //Login Object Initialize
            L.selection = "Cashier";  //LET LOGIN.CS KNOW THAT USER CLICKED INVENTORY
            L.ShowDialog();         //Show Login form
            if (StateCheck.logicSwitch == true)
            {
                StateCheck.logicSwitch = false;
                this.Hide();
            }
        }

        



    }
}
