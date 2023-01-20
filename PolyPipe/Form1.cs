using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyPipe
{
    public partial class Form1 : Form
    {

        public string cusID;
        MySqlConnection conn = new MySqlConnection(@"SERVER=localhost;DATABASE=polypipe;UID=root;PASSWORD=''; persistsecurityinfo=True;SslMode=none;");
        public Form1()
        {
            InitializeComponent();

            //connetionString = 
            //conn = new MySqlConnection(connetionString);
            //try
            //{
            //    cnn.Open();
            //    MessageBox.Show("Connection Open ! ");
            //    cnn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Can not open connection ! ");
            //}
        }


        private void button1_Click(object sender, EventArgs e)
        {

            conn.Open();
            Console.WriteLine("Connected");
            try
            {
                string query = "insert into customers(c_name) values('" + nameTxt.Text + "')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Done"); MessageBox.Show("Done");
                nameTxt.Clear();
                updateList();
               
            }
            catch (Exception)
            {
                Console.WriteLine("errr"); MessageBox.Show("err");
            }


            conn.Close();
            Console.WriteLine("Closed");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private void cuslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cuslist.SelectedItems.Count == 0)
                    return;
                ListViewItem item = cuslist.SelectedItems[0];

                cusID = item.Text;

                string query = "select c_name from customers where c_id='" + cusID + "' ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                string cusName = Convert.ToString(rd["c_name"]);
                nameTxt.Text = cusName;
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void updateList() {

            try
            {


                cuslist.View = View.Details;
                cuslist.Items.Clear();

                MySqlDataAdapter ada_2 = new MySqlDataAdapter("select * from customers ", conn);

                DataTable dt_1 = new DataTable();
                ada_2.Fill(dt_1);

                conn.Close();
                for (int i = 0; i < dt_1.Rows.Count; i++)
                {
                    DataRow dr = dt_1.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["c_id"].ToString());
                    listitem.SubItems.Add(dr["c_name"].ToString());
                    cuslist.Items.Add(listitem);
                }

                cuslist.Refresh();


            }
            catch (Exception)
            {
                MessageBox.Show("Error", "****", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            conn.Close();
            Console.WriteLine("Closed");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            Console.WriteLine("Connected");
            try
            {
                string query = "update customers set c_name='" + nameTxt.Text + "' where c_id='"+cusID+"'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Done"); MessageBox.Show("Done");
                nameTxt.Clear();
                updateList();
                
            }
            catch (Exception)
            {
                Console.WriteLine("errr"); MessageBox.Show("err");
            }


            conn.Close();
            Console.WriteLine("Closed");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            Console.WriteLine("Connected");
            try
            {
                string query = "delete from customers  where c_id='" + cusID + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Done"); MessageBox.Show("Done");
                nameTxt.Clear();
                updateList();

            }
            catch (Exception)
            {
                Console.WriteLine("errr"); MessageBox.Show("err");
            }


            conn.Close();
            Console.WriteLine("Closed");

        }
    }
    
    
}
