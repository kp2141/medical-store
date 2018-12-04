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
    public partial class billing : Form
    {
        public billing()
        {
            InitializeComponent();
            
        }
        public void load()
        {
            String constring = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=medical";
                
            MySqlConnection conDataBase = new MySqlConnection(constring);
            conDataBase.Open();
            MySqlCommand cmdDataBase = new MySqlCommand("select * from bill", conDataBase);
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
                int check = int.Parse(rdr["p_quantity"].ToString());
                if (check > (int.Parse(textBox2.Text)))
                {
                    int total = 0;
                    int price = int.Parse(rdr["p_price"].ToString());

                    int new_q = 0;
                    new_q = int.Parse(rdr["p_quantity"].ToString()) - int.Parse(textBox2.Text);
                    rdr.Close();
                    MySqlCommand cmd3 = new MySqlCommand("update avl_stock set p_quantity=" + new_q + " where p_name='" + textBox1.Text + "' ",conDataBase);
                    cmd3.ExecuteNonQuery();


                    total = (int.Parse(textBox2.Text)) * price;
                    MySqlCommand cmd2 = new MySqlCommand("insert into  bill (p_name,p_quantity,p_total) values ('" + textBox1.Text + "'," + textBox2.Text + "," + total + ")", conDataBase);
                    cmd2.ExecuteNonQuery();
                   
                    load();
                    conDataBase.Close();
                }
                else
                {
                    MessageBox.Show("item is not available");
                    available a = new available();
                    a.Show();
                }
               }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["p_name"].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=medical";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                conDataBase.Open();
                string sql = "delete from bill where p_name='" + textBox1.Text + "'";
                MySqlCommand cmdDataBase = new MySqlCommand(sql, conDataBase);
                cmdDataBase.ExecuteNonQuery();
                conDataBase.Close();
               load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void billing_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'medicalDataSet.avl_stock' table. You can move, or remove it, as needed.
           // this.avl_stockTableAdapter.Fill(this.medicalDataSet.avl_stock);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            final_bill f = new final_bill();
            f.Show();
        }
    }
}
