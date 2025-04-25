using System.Drawing;
using System.Windows.Forms;

namespace AIS_Abiturient.Forms
{
    public class PanelBase : Form
    {
        protected DataGridView grid;

        public PanelBase()
        {
            WindowState = FormWindowState.Maximized;
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10);
            StartPosition = FormStartPosition.CenterScreen;
        }

        protected void InitCommonUI()
        {
            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    SelectionBackColor = Color.LightBlue
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray
                }
            };

            Controls.Add(grid);
        }

        private void InitializeComponent()
        {

        }

        public virtual void LoadData() { }
    }
}
