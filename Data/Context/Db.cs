using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class Db : DbContext
    {
        public Db() : base(options: new DbContextOptionsBuilder<Db>().UseSqlServer("Data Source=DESKTOP-D516960\\MSSQLSERVER01;Initial Catalog=proje;Integrated Security=True;Encrypt=False").Options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}