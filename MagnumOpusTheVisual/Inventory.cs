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
        DBClass DB = new DBClass();     //DECLARES A NEW OBJECT
        public MySqlConnection conn;           //DECLARES A NEW MYSQL CONNECTION

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

        public void Reload()
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DBmodifier D = new DBmodifier();    //GETS OBJECT OF THAT FORM
            D.Command = "TO ADD ITEMS TO DATABASE\nPLEASE FILL ALL THE FIELDS\nAND CLICK ADD ITEM"; //DYNAMIC HELP
            D.Conn = conn;    //PASS THE SQL CONNECTION TO THE OTHER FORM WITHOUT DECLARING ANOTHER CONNECTION FUNCTION            

            //THE NEXT LINES ALLOW TO REFRESH THE FORM AFTER INSERTING/EDITING
            var child = D;                                  //DECLARES VARIABLE OF CHILD TO THE NEW OBJECT
            child.FormClosed += ChildFormClosed;            //IF CHILD FORM IS CLOSED
            child.ShowDialog();                             //THIS EXECUTES
        }

        void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            Reload();       //AFTER INSERT OR UPDATE, THAT FORM CLOSES, IT SENDS A COMMAND TO REFRESH THE DATABASE
        }
       

        
    }
}
