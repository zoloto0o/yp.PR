using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using AIS_Abiturient.Helpers;


namespace AIS_Abiturient.Forms
{
    public class AdminPanel : PanelBase
    {
       
        private Button btnAdd, btnEdit, btnDelete, btnExport;

        public AdminPanel()
        {
            Text = "Админ Панель";
            this.Icon = new Icon("sms.ico");
            InitCommonUI();

            btnAdd = new Button { Text = "Добавить", AutoSize = true };
            btnAdd.Click += (s, e) => { new ApplicantForm().ShowDialog(); LoadData(); };

            btnEdit = new Button { Text = "Редактировать", AutoSize = true };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button { Text = "Удалить", AutoSize = true };
            btnDelete.Click += BtnDelete_Click;

            btnExport = new Button { Text = "Экспорт в Excel", AutoSize = true };
            btnExport.Click += (s, e) => ExportHelper.ExportToExcel(grid, "export.xlsx");

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                AutoSize = true
            };
            buttonPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnExport });
            Controls.Add(buttonPanel);

            LoadData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells["id"].Value);

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM applicants WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells["id"].Value);
            var form = new ApplicantForm(id);
            form.ShowDialog();
            LoadData();
        }

        private void InitializeComponent()
        {

        }

        public override void LoadData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var adapter = new SQLiteDataAdapter("SELECT * FROM applicants", conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                grid.DataSource = dt;

                // Русские заголовки
                if (grid.Columns["id"] != null) grid.Columns["id"].HeaderText = "ID";
                if (grid.Columns["full_name"] != null) grid.Columns["full_name"].HeaderText = "ФИО";
                if (grid.Columns["birth_day"] != null) grid.Columns["birth_day"].HeaderText = "Дата рождения";
                if (grid.Columns["phone"] != null) grid.Columns["phone"].HeaderText = "Телефон";
                if (grid.Columns["email"] != null) grid.Columns["email"].HeaderText = "Email";
                if (grid.Columns["document_type"] != null) grid.Columns["document_type"].HeaderText = "Тип документа";
                if (grid.Columns["document_number"] != null) grid.Columns["document_number"].HeaderText = "Номер документа";
                if (grid.Columns["submission_date"] != null) grid.Columns["submission_date"].HeaderText = "Дата подачи";
            }
        }
    }
}
