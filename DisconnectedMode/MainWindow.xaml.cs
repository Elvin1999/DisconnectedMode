using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisconnectedMode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            conn=new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

            using (conn=new SqlConnection())
            {
                var da=new SqlDataAdapter();
                conn.ConnectionString = cs;
                conn.Open();
                var set = new DataSet();

                SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);

                da.SelectCommand = command;
                da.Fill(set, "AuthorsSet");
                myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
