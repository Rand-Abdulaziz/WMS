using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Properties;

namespace WMS
{
    public partial class Form_Main : Form
    {
        DatabaseOperations databaseOperation = new DatabaseOperations();
        String query;
        public Form_Main()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                query = $"SELECT * FROM whms_schema.Users WHERE Email = '{textBox1.Text}'";

                DataSet ds = databaseOperation.getData(query);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("god");
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        Console.WriteLine(column.ColumnName);
                    }

                    string role = ds.Tables[0].Rows[0]["Role"].ToString();
                    Console.WriteLine("Role: " + role);  

                    
                    if (string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        Console.WriteLine("Admin role detected.");
                    }
                    else if (string.Equals(role, "employee", StringComparison.OrdinalIgnoreCase))
                    {
                       
                        Console.WriteLine("Employee role detected.");
                    }
                    Console.WriteLine("User Role: " + role);
                    Int64 appUserPK = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    Dashboard mdash = new Dashboard(role);
                    mdash.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bad Credentials.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in btnLogin Click : " + ex);
                MessageBox.Show("Somthing went wrong : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
