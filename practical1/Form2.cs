using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace practical1
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\visual basics practical\practical1\practical1\Database1.mdf;Integrated Security=True");

        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string q = "select * from fruits";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            MessageBox.Show("number of rows returned" + ds.Tables[0].Rows.Count);
            if(ds.Tables[0].Rows.Count!=0)
            {
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.DisplayMember = "fruits_name";
                comboBox1.ValueMember = "Id";
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = 0;
                dr[1] = "---select---";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                comboBox1.DataSource = ds.Tables[0];
                textBox1.Text = "";
                listBox1.Items.Clear();
            }
              
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fruit = textBox1.Text;
            SqlDataAdapter adp = new SqlDataAdapter("select * from fruits", con);
            SqlCommandBuilder cmb = new SqlCommandBuilder(adp);
            DataSet ds = new DataSet();
            adp.Fill(ds,"fruits");
            DataRow rw = ds.Tables[0].NewRow();

            rw[1] = fruit;
     

            ds.Tables[0].Rows.Add(rw);

            int i = adp.Update(ds.Tables[0]);
            if (i == 1)
                MessageBox.Show(i + " New row added.");
            else
                MessageBox.Show("Insertion Failed.");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string q = "select * from fruits";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            DataGridViewImageColumn delbut = new DataGridViewImageColumn();
           delbut.Name = "pic";
           delbut.Image  = Image.FromFile(path + "\\image\\delete.jpg");
            
            delbut.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(delbut);
            DataGridViewImageColumn del = new DataGridViewImageColumn();
            del.Name = "edit";
            del.Image = Image.FromFile(path + "\\image\\edit.jpg");
            del.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(del);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;
            if(e.ColumnIndex == dataGridView1.Columns["pic"].Index)
            {

                SqlCommand delcmd = new SqlCommand();
                delcmd.CommandText = "delete from fruits where Id=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "";
                //delcmd.CommandText = "update fruits set fruits_name =('"+dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +"') where Id=("+dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()+")";
                con.Open();
                delcmd.Connection = con;
                delcmd.ExecuteNonQuery();
                con.Close();
                dataGridView1.Rows.RemoveAt(e.RowIndex);
        


                MessageBox.Show("row deleted");
                return;
            }
            if(e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                SqlCommand delcmd = new SqlCommand();
                delcmd.CommandText = "update fruits set fruits_name =('" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "') where Id=(" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + ")";
                con.Open();
                delcmd.Connection = con;
                delcmd.ExecuteNonQuery();
                con.Close();
               // dataGridView1.Rows.RemoveAt(e.RowIndex);
                MessageBox.Show("row updated");
            }
            
        }

        private void hOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controldemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 d = new Form1();
            d.Show();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userdetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 da = new Form3();
            da.Show();
        }
    }
}

