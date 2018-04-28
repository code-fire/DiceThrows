using DiceThrows.EntityFramework.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.EntityFramework.Database
{
    public class DB : DbContext
    {
        public static int RequiredDatabaseVersion = 2;

        public DB() : base(nameOrConnectionString: "DiceThrows")
        {
            //disable Migrations (not supported for SQLite, see Devart libraries if Migrations and more are needed)
            System.Data.Entity.Database.SetInitializer<DB>(null);
        }

        public DbSet<SchemaInfo> SchemaInfoes { get; set; }
        public DbSet<Dice> Dices { get; set; }
        public DbSet<RollResult> RollResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //no special relation   
        }

        public void Initialize()
        {
            using (DB DBContext = new DB())
            {
                int currentVersion = 0;
                DBHelper dbHelper = null;

                DBContext.Database.ExecuteSqlCommand(DBHelper.CreateSchemaInfoIfNotExists());

                if (DBContext.SchemaInfoes.Count() > 0)
                    currentVersion = DBContext.SchemaInfoes.Max(x => x.Version);

                //No need to load sql querry strings if no DB update is needed
                if (currentVersion < RequiredDatabaseVersion)
                    dbHelper = new DBHelper();

                while (currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach (string migration in dbHelper.Migrations[currentVersion])
                    {
                        DBContext.Database.ExecuteSqlCommand(migration);
                    }
                    DBContext.SchemaInfoes.Add(new SchemaInfo() { Version = currentVersion });
                    DBContext.SaveChanges();
                }
            }
        }

        public void Compact()
        {
            using (DB DBContext = new DB())
            {
                DBContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "vacuum;");
                DBContext.SaveChanges();
            }
        }
    }
}
