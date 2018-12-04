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
    public partial class add_item : Form
    {
        String constring = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=medical";
            
        public add_item()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                MySqlConnection conDataBase = new MySqlConnection(constring);
                conDataBase.Open();
                string sql = "select * from avl_stock where p_name='" + textBox1.Text + "'";
                MySqlCommand cmdDataBase = new MySqlCommand(sql, conDataBase);
                MySqlDataReader rdr;
                rdr = cmdDataBase.ExecuteReader();
                int x = 0;
                rdr.Read();

                if ((rdr["p_name"].ToString()).Equals(textBox1.Text))
                {
                    x = int.Parse(rdr["p_quantity"].ToString()) + int.Parse(textBox2.Text);
                }

                rdr.Close();
                string sql1 = "update avl_stock set p_quantity=" + x + " where p_name='" + textBox1.Text + "'";
                MySqlCommand comm2 = new MySqlCommand(sql1, conDataBase);
                comm2.ExecuteNonQuery();




                conDataBase.Close();
                load();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

      private void Available_Stock_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'medicalDataSet1.avl_stock' table. You can move, or remove it, as needed.
            //this.avl_stockTableAdapter1.Fill(this.medicalDataSet1.avl_stock);
            // TODO: This line of code loads data into the 'medicalDataSet.avl_stock' table. You can move, or remove it, as needed.
           // this.avl_stockTableAdapter.Fill(this.medicalDataSet.avl_stock);
        
        }

      private void fillToolStripButton_Click(object sender, EventArgs e)
      {
          try
          {
              this.avl_stockTableAdapter.Fill(this.medicalDataSet.avl_stock);
          }
          catch (System.Exception ex)
          {
              System.Windows.Forms.MessageBox.Show(ex.Message);
          }

      }
        
      private void button3_Click(object sender, EventArgs e)
      {
                }

      private void textBox1_TextChanged(object sender, EventArgs e)
      {

      }

      private void button2_Click(object sender, EventArgs e)
      {
          main m = new main();
          m.Show();
      }

      private void button2_Click_1(object sender, EventArgs e)
      {
          this.Close();
      }
    }
}
