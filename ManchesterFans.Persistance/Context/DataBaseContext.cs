using Microsoft.EntityFrameworkCore;
using ManchesterFans.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using ManchesterFans.Domain.Entities.Pages;
using ManchesterFans.Domain.Entities.Site;
using ManchesterFans.Application.Interfaces.Context;
using ManchesterFans.Common.Utilities;

namespace ManchesterFans.Persistance.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        //User Tables
        public DbSet<User> Users { get; set; }

        //Page Tables
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageComments> PageComments { get; set; }
        public DbSet<PageLikes> PageLikes { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }

        //Site Tables
        public DbSet<Header> Headers { get; set; }
        public DbSet<HeaderLinks> HeaderLinks { get; set; }
        public DbSet<SliderPosts> SliderPosts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.; Initial Catalog=ManchesterFans_DB ; Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);


            SetQueryFilter(modelBuilder);

        }

        private static void SetQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<PageComments>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<PageGroup>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<HeaderLinks>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<PageLikes>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<SliderPosts>().HasQueryFilter(e => !e.IsRemoved);
            modelBuilder.Entity<User>().HasIndex(e=> e.Username);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User() { image = "Default.png", Level = 10, LoginId = 1, Password = "Admin".ToSHA256(), Username = "MatinKing" });
            modelBuilder.Entity<Header>().HasData(new Header() { Id = 1, SiteName = "Manchester Fans" });
        }
    }
}
