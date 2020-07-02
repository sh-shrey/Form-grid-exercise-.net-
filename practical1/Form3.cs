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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\visual basics practical\practical1\practical1\Database1.mdf;Integrated Security=True");
        int j;
        public Form3()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name=textBox2.Text;
            int age =Convert.ToInt32(textBox3.Text);
            string email = textBox4.Text;
            string gender;
            if (radioButton1.Checked)
                gender = radioButton1.Text;
            else
                gender = radioButton2.Text;
            string hobby= "\n";

            for (j = 0; j < checkedListBox1.Items.Count; j++)
            {
                if (checkedListBox1.GetItemChecked(j))
                {
                    hobby += "\n" + checkedListBox1.Items[j].ToString();
                }
            }
            string city = comboBox1.SelectedItem.ToString();
            SqlDataAdapter adp = new SqlDataAdapter("select * from stu_details", con);
            SqlCommandBuilder cmb = new SqlCommandBuilder(adp);
            DataSet ds = new DataSet();
            adp.Fill(ds, "stu_details");
            DataRow rw = ds.Tables[0].NewRow();

            rw[1] = name;
            rw[2] = age;
            rw[3] = email;
            rw[4] = gender;
            rw[5] = hobby;
            rw[6] = city;


            ds.Tables[0].Rows.Add(rw);

            int i = adp.Update(ds.Tables[0]);
            if (i == 1)
                MessageBox.Show(i + " New row added.");
            else
                MessageBox.Show("Insertion Failed.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string q = "select * from stu_details";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            DataGridViewImageColumn delbut = new DataGridViewImageColumn();
            delbut.Name = "pic";
            delbut.Image = Image.FromFile(path + "\\image\\delete.jpg");

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
            if (e.ColumnIndex == dataGridView1.Columns["pic"].Index)
            {

                SqlCommand delcmd = new SqlCommand();
                delcmd.CommandText = "delete from stu_details where Id=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "";
                
                con.Open();
                delcmd.Connection = con;
                delcmd.ExecuteNonQuery();
                con.Close();
                dataGridView1.Rows.RemoveAt(e.RowIndex);



                MessageBox.Show("row deleted");
                return;
            }
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
            {
                SqlCommand del = new SqlCommand();
                del.CommandText = "update stu_details set name =('" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() +"','"+dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()+"','"+dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()+"','"+dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()+"','"+dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()+"') where Id=(" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + ")";
                con.Open();
                del.Connection = con;
                //del.ExecuteNonQuery();
                con.Close();
                
                MessageBox.Show("row updated");
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
    }

