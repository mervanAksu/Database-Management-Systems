using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dr
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }
        NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432;" +
            "Database=Cinema; user Id=postgres; password=merwan02.");
        //private object categoryID;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            connection.Open();
            //MessageBox("Connection opened!");
            string query = "SELECT * from \"category\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command1 = new NpgsqlCommand("insert into \"category\" (\"categoryName\")values (@p1)", connection);
            //command1.Parameters.AddWithValue("@p1", int.Parse(txtCategoryID.Text));
            command1.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command1.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("The query has been executed succesfully!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command2 = new NpgsqlCommand("Delete from \"category\" WHERE \"categoryID\" = @p1", connection);
            command2.Parameters.AddWithValue("@p1", int.Parse(txtCategoryID.Text));
            if (MessageBox.Show("Do you want to Remove this category?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command2.ExecuteNonQuery();
                MessageBox.Show("The Category Deleted Successfully!");
            }
            else
            {
                MessageBox.Show("The Category Not Removed!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command3 = new NpgsqlCommand("UPDATE \"category\" SET \"categoryName\" = @p1 WHERE \"categoryID\" = @p2", connection);
            command3.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command3.Parameters.AddWithValue("@p2", int.Parse(txtCategoryID.Text));
            if (MessageBox.Show("Do you want to Update this category?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command3.ExecuteNonQuery();
                MessageBox.Show("The Category Updated Successfully!");
            }
            else
            {
                MessageBox.Show("The Category Not Removed!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            connection.Close();
        }
    }
}
