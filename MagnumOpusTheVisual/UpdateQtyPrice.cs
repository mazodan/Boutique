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
    public partial class UpdateQtyPrice : Form
    {
        /*
         * ALLOWS UPDATE TO THE QUANTITY AND PRICE TO THE PROGRAM
         */

        public UpdateQtyPrice()
        {
            InitializeComponent();
        }

        DBClass DB = new DBClass();                 //CREATE AN OBJECT FOR THE CLASS DBClass
        public MySqlConnection Conn { get; set; }   //CONNECTION STRING FROM THE OTHER FORM

        public string Instruction { get; set; }     //RECEIVE INSTRUCTION
        public string ButtonCaption { get; set; }   //GET BUTTON CAPTION

        public string QTY { get; set; } //GET QUANTTY
        public string ItemID { get; set; } //GET ITEM ID
        public string NameOfItem { get; set; } //GET ITEM NAME
        public string Price { get; set; }   //GET PRICE

        private void QTYupdate_Load(object sender, EventArgs e)
        {
            btnUpdate.Text = ButtonCaption;
            label1.Text = Instruction;
                                        //THIS IS FROM C, LEGACY WAY OF IF STATEMENTS

                                //CONDITION                     //TRUE  //FALSE
            txtQTY.Text = (btnUpdate.Text == "Update Quantity") ? QTY : Price;  //IF STATEMENT TERNARY OPERATOR
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Text == "Update Quantity")
            {
                Conn.Open();
                DB.update("inventory", ItemID, txtQTY.Text, Conn);
                Conn.Close();
            }
            if (DB.Success == true)
            {
                StateCheck.logicSwitch = true;
                StateCheck.searchItem = NameOfItem;
                this.Close();
            }
        } 
    }
}
