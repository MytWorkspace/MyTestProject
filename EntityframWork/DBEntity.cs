using DBModel;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityframWork
{
    public class DBEntity : DbContext
    {

        //定义属性，便于外部访问数据表
        public DbSet<Book> Books { get { return Set<Book>(); } }

        public DbSet<Person> Persons { get { return Set<Person>(); } }

        public DBEntity() : base("dbConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            ModelConfiguration.Configure(modelBuilder);
            var init = new SqliteCreateDatabaseIfNotExists<DBEntity>(modelBuilder);
            Database.SetInitializer(init);
        }

        public class ModelConfiguration
        {
            public static void Configure(DbModelBuilder modelBuilder)
            {
                ConfigureBookEntity(modelBuilder);
            }
            private static void ConfigureBookEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Book>();
                modelBuilder.Entity<Person>();
            }
        }
    }
}
