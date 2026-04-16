using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
namespace SQLite_PoC.Data
{
    public static class DataAccess
    {
        public static async Task InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder
                .CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand = @"
                    CREATE TABLE IF NOT EXISTS MyTable (
                        Primary_Key INTEGER PRIMARY KEY,
                        Text_Entry NVARCHAR(2048) NULL
                    )";
                using (var createTable = new SqliteCommand(tableCommand, db))
                {
                    createTable.ExecuteNonQuery();
                }
            }

            await Windows.System.Launcher.LaunchFolderAsync(
                ApplicationData.Current.LocalFolder);
        }

        public static void AddData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                using (var insertCommand = db.CreateCommand())
                {
                    insertCommand.CommandText = "INSERT INTO MyTable (Text_Entry) VALUES (@Entry);";
                    insertCommand.Parameters.AddWithValue("@Entry", inputText);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public static List<string> GetData()
        {
            var entries = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                using (var selectCommand = new SqliteCommand("SELECT Text_Entry FROM MyTable;", db))
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                        entries.Add(reader.GetString(0));
                }
            }
            return entries;
        }
    }
}
