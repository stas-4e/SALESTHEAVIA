using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace SALESTHEAVIA
{
    public partial class MainWindow : Window
    {
        DataBase database = new DataBase();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }

        private void enter_btn_Click(object sender, RoutedEventArgs e)
        {
            var Login = log_box.Text;
            var Password = pass_box.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT count (*) FROM dbo.Users WHERE Login = '{Login}' and Password = '{Password}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Вы успешно вошли!");
                WindowMain window = new WindowMain(Login, Password);
                window.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Введены неверные данные!");

            }
        }

    }
}
