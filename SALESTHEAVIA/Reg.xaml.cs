using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace SALESTHEAVIA
{
    public partial class Reg : Window
    {
        DataBase database = new DataBase();
        public Reg()
        {
            InitializeComponent();
        }

        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            database.openConnection();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT count (*) FROM dbo.Users WHERE Login = '{login_box.Text}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (Convert.ToInt32(table.Rows[0][0]) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже есть");
                login_box.Text = "";

            }
            else
            {
                var addQuerry = $"insert into dbo.Users (Login, Password, Name, Surname) values ('{login_box.Text}','{password_box.Text}','{name_box.Text}','{surname_box.Text}')";

                SqlCommand addcommand = new SqlCommand(addQuerry, database.getConnection());
                addcommand.ExecuteNonQuery();

                MessageBox.Show("Пользователь добавлен");

                database.closeConnection();

                MainWindow avt = new MainWindow();
                avt.Show();
                this.Close();
            }
        }
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow avt = new MainWindow();
            avt.Show();
            this.Close();
        }
    }
}
