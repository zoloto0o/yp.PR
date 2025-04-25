using AIS_Abiturient.Helpers;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace AIS_Abiturient.Forms
{
    public class OperatorPanel : PanelBase
    {
        private Button btnAdd;

        public OperatorPanel()
        {
            Text = "Панель Оператора";
            this.Icon = new Icon("sms.ico");
            InitCommonUI();

            btnAdd = new Button { Text = "Добавить", AutoSize = true };
            btnAdd.Click += (s, e) => { new ApplicantForm().ShowDialog(); LoadData(); };

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
                AutoSize = true
            };
            buttonPanel.Controls.Add(btnAdd);
            Controls.Add(buttonPanel);

            LoadData();
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

        private void InitializeComponent()
        {

        }
    }
}
