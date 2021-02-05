using Bookcase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookcase.Data
{
    public class BookcaseContext : DbContext
    {
        public BookcaseContext (DbContextOptions<BookcaseContext> options) 
            : base(options)
        {
        }

        public DbSet<AppUser> AppUser { get; set; }

        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }

        // we add our book isbn
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            for (int i=1; i<11; i++)
            {
                builder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        Id = i,
                        Name = $"admin{i}",
                        Email = $"admin{i}@admin.pl",
                        Password = $"adminadmin{i}",
                        Role = Roles.Admin
                    }); ;

                builder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        Id = i+10,
                        Name = $"member{i}",
                        Email = $"member{i}@member.pl",
                        Password = $"membermember{i}",
                        Role = Roles.Member
                    });
            }


        }

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=BookcaseContextDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }*/
    }

}
