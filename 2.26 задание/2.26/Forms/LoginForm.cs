using AIS_Abiturient.Helpers;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AIS_Abiturient.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT role FROM users WHERE LOWER(username) = @username AND password = @password", conn);
                cmd.Parameters.AddWithValue("@username", username.ToLower());
                cmd.Parameters.AddWithValue("@password", password);
                var role = cmd.ExecuteScalar();

                if (role != null)
                {
                    string userRole = role.ToString().ToLower();
                    this.Hide();
                    if (userRole == "admin")
                        new AdminPanel().ShowDialog();
                    else if (userRole == "operator")
                        new OperatorPanel().ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}