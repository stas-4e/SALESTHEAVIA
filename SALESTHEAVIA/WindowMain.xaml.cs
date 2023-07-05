using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;
using System.Data;
using System.Runtime.ExceptionServices;
using System.IO;

namespace SALESTHEAVIA
{
    public partial class WindowMain : Window
    {
        DataBase database = new DataBase();
        AllJson allJson = new AllJson();

        int id, change_log;
        string first_log;
        public WindowMain(string Login, string Password)
        {
            first_log = Login;
            InitializeComponent();

            var all_data = allJson.Get_all_display_data();
            grid1.ItemsSource = all_data;

            string searchString = $"Select * FROM dbo.Users where Login = '{Login}' and Password = '{Password}'";

            SqlCommand search = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = search.ExecuteReader();


            while (reader.Read())
            {
                log_box.Text = reader[0].ToString();
                pass_box.Text = reader[1].ToString();
                name_box.Text = reader[2].ToString();
                surname_box.Text = reader[3].ToString();
                status_box.Text = reader[4].ToString();
                id = Convert.ToInt32(reader[5]);
            }
            safe_btn.Visibility = Visibility.Hidden;
            reader.Close();
        }

        private void status_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            safe_btn.Visibility = Visibility.Visible;
        }

        private void surname_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            safe_btn.Visibility = Visibility.Visible;

        }

        private void name_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            safe_btn.Visibility = Visibility.Visible;
        }

        private void log_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            safe_btn.Visibility = Visibility.Visible;
            change_log++;
            if (first_log == log_box.Text) change_log = 0;
        }

        private void baseSafe()
        {
            database.openConnection();
            var updateQuery = $"UPDATE dbo.Users SET Name='{name_box.Text}', Surname='{surname_box.Text}', Status='{status_box.Text}',Login='{log_box.Text}',Password = '{pass_box.Text}' where id='{id}'";
            var command = new SqlCommand(updateQuery, database.getConnection());
            command.ExecuteNonQuery();
            database.closeConnection();

        }

        private void safe_btn_Click(object sender, RoutedEventArgs e)
        {
            if (change_log == 0) baseSafe();
            else
            {
                database.openConnection();

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string querystring = $"SELECT count (*) FROM dbo.Users WHERE Login = '{log_box.Text}'";

                SqlCommand checkcommand = new SqlCommand(querystring, database.getConnection());

                adapter.SelectCommand = checkcommand;
                adapter.Fill(table);
                if (Convert.ToInt32(table.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Пользователь с таким логином уже есть");
                    log_box.Text = first_log;
                }
                else
                {
                    baseSafe();
                }
                database.closeConnection();
            }
            safe_btn.Visibility = Visibility.Hidden;
        }
    }
}