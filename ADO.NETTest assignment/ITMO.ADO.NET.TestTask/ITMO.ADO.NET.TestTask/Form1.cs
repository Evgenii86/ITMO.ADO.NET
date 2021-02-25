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

namespace ITMO.ADO.NET.TestTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection MyDBConnection = new SqlConnection("Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=MyDB;Data Source=MOJ-PK\\SQLEXPRESS");
        private SqlDataAdapter MyDataAdapter;
        private DataSet MyDataset = new DataSet("MyDB");
        private DataTable CustomersTable = new DataTable("Customers");
        private DataTable OrdersTable = new DataTable("Orders");
        
        private void Form1_Load(object sender, EventArgs e)
        {
            using (MyDataAdapter = new SqlDataAdapter("SELECT * FROM Customers", MyDBConnection))
            {
                MyDataset.Tables.Add(CustomersTable);
                MyDataAdapter.Fill(MyDataset.Tables["Customers"]);
                MyDBdataGrid.DataSource = MyDataset.Tables["Customers"];
                MyDataAdapter.SelectCommand.CommandText = "SELECT * FROM Orders";
                MyDataset.Tables.Add(OrdersTable);
                MyDataAdapter.Fill(MyDataset.Tables["Orders"]);

            }

            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommandBuilder cmd1 = new SqlCommandBuilder(MyDataAdapter))
            {
                MyDataset.EndInit();
                SqlCommand command1 = new SqlCommand("SELECT * FROM Customers", MyDBConnection);
                MyDataAdapter.SelectCommand = command1;
                MyDataAdapter.Update(MyDataset.Tables["Customers"]);
                SqlCommand command2 = new SqlCommand("SELECT * FROM Orders", MyDBConnection);
                MyDataAdapter.SelectCommand = command2;
                MyDataAdapter.Update(MyDataset.Tables["Orders"]);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MyDBdataGrid.DataSource == MyDataset.Tables["Orders"])
                MyDBdataGrid.DataSource = MyDataset.Tables["Customers"];
            else
                MyDBdataGrid.DataSource = MyDataset.Tables["Orders"];
        }
    }


}
