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
    public partial class available : Form
    {
        public available()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            String constring = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=medical";

            MySqlConnection conDataBase = new MySqlConnection(constring);
            conDataBase.Open();
            MySqlCommand cmdDataBase = new MySqlCommand("select * from avl_stock", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
                conDataBase.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
