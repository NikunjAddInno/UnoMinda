using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace Measurement_AI.classes
{
    public class Database
    {
        

        public void InsertRecord(DateTime date, DateTime time, string modelname, float height, float dia, string color)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                
                string query = @"insert into public.logreport(_date, _time, modelname, height, dia, color)
                                values(@date, @time, @modelname, @height, @dia, @color)";

                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@modelname", modelname);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@dia", dia);
                cmd.Parameters.AddWithValue("@color", color);

                con.Open();
                int n = cmd.ExecuteNonQuery();
            }
        }


        public void TestConnection()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();
                if (con.State != ConnectionState.Open)
                {
                    MessageBox.Show("I'm lost ....");
                }
            }
        }

        public void GroupTableColItems(ComboBox comboBox, string tableName, string colName)
        {
            try
            {
                comboBox.Items.Clear();
                using (NpgsqlConnection con = GetConnection())
                {
                    string query = string.Format(@"select {0} from public.{1} group by {0} order by {0} ASC", colName, tableName);
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader[0].ToString());

                    }
                    if (comboBox.Items.Count > 0)
                    {
                        comboBox.SelectedIndex = 0;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost; Port = 5432; user Id = postgres; password = 1234; Database = postgres;");
        }
    }


}