using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Context
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"server=.;database=cakeShop;uid=sa;pwd=123");
            // server=rajje.db.elephantsql.com;
            // database=etnbnasf;
            // password=mQU7BOsKE5G580b8aO7akJYZxzOUwMhh;
            // port=5432;
            // url=	postgres://etnbnasf:mQU7BOsKE5G580b8aO7akJYZxzOUwMhh@rajje.db.elephantsql.com:5432/etnbnasf;

            optionsBuilder.UseNpgsql(@"UserID=etnbnasf;Password=mQU7BOsKE5G580b8aO7akJYZxzOUwMhh;Server=rajje.db.elephantsql.com;Port=5432;Database=etnbnasf;Integrated Security=true;Pooling=true;");

            /*optionsBuilder.UseNpgsql(@"UserID=postgres;Password=Mete2626;Server=.;Port=5432;Database=cakeShop;Integrated Security=true;Pooling=true;");*/

        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
