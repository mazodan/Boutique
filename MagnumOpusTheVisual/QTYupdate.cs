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
    public partial class QTYupdate : Form
    {
        public QTYupdate()
        {
            InitializeComponent();
        }

        DBClass DB = new DBClass();                 //CREATE AN OBJECT FOR THE CLASS DBClass
        public MySqlConnection Conn { get; set; }   //CONNECTION STRING FROM THE OTHER FORM

        public string QTY { get; set; } //GET QUANTTY
        public string ItemID { get; set; } //GET ITEM ID
        public string NameOfItem { get; set; } //GET ITEM NAME

        private void QTYupdate_Load(object sender, EventArgs e)
        {
            txtQTY.Text = QTY;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Conn.Open();
            DB.update("inventory", ItemID, txtQTY.Text, Conn);
            Conn.Close();
            if (DB.Success == true)
            {
                StateCheck.logicSwitch = true;
                StateCheck.searchItem = NameOfItem;
                this.Close();
            }
        } 
    }
}
