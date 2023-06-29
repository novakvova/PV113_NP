using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDatabase.Data.Entities;

namespace TestDatabase.Data
{
    public class AppEFContext : DbContext
    {
        public AppEFContext()
        {
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string strCon = "Server=.;Database=pv113;Trusted_Connection=True;TrustServerCertificate=true;";
            //string strCon = "Server=komaxasomeecom.mssql.somee.com;Database=komaxasomeecom;User Id=vovavovan_SQLLogin_2;Password=Qwerty1-;TrustServerCertificate=true;";
            string strCon = "Data Source=mydata.sqlite;";
            //optionsBuilder.UseSqlServer(strCon);
            optionsBuilder.UseSqlite(strCon);
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
