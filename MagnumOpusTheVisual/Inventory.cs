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
    public partial class Inventory : Form
    {
        /*
         * THIS FORM ALLOWS MANIPULATION OF THE INVENTORY, ANY CHANGES HERE WILL PASS
         * TO THE CASHIER
         */

        //C# > VB.NET
        DBmodifier D = new DBmodifier();    //GETS OBJECT OF THAT FORM DBMODIFIER
        DBClass DB = new DBClass();     //DECLARES A NEW OBJECT
        public MySqlConnection conn;           //DECLARES A NEW MYSQL CONNECTION
        int currentRow;                 //STORES THE CURRENT ROW

        UpdateQtyPrice Q = new UpdateQtyPrice(); //NEW OBJECT FOR THE QTYEDIT FORM

        public Inventory()
        {
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            conn = DB.sqlConnect("localhost", "iteminventory", "root", "root"); //EDIT CREDENTIALS HERE
            try
            {
                conn.Open();            //CONNECTS TO THE MYSQL DATABASE (MYSQL IS OUTDATED, CAN WE USE SQL REFERENCES?)
            }
            catch (MySqlException ex)   //CATCH DEM EXCEPTIONS, FYEAH!!!!
            {
                MessageBox.Show(ex.Message);
            }
            Reload();
            conn.Close();   //CLOSE THE CONNECTION TO AVOID OVERCROWDING, THINK OF THE ENERGY SAVED BY NOT CONNECTING
                            //ALL THE TIME!!!!! CPU POWER SAVED, THIS PROGRAM IS ECO-FRIENDLY
        }

        private void Reload()
        {
            string query = "SELECT * FROM inventory";           //SELECTS ALL ITEMS FROM TABLE
            DB.reload(conn, query, "inventory", dgvInventory);  //RELOADS THE CURRENT DATABASE BY PASSING query TO MYSQLCOMMAND
            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            DB.search("inventory", txtSearch.Text, conn, dgvInventory); //SEARCHES THE DATABASE, RETURNS TO DATAGRIDVIEW
            conn.Close();  //NAME OF TABLE, TEXT TO BE SEARCHED, CONNECTION STRING AND DATAGRIDVIEW TO DISPLAY RESULT
        }

        

        private void btnMenu_Click(object sender, EventArgs e)  //IF Clicked, GOES BACK TO MAIN MENU
        {
            MainSelection M = new MainSelection();
            M.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)  //THIS EXITS THE APP
        {
            Application.Exit();
        }       
        
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            D.Command = "TO ADD ITEMS TO DATABASE\nPLEASE FILL ALL THE FIELDS\nAND CLICK ADD ITEM"; //DYNAMIC HELP
            D.Conn = conn;    //PASS THE SQL CONNECTION TO THE OTHER FORM WITHOUT DECLARING ANOTHER CONNECTION FUNCTION            
            D.Caption = "Add Item";     //PASS THE APPROPRIATE CAPTION
            //THE NEXT LINES ALLOW TO REFRESH THE FORM AFTER INSERTING/EDITING
            var child = D;                                  //DECLARES VARIABLE OF CHILD TO THE NEW OBJECT
            child.FormClosed += ChildFormClosed;            //IF CHILD FORM IS CLOSED THIS EXECUTES
            child.ShowDialog();                             
        }

        void ChildFormClosed(object sender, FormClosedEventArgs e)      //EXECUTES IF CHILD FORM CLOSES
        {
            if (StateCheck.logicSwitch == true) //IF USER HAS ADDED DATA
            {
                DB.search("inventory", StateCheck.searchItem, conn, dgvInventory);
                //AFTER INSERT OR UPDATE, THAT FORM CLOSES, IT SENDS A COMMAND TO SEARCH THE DATABASE
                //OF THE ITEM THAT THE USER INSERTED TO ENSURE THE DATA HAS BEEN ENTERED PROPERLY

                StateCheck.searchItem = string.Empty;   //RESETS THE STRING
                StateCheck.logicSwitch = false;     //RESETS THE SWITCH    
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            D.Command = "TO UPDATE, EDIT THE VALUES\nCLICK UPDATE BUTTON, \nNO EMPTY FIELDS!!!";
            D.Caption = "Update";    //SETS THE CAPTION TO UPDATE   
            D.Conn = conn;
            D.ID = dgvInventory.Rows[currentRow].Cells[0].Value.ToString();         //PASS SELECTED FORMS TO UPDATE FORM
            D.itemname = dgvInventory.Rows[currentRow].Cells[1].Value.ToString();
            D.QTY = dgvInventory.Rows[currentRow].Cells[2].Value.ToString();
            D.Price = dgvInventory.Rows[currentRow].Cells[3].Value.ToString();
            var child = D;
            child.FormClosed += ChildFormClosed;
            child.ShowDialog();
        }

        private void btnEQty_Click(object sender, EventArgs e)
        {
            
            Q.Text = "Update Quantity";             //CHANGE THE FORM TITLE
            Q.Instruction = "Update the Quantity";  //SEND INSTRUCTIONS
            Q.ButtonCaption = "Update Quantity";    //SEND THE CAPTION FOR COMPARISON
            Q.Conn = conn;
            Q.ItemID = dgvInventory.Rows[currentRow].Cells[0].Value.ToString();     //GET REFERENCE
            Q.NameOfItem = dgvInventory.Rows[currentRow].Cells[1].Value.ToString(); //TO IDENTIFY THE FORM UPDATED
            Q.QTY = dgvInventory.Rows[currentRow].Cells[2].Value.ToString();
            var child = Q;
            child.FormClosed += ChildFormClosed;
            child.ShowDialog();
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            Q.Text = "Update Price";      //SAME AS THE UPDATE QUANTITY BUTTON EVENT, LOOK UP
            Q.Instruction = "Update the Price";
            Q.ButtonCaption = "Update Price";
            Q.Conn = conn;
            Q.ItemID = dgvInventory.Rows[currentRow].Cells[0].Value.ToString();     //GET REFERENCE
            Q.NameOfItem = dgvInventory.Rows[currentRow].Cells[1].Value.ToString(); //TO IDENTIFY THE FORM UPDATED
            Q.Price = dgvInventory.Rows[currentRow].Cells[3].Value.ToString();
            var child = Q;
            child.FormClosed += ChildFormClosed;
            child.ShowDialog();
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRow = e.RowIndex;    //SETS ROW INDEX WHEN CELL IS CHANGED
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            currentRow = dgvInventory.CurrentCell.RowIndex;     //AFTER INSERT/UPDATE, GETS NEW SELECTED ROW
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();        //GETS THE ID
            string ID = dgvInventory.Rows[currentRow].Cells[0].Value.ToString();
            if (MessageBox.Show("This will delete the Item, Click OK to Proceed", "Delete Item",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DB.delete("inventory", ID, conn);    //DELETE ITEM
                conn.Close();                           //CLOSES CONNECTION
                Reload();                               //RELOADS THE DATAGRIDVIEW
            }
            else
            {
                conn.Close();
            }
                                       
            

        }

        

        



        

        

        
       

        
    }
}
