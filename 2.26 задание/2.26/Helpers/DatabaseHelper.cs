using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace AIS_Abiturient.Helpers
{
    public static class DatabaseHelper
    {
        // Абсолютный путь к базе данных рядом с .exe или в AppData
        private static readonly string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AIS_Abiturient",
            "AIS_Abiturient.db"
        );

        public static SQLiteConnection GetConnection()
        {
            EnsureDatabaseExists();
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        private static void EnsureDatabaseExists()
        {
            var directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(dbPath))
                Init();
        }

        private static void Init()
        {
            SQLiteConnection.CreateFile(dbPath);
            using var conn = GetConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE users (
                    username TEXT,
                    password TEXT,
                    role TEXT
                );

                INSERT INTO users VALUES ('admin', 'admin', 'admin');
                INSERT INTO users VALUES ('operator', 'operator', 'operator');

                CREATE TABLE applicants (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    full_name TEXT,
                    birth_day TEXT,
                    phone TEXT,
                    email TEXT,
                    document_type TEXT,
                    document_number TEXT,
                    submission_date TEXT
                );
            ";
            cmd.ExecuteNonQuery();
        }

        public static void LoadApplicantsToGrid(DataGridView grid)
        {
            grid.Columns.Clear();
            grid.Columns.Add("id", "ID");
            grid.Columns.Add("full_name", "ФИО");
            grid.Columns.Add("birth_day", "Дата рождения");
            grid.Columns.Add("phone", "Телефон");
            grid.Columns.Add("email", "Email");
            grid.Columns.Add("document_type", "Тип документа");
            grid.Columns.Add("document_number", "Номер документа");
            grid.Columns.Add("submission_date", "Дата подачи");

            using var conn = GetConnection();
            conn.Open();
            var cmd = new SQLiteCommand("SELECT * FROM applicants", conn);
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                grid.Rows.Add(
                    rdr["id"],
                    rdr["full_name"],
                    rdr["birth_day"],
                    rdr["phone"],
                    rdr["email"],
                    rdr["document_type"],
                    rdr["document_number"],
                    rdr["submission_date"]
                );
            }
        }
    }
}
