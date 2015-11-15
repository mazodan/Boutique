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
    {
        public bool Success { get; set; } //CHECKS IF SQL IS EXECUTED FOR REFERENCE

        //INITIALIZE THE DATABASE SQL CREDENTIALS; MAKE IT EDITABLE FOR THESIS TIER PROJECT
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

        public void insert(string table, string ID, string Name, string Qty, string Price, MySqlConnection conn)
        {
            string query = "INSERT INTO " + table + " VALUES (@id, @name, @qty, @price)"; //INSERT QUERY FOR DATABASE
                                                                            //PARAMETERIZED TO PROTECT FROM INJECTIONS!!
            MySqlCommand comm = new MySqlCommand(query, conn);  //LOOK TO SEARCH FUNCTION FOR DETAILS
            comm.Parameters.Add(new MySqlParameter("@id",ID));
            comm.Parameters.Add(new MySqlParameter("@name", Name));     //APPLY DATA TO PARAMETERS  
            comm.Parameters.Add(new MySqlParameter("@qty", Qty));
            comm.Parameters.Add(new MySqlParameter("@price", Price));

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySqlException ex)       //CATCH POTENTIAL EXCEPTIONS, EX. SOME IDIOT PUT A STRING IN A DECIMAL
            {                               //IT AIN'T A VARCHAR
                MessageBox.Show(ex.Message,"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Success = false;            //SHOWS THAT THE EXECUTION FAILED
                return;                     //EXCEPTION THROWN, RETURN STATEMENT TERMINATES THE FUNCTION
            }
                                            //MESSAGEBOX APPEARS IF NO EXCEPTION IS THROWN
            MessageBox.Show("ITEM SUCCESSFULLY ADDED", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;                 //EXECUTION SUCCEEDED
        }

        public void update(string table, string ID, string name, string qty, string price, MySqlConnection conn)
        {
            string query = "UPDATE " + table + " SET Name=@name, Qty=@qty, Price=@price WHERE itemID=@id;";
            MySqlCommand comm = new MySqlCommand(query, conn);

            comm.Parameters.Add(new MySqlParameter("@id", ID));
            comm.Parameters.Add(new MySqlParameter("@name", name));
            comm.Parameters.Add(new MySqlParameter("@qty", qty));
            comm.Parameters.Add(new MySqlParameter("@price", price));

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Success = false;
                return;
            }

            MessageBox.Show("ITEM SUCCESSFULLY UPDATED", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;
        }                                                  
    }
}
