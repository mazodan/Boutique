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
    public partial class DBmodifier : Form
    {
        public DBmodifier()
        {
            InitializeComponent();
        }
        DBClass DB = new DBClass();                 //CREATE AN OBJECT FOR THE CLASS DBClass
        public MySqlConnection Conn { get; set; }   //CONNECTION STRING FROM THE OTHER FORM
        

        public string Command { get; set; }  //THIS IS CALLED A PROPERTY, ALLOWS SAFE MODIFICATION TO PRIVATE CONTROLS
                                //THANKS TO THE OOP FEATURE CALLED ENCAPSULATION, TO AVOID DETECTION BY REVERSE ENGINEERS

        private void DBmodifier_Load(object sender, EventArgs e)
        {
            lblHelp.Text = Command;     //PROPERTY SET TO THE LABEL, DON'T HAVE TO MAKE LABEL PUBLIC, JUST PASS THE VALUE
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtName.Text == "" || txtPrice.Text == "" || txtQty.Text == "")
            {
                MessageBox.Show("Enter all the Fields");
            }
            else 
            {
                Conn.Open();
                DB.insert("inventory", txtID.Text, txtName.Text, txtQty.Text, txtPrice.Text, Conn); //SETS THE PARAMETERS                
                Conn.Close();
                if (DB.Success == true)     //CHECKS THE STATUS OF THE QUERY EXECUTION, IF TRUE, THE FORM CLOSES AND REFRESH THE DATAGRIDVIEW
                {
                    this.Close();
                }
            }
        }

    }
}
