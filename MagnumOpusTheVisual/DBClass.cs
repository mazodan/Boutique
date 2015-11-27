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

        //FOR DATABASES WITH NO GODDAMNED ROOT
        public MySqlConnection sqlConnect(string server, string dbase, string user)
        {
            MySqlConnection conn = new MySqlConnection(
                "server='" + server + "';Database=" + dbase + ";User ID=" + user + ";");
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

        public void search(string table, string parameter, DataGridView dgv, MySqlConnection conn)  //OVERLOADED FOR CASHIER
        {
            string query = "SELECT itemID, Name, Price FROM " + table + " WHERE Name LIKE @param;";  
            string searchTerm = string.Format("%{0}%", parameter); 
            
            MySqlCommand comm = new MySqlCommand(query, conn);
            comm.Parameters.Add(new MySqlParameter("@param", searchTerm));  

            MySqlDataAdapter adapt = new MySqlDataAdapter(comm);   
            DataSet dset = new DataSet();                           
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
                                                                                        //OVERLOADED METHODS, 2 VERSIONS OF UPDATE
        public void update(string table, string ID, string qty, MySqlConnection conn)   //UPDATE SPECIFIC FOR EDITING QUANTITY
        {
            string query = "UPDATE " + table + " SET Qty = @qty WHERE itemID = @id;";
            MySqlCommand comm = new MySqlCommand(query, conn);

            comm.Parameters.Add(new MySqlParameter("@id", ID));
            comm.Parameters.Add(new MySqlParameter("@qty", qty));

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

            MessageBox.Show("QUANTITY SUCCESSFULLY UPDATED", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;
        }

        public void update(string table, string ID, MySqlConnection conn , string price)   //UPDATE SPECIFIC FOR EDITING QUANTITY
        {
            string query = "UPDATE " + table + " SET Price = @price WHERE itemID = @id;";
            MySqlCommand comm = new MySqlCommand(query, conn);

            comm.Parameters.Add(new MySqlParameter("@id", ID));
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

            MessageBox.Show("PRICE SUCCESSFULLY UPDATED", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;
        }

        public void delete(string table, string ID, MySqlConnection conn)
        {
            string query = "DELETE FROM " + table + " WHERE itemID = @id;"; //DELETE QUERY
            MySqlCommand comm = new MySqlCommand(query, conn);

            comm.Parameters.Add(new MySqlParameter("@id", ID));
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }

            MessageBox.Show("ITEM SUCCESSFULLY DELETED", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        public string ReceiptIntro()
        {
            return "Boutique Official Receipt(MOCK RECEIPT)\nSignature with Printed Name:\nDate Issued:"; 
        }

        public string InsertItem(string ItemName, string ItemPrice, string Quantity)
        {
            return ItemName + "\t\t x" + Quantity + "\n\t\t" + ItemPrice;
        }

        public string ReceiptFooter(string total, string change)
        {
            return "\n\nTOTAL AMOUNT: " + total + "\nAMOUNT PAID: "
                + StateCheck.AmtPaid.ToString() + "\nVAT: " + StateCheck.VAT.ToString() +                
                "\nCHANGE: " + StateCheck.Change.ToString() +
                "\nTHANK YOU FOR TRANSACTING WITH US";

            //APOLOGIES FOR THE SPAGHETTI CODE, LIMITATIONS OF SPACE
        }
       
    }
}
