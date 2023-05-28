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
                //myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
            }
        }

        DataSet set = new DataSet();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region DataTable

            //using (var conn=new SqlConnection())
            //{
            //    conn.ConnectionString = cs;
            //    conn.Open();

            //    SqlCommand command = new SqlCommand();
            //    command.CommandText = "SELECT * FROM Authors";
            //    command.Connection = conn;

            //    table = new DataTable();

            //    bool hasColumnAdded = false;
            //    using (reader=command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            if (!hasColumnAdded)
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    table.Columns.Add(reader.GetName(i));
            //                }
            //                hasColumnAdded = true;
            //            }

            //            DataRow row = table.NewRow();
            //            for (int i = 0; i < reader.FieldCount; i++)
            //            {
            //                row[i] = reader[i];
            //            }
            //            table.Rows.Add(row);
            //        }

            //        myDataGrid1.ItemsSource = table.DefaultView;

            //    }
            //}

            #endregion


            #region DataSet And SqlDataAda[ter
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                var da=new SqlDataAdapter("SELECT * FROM Authors;SELECT * FROM Books",conn);

                da.Fill(set, "authorsbooks");

                myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
              //  myDataGrid2.ItemsSource = set.Tables[1].DefaultView;

            }



            #endregion

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                var command = new SqlCommand("UPDATE Authors SET Firstname=@firstName WHERE Id=@id", conn);

                command.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = 1
                });

                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType=SqlDbType.NVarChar,
                    ParameterName="@firstName",
                    Value="ADMIN ADMIN"
                });

                var da = new SqlDataAdapter();
                da.UpdateCommand = command;
                da.UpdateCommand.ExecuteNonQuery();

                da.Update(set, "authorsbooks");
                set.Clear();

                da = new SqlDataAdapter("SELECT * FROM Authors;SELECT * FROM Books", conn);

                da.Fill(set, "authorsbooks");

                myDataGrid1.ItemsSource = set.Tables[0].DefaultView;


            }
        }
    }
}
