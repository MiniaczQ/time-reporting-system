using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EFCore.NamingConventions;

namespace lab1.Entities
{
    public class AppContext : DbContext
    {
        public DbSet<AcceptedEntry> acceptedEntries { get; set; }
        public DbSet<Activity> activities { get; set; }
        public DbSet<Entry> entries { get; set; }
        public DbSet<Report> reports { get; set; }
        public DbSet<Subcode> subcodes { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=jmotyka;Username=jmotyka;Password=jmotyka")
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Accepted entry
            modelBuilder.Entity<AcceptedEntry>()
                .HasKey(a => new { a.ReportMonth, a.UserName, a.ActivityCode });
            modelBuilder.Entity<AcceptedEntry>()
                .Property(a => a.Time)
                .IsRequired();
            modelBuilder.Entity<AcceptedEntry>()
                .HasOne(a => a.Report)
                .WithMany(r => r.AcceptedEntries)
                .HasForeignKey(a => new { a.ReportMonth, a.UserName });
            modelBuilder.Entity<AcceptedEntry>()
                .HasOne(a => a.Activity)
                .WithMany(a => a.AcceptedEntries)
                .HasForeignKey(a => a.ActivityCode);

            // Activity
            modelBuilder.Entity<Activity>()
                .HasKey(a => a.ActivityCode);
            modelBuilder.Entity<Activity>()
                .Property(a => a.Budget)
                .IsRequired();
            modelBuilder.Entity<Activity>()
                .Property(a => a.Active)
                .IsRequired();
            modelBuilder.Entity<Activity>()
                .Property(a => a.ManagerName)
                .IsRequired();
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Manager)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.ManagerName);

            // Entry
            modelBuilder.Entity<Entry>()
                .HasKey(e => new { e.ReportMonth, e.UserName, e.ActivityCode, e.SubactivityCode });
            modelBuilder.Entity<Entry>()
                .Property(e => e.Date)
                .IsRequired();
            modelBuilder.Entity<Entry>()
                .Property(e => e.Time)
                .IsRequired();
            modelBuilder.Entity<Entry>()
                .Property(e => e.Description);
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Subcode)
                .WithMany(s => s.Entries)
                .HasForeignKey(e => new { e.ActivityCode, e.SubactivityCode });
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Report)
                .WithMany(r => r.Entries)
                .HasForeignKey(e => new { e.ReportMonth, e.UserName });

            // Report
            modelBuilder.Entity<Report>()
                .HasKey(r => new { r.UserName, r.ReportMonth });
            modelBuilder.Entity<Report>()
                .Property(r => r.Frozen);
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UserName);

            // Subcode
            modelBuilder.Entity<Subcode>()
                .HasKey(s => new { s.ActivityCode, s.SubactivityCode });
            modelBuilder.Entity<Subcode>()
                .HasOne(s => s.Activity)
                .WithMany(a => a.Subcodes)
                .HasForeignKey(s => s.ActivityCode);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserName);
        }
    }
}
