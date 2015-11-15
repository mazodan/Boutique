using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace MagnumOpusTheVisual
{
    class DBClass
    {          //INITIALIZE THE DATABASE SQL CREDENTIALS; MAKE IT EDITABLE FOR THESIS TIER PROJECT
        public MySqlConnection sqlConnect(string server, string dbase, string user, string pwrd)
        {
            MySqlConnection conn = new MySqlConnection(
                "server='" + server + "';Database="+ dbase +";User ID=" + user +";Password=" + pwrd);
            return conn;
        }
               //THIS CREATES AN ADAPTER TO FILL ALL ITEMS TO THE DATAGRIDVIEW
        public void reload(MySqlConnection conn, string query, string table, DataGridView dgv)
        {
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, conn); //CREATE NEW DATA ADAPTER
            DataSet dset = new DataSet();                               //CREATE NEW DATASET
            adapt.Fill(dset, table);                                    //GETS THE DATA FROM TABLE
            dgv.DataSource = dset.Tables[table].DefaultView;            //THEN PUTS IT TO DATAGRID
        }

        public void search(string table, string parameter, MySqlConnection conn, DataGridView dgv)
        {
            string query = "SELECT * FROM " + table + " WHERE Name LIKE @param;";   //DECLARES THE QUERY
            string searchTerm = string.Format("%{0}%", parameter); //THIS SETS THE FORMAT, REMEMBER! PARAMETER IS SET TO {0} ARGUMENT
                                                                   //THE LIKE OPERATOR ACCEPTS % AS WILDCARDS, GOOD FOR SEARCH FUNCTION
            MySqlCommand comm = new MySqlCommand(query, conn);
            comm.Parameters.Add(new MySqlParameter("@param", searchTerm));  //QUERY IS PARAMETERIZED, NO SQL INJECTIONS HERE
                                                                            //NOPE :D, searchTerms SUBSTITUTES @param IN QUERY

            MySqlDataAdapter adapt = new MySqlDataAdapter(comm);   //DIFFERENCE WITH RELOAD() IS THAT IT RETURNS
            DataSet dset = new DataSet();                           //THE RESULT TO THE DATAGRIDVIEW CONTROL
            adapt.Fill(dset, table);
            dgv.DataSource = dset.Tables[table].DefaultView;  
        }
    }
}
