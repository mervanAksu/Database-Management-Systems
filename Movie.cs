using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class Movie : Form
    {
        public Movie()
        {
            InitializeComponent();
        }

        NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432;" +
            "Database=Cinema; user Id=postgres; password=merwan02.");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Movie_Load(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"category\"", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            categoryComboBox.DisplayMember = "categoryName";
            categoryComboBox.ValueMember = "categoryID";
            categoryComboBox.DataSource = dt;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {

            connection.Open();
            //MessageBox("Connection opened!");
            string query = "SELECT \"Movie\".\"movieID\", \"Movie\".\"movieName\", \"category\".\"categoryName\" from \"Movie\" INNER JOIN \"category\" ON" +
                "\"Movie\".\"categoryID\" = \"category\".\"categoryID\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO \"Movie\"(\"movieName\", \"categoryID\"," +
                    " \"directorID\", \"duration\", \"rating\", \"language\", \"description\") values(@p2, @p4, @p5, @p6, @p7, @p8, @p9)", connection);
                //command1.Parameters.AddWithValue("@p1", int.Parse(txtMovieID.Text)); 
                //NpgsqlDataReader reader = command1.ExecuteReader();

                command1.Parameters.AddWithValue("@p2", txtMovieName.Text);
                //command1.Parameters.Add("@p3", NpgsqlTypes.NpgsqlDbType.Date, 0, "db1");
                command1.Parameters.AddWithValue("@p4", int.Parse(categoryComboBox.SelectedValue.ToString()));
                command1.Parameters.AddWithValue("@p5", int.Parse(directorNumericUD.Value.ToString()));
                command1.Parameters.AddWithValue("@p6", float.Parse(txtDuration.Text));
                command1.Parameters.AddWithValue("@p7", float.Parse(txtRating.Text));
                command1.Parameters.AddWithValue("@p8", txtLanguage.Text);
                command1.Parameters.AddWithValue("@p9", txtDescription.Text);
                command1.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("The added query has been executed succesfully!");
            }
            catch(NpgsqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command2 = new NpgsqlCommand("Delete from \"Movie\" where \"movieID\" = @p1", connection);
            command2.Parameters.AddWithValue("@p1", int.Parse(txtMovieID.Text));
            if (MessageBox.Show("Do you want to remove this movie?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command2.ExecuteNonQuery();
                MessageBox.Show("The Movie Deleted Successfully!");
            }
            else
            {
                MessageBox.Show("Movie Not Removed!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            connection.Close();
            //MessageBox.Show("The delete query has been executed succesfully!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command3 = new NpgsqlCommand("UPDATE \"Movie\" SET \"movieName\" = @p1, \"categoryID\" = @p2 WHERE \"movieID\" = @p3", connection);
            command3.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command3.Parameters.AddWithValue("@p2", int.Parse(categoryComboBox.SelectedValue.ToString()));
            command3.Parameters.AddWithValue("@p3", int.Parse(txtMovieID.Text));
            if (MessageBox.Show("Do You Want to Update This Movie?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command3.ExecuteNonQuery();
                MessageBox.Show("The Movie Updated Successfully!");
            }
            else
            {
                MessageBox.Show("Movie Not Updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            connection.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "SELECT \"Movie\".\"movieID\", \"Movie\".\"movieName\", \"category\".\"categoryName\", \"Movie\".\"rating\" FROM \"Movie\" INNER JOIN \"category\" ON \"Movie\".\"categoryID\" = \"category\".\"categoryID\" WHERE \"movieName\" ='" + txtMovieName.Text+"'";
            NpgsqlCommand command4 = new NpgsqlCommand(query, connection);
            //command4.Parameters.AddWithValue("@p1", txtMovieName.Text);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            command4.ExecuteNonQuery();

            connection.Close();
        }
    }
}
