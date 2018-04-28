using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.EntityFramework.Database
{
    class DBHelper
    {
        public DBHelper()
        {
            Migrations = new Dictionary<int, IList<string>>();

            //To apply a new DB update, just add a new "Migration function" here, and add it to the Migrations Dictionary
            MigrationVersion1();
            MigrationVersion2();
        }

        public Dictionary<int, IList<string>> Migrations { get; set; }

        public static string CreateSchemaInfoIfNotExists()
        {
            string sql_query = string.Empty;

            sql_query = "CREATE TABLE IF NOT EXISTS \"tbl_schemaInfo\" (\"id\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, \"version\" int);";

            return sql_query;
        }

        private void MigrationVersion1()
        {
            IList<string> steps = new List<string>();

            steps.Add("CREATE TABLE IF NOT EXISTS \"tbl_dice\" (\"dice_id\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, \"color\" INTEGER);");
            steps.Add("CREATE TABLE IF NOT EXISTS \"tbl_roll_result\" (\"roll_result_id\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, \"dice_id\" INTEGER NOT NULL, FOREIGN KEY (dice_id) REFERENCES tbl_dice(dice_id));");
            Migrations.Add(1, steps);
        }

        private void MigrationVersion2()
        {
            IList<string> steps = new List<string>();

            steps.Add("ALTER TABLE \"tbl_roll_result\" ADD COLUMN \"roll_value\" INTEGER;");
            Migrations.Add(2, steps);
        }
    }
}
