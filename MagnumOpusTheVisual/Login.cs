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
    public partial class Login : Form
    {
        DBClass DB = new DBClass();
        MySqlConnection conn;
        public string selection { get; set; }   //Property for selection of menu, to identify which
                                                //user wants to go, Inventory or Cashier mode
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            conn = DB.sqlConnect("localhost", "iteminventory", "root", "root");            
            try
            {
                conn.Open();     //CONNECTS TO THE MYSQL DATABASE
            }
            catch (MySqlException ex)   //CATCH EXCEPTIONS,!!!!
            {
                MessageBox.Show(ex.Message);
                return;                     //terminates method if exception caught
            }

            string query = "select * from security where username = @user and password = @pass;";
            MySqlCommand comm = new MySqlCommand(query, conn);
            comm.Parameters.Add(new MySqlParameter("@user", txtUser.Text));
            comm.Parameters.Add(new MySqlParameter("@pass", txtPass.Text));

            MySqlDataAdapter adapt = new MySqlDataAdapter(comm);    //passing command to adapter
            DataSet dset = new DataSet();
            adapt.Fill(dset, "security");
            int rowcount = dset.Tables["security"].Rows.Count;

            if (rowcount == 1)
            {
                MessageBox.Show("Login Success", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (selection == "Inventory")
                {
                    Inventory I = new Inventory();
                    I.Show();
                    StateCheck.logicSwitch = true;                    
                    conn.Close();
                    this.Close();
                }
                else if (selection == "Cashier")
                {
                    Cashier C = new Cashier();
                    C.Show();
                    StateCheck.logicSwitch = true;
                    conn.Close();
                    this.Close();
                }
            }
            else
            {
                conn.Close();
            }

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("An Admin account is required to access User Management Privileges", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dialogResult == DialogResult.Yes && txtUser.Text != "" && txtPass.Text != "")
            {
                conn = DB.sqlConnect("localhost", "iteminventory", "root", "root");
                try
                {
                    conn.Open();     //CONNECTS TO THE MYSQL DATABASE
                }
                catch (MySqlException ex)   //CATCH EXCEPTIONS,!!!!
                {
                    MessageBox.Show(ex.Message);
                    return;                     //terminates method if exception caught
                }

                string query = "select * from admins where username = @user";
                MySqlCommand comm = new MySqlCommand(query, conn);
                comm.Parameters.Add(new MySqlParameter("@user", txtUser.Text));

                MySqlDataAdapter adapt = new MySqlDataAdapter(comm);    //passing command to adapter
                DataSet dset = new DataSet();
                DataRow drow;
                adapt.Fill(dset, "admins");

                drow = dset.Tables["admins"].Rows[0];
                string hash = drow.ItemArray.GetValue(1).ToString();
                bool confirm = PasswordHash.PasswordHash.ValidatePassword(txtPass.Text, hash);

                if (confirm == true)
                {
                    MessageBox.Show("Admin Login Success", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    AdminManage AM = new AdminManage();
                    AM.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login Failiure");
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Insert Login Credentials");
            }
        }

    }
}
