using App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace App.Repository
{
    public class AppDatabase : DbContext
    {
        public DbSet<UrlShort> Faculties { get; set; }

        public AppDatabase(): base("AppConnection")
        {
            //Database.SetInitializer<AppDatabase>(new AppDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }








}