using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data.Sql;

namespace medical_store
{
    public partial class change_price : Form
    {
        public change_price()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=medical";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                conDataBase.Open();
                string sql = "select * from avl_stock where p_name='" + textBox1.Text + "'";
                MySqlCommand cmdDataBase = new MySqlCommand(sql, conDataBase);
                MySqlDataReader rdr;
                rdr = cmdDataBase.ExecuteReader();
                rdr.Read();
                int old = int.Parse(rdr["p_price"].ToString());
                int ne =int.Parse(textBox2.Text);
                MySqlCommand cmd2 = new MySqlCommand("update avl_stock set p_price=" + ne + " where p_name='" + textBox1.Text + "'", conDataBase);
                rdr.Close();
                cmd2.ExecuteNonQuery();
                conDataBase.Close();
                available v = new available();
                v.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
