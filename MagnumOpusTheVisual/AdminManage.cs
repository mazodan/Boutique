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
    public partial class AdminManage : Form
    {
        public MySqlConnection conn { get; set; }
        DBClass DB = new DBClass();

        public AdminManage()
        {
            InitializeComponent();
        }

        private void AdminManage_Load(object sender, EventArgs e)
        {
            conn = DB.sqlConnect("localhost", "iteminventory", "root", "root"); //EDIT CREDENTIALS HERE
            try
            {
                conn.Open();            //CONNECTS TO THE MYSQL DATABASE 
            }
            catch (MySqlException ex)   //CATCH DEM EXCEPTIONS, 
            {
                MessageBox.Show(ex.Message);
            }
            Reload();
            conn.Close();   //CLOSE THE CONNECTION TO AVOID OVERCROWDING, THINK OF THE ENERGY SAVED BY NOT CONNECTING
            //ALL THE TIME!!!!! CPU POWER SAVED, THIS PROGRAM IS ECO-FRIENDLY

            //MAKES ROWS NOT SORTABLE, PREVENTS EXECPTIONS, 
            dgvUserInfo.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        private void Reload()
        {
            string query = "SELECT username, firstname, lastname FROM security;";  //SELECTS ALL ITEMS FROM TABLE
            DB.reload(conn, query, "inventory", dgvUserInfo);  //RELOADS THE CURRENT DATABASE BY PASSING query TO MYSQLCOMMAND
            conn.Close();
            dgvUserInfo.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            DB.search(conn, txtSearch.Text, "security", dgvUserInfo); //SEARCHES THE DATABASE, RETURNS TO DATAGRIDVIEW
            conn.Close();  //NAME OF TABLE, TEXT TO BE SEARCHED, CONNECTION STRING AND DATAGRIDVIEW TO DISPLAY RESULT
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModify UM = new UserModify();
            UM.conn = conn;


        }
    }
}
