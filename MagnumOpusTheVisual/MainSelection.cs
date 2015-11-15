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

        private void button1_Click(object sender, EventArgs e)
        {
            Inventory I = new Inventory();      //SHOW INVENTORY FORM, ACCESS INVENTORY
            I.Show();                           //BY CREATING A NEW OBJECT THAT REPRESENTS THE FORM
            this.Hide();
        }

        private void MainSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();                 //MAKE SURE APPLICATION CLOSES IF CLOSED BY USER
        }



    }
}
