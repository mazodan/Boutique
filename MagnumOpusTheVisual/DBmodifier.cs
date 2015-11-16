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
        /*
         * THIS FORM ALLOWS INSERT/UPDATE OF FILES
         */


        public DBmodifier()
        {
            InitializeComponent();
        }
        DBClass DB = new DBClass();                 //CREATE AN OBJECT FOR THE CLASS DBClass
        public MySqlConnection Conn { get; set; }   //CONNECTION STRING FROM THE OTHER FORM
        public string Caption { get; set; }     //GETS THE CAPTION WHENEVER UPDATE OR ADD;
        public string Command { get; set; }  //THIS IS CALLED A PROPERTY, ALLOWS SAFE MODIFICATION TO PRIVATE CONTROLS
                                //THANKS TO THE OOP FEATURE CALLED ENCAPSULATION, TO AVOID DETECTION BY REVERSE ENGINEERS
        
        public string ID { get; set; }          //DECLARES PROPERTIES TO GET VALUES SELECTED FROM DATABASES
        public string itemname { get; set; }
        public string QTY { get; set; }
        public string Price { get; set; }

        private void DBmodifier_Load(object sender, EventArgs e)
        {
            btnExecute.Text = Caption;
            lblHelp.Text = Command;     //PROPERTY SET TO THE LABEL, DON'T HAVE TO MAKE LABEL PUBLIC, JUST PASS THE VALUE

            if (btnExecute.Text == "Update")    //WHILE FORM LOADS, CHECKS IF ADDING OR UPDATING
            {
                txtID.Text = ID;
                txtName.Text = itemname;        //IF UPDATING, DISABLES ID AND SETS SELECTED FROM DGV TO THE TEXTBOXES
                txtQty.Text = QTY;
                txtPrice.Text = Price;

                txtID.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtName.Text == "" || txtPrice.Text == "" || txtQty.Text == "")
            {
                MessageBox.Show("Enter all the Fields");
            }
            else 
            {
                if (btnExecute.Text == "Add Item")      //CHECKS IF CAPTION UPDATES OR ADD DEPENDING ON BUTTON CAPTION
                {
                    Conn.Open();
                    DB.insert("inventory", txtID.Text, txtName.Text, txtQty.Text, txtPrice.Text, Conn); //SETS THE PARAMETERS                
                    Conn.Close();
                }
                else if (btnExecute.Text == "Update")
                {
                    Conn.Open();
                    DB.update("inventory", txtID.Text, txtName.Text, txtQty.Text, txtPrice.Text, Conn);
                    Conn.Close();
                }


                if (DB.Success == true)     //CHECKS THE STATUS OF THE QUERY EXECUTION, IF TRUE, THE FORM CLOSES AND REFRESH THE DATAGRIDVIEW
                {
                    StateCheck.logicSwitch = true;      //THIS RETURNS THAT THE USER HAS DONE SOMETHING
                    StateCheck.searchItem = txtName.Text;
                    this.Close();
                }
            }
        }



    }
}
