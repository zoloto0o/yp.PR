using AIS_Abiturient.Helpers;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AIS_Abiturient.Forms
{
    public class ApplicantForm : Form
    {
        private TextBox txtName, txtPhone, txtEmail, txtDocNumber;
        private DateTimePicker dtpBirthDay, dtpSubmission;
        private ComboBox cmbDocType;
        private Button btnSave;

        private int? applicantId = null;

        public ApplicantForm(int? id = null)
        {
            applicantId = id;

            Text = id == null ? "Добавление абитуриента" : "Редактирование абитуриента";
            Width = 400;
            Height = 430;

            Label lblName = new Label { Text = "ФИО:", Top = 20, Left = 20 };
            txtName = new TextBox { Top = 40, Left = 20, Width = 300 };

            Label lblBirth = new Label { Text = "Дата рождения:", Top = 70, Left = 20 };
            dtpBirthDay = new DateTimePicker { Top = 90, Left = 20 };

            Label lblPhone = new Label { Text = "Телефон:", Top = 120, Left = 20 };
            txtPhone = new TextBox { Top = 140, Left = 20, Width = 300 };

            Label lblEmail = new Label { Text = "Email:", Top = 170, Left = 20 };
            txtEmail = new TextBox { Top = 190, Left = 20, Width = 300 };

            Label lblDocType = new Label { Text = "Тип документа:", Top = 220, Left = 20 };
            cmbDocType = new ComboBox { Top = 240, Left = 20, Width = 200 };
            cmbDocType.Items.AddRange(new string[] { "Паспорт", "Свидетельство", "Иной" });
            cmbDocType.SelectedIndex = 0;

            Label lblDocNum = new Label { Text = "Номер документа:", Top = 270, Left = 20 };
            txtDocNumber = new TextBox { Top = 290, Left = 20, Width = 300 };

            Label lblSubDate = new Label { Text = "Дата подачи:", Top = 320, Left = 20 };
            dtpSubmission = new DateTimePicker { Top = 340, Left = 20 };

            btnSave = new Button { Text = "Сохранить", Top = 370, Left = 20 };
            btnSave.Click += SaveApplicant;

            Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblBirth, dtpBirthDay,
                lblPhone, txtPhone,
                lblEmail, txtEmail,
                lblDocType, cmbDocType,
                lblDocNum, txtDocNumber,
                lblSubDate, dtpSubmission,
                btnSave
            });

            if (id != null) LoadApplicantData(id.Value);
        }

        private void LoadApplicantData(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                new SQLiteCommand("PRAGMA journal_mode=WAL;", conn).ExecuteNonQuery();

                using (var cmd = new SQLiteCommand("SELECT * FROM applicants WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["full_name"].ToString();
                            dtpBirthDay.Value = DateTime.Parse(reader["birth_day"].ToString());
                            txtPhone.Text = reader["phone"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            cmbDocType.Text = reader["document_type"].ToString();
                            txtDocNumber.Text = reader["document_number"].ToString();
                            dtpSubmission.Value = DateTime.Parse(reader["submission_date"].ToString());
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {

        }

        private void SaveApplicant(object sender, EventArgs e)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                new SQLiteCommand("PRAGMA journal_mode=WAL;", conn).ExecuteNonQuery();

                string query = applicantId == null
                    ? @"INSERT INTO applicants (full_name, birth_day, phone, email, document_type, document_number, submission_date)
                        VALUES (@name, @birth, @phone, @mail, @docType, @docNum, @subDate)"
                    : @"UPDATE applicants SET 
                            full_name = @name, 
                            birth_day = @birth, 
                            phone = @phone, 
                            email = @mail, 
                            document_type = @docType, 
                            document_number = @docNum, 
                            submission_date = @subDate 
                        WHERE id = @id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@birth", dtpBirthDay.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@docType", cmbDocType.Text);
                    cmd.Parameters.AddWithValue("@docNum", txtDocNumber.Text);
                    cmd.Parameters.AddWithValue("@subDate", dtpSubmission.Value.ToString("yyyy-MM-dd"));

                    if (applicantId != null)
                        cmd.Parameters.AddWithValue("@id", applicantId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show(applicantId == null ? "Абитуриент добавлен." : "Изменения сохранены.");
            Close();
        }
    }
}
