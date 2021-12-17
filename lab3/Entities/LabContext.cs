using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EFCore.NamingConventions;

namespace lab1.Entities
{
    public class LabContext : DbContext
    {
        public DbSet<AcceptedEntry> AcceptedEntries { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Subcode> Subcodes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseNpgsql("Server=localhost;Port=5432;Database=jmotyka;User Id=jmotyka;Password=jmotyka;Include Error Detail=true")
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
                .HasForeignKey(a => new { a.ReportMonth, a.UserName })
                .IsRequired();
            modelBuilder.Entity<AcceptedEntry>()
                .HasOne(a => a.Activity)
                .WithMany(a => a.AcceptedEntries)
                .HasForeignKey(a => a.ActivityCode)
                .IsRequired();
            modelBuilder.Entity<AcceptedEntry>()
                .Property(a => a.Timestamp)
                .IsRowVersion()
                .IsRequired();

            // Activity
            modelBuilder.Entity<Activity>()
                .HasKey(a => a.ActivityCode);
            modelBuilder.Entity<Activity>()
                .Property(a => a.ActivityName)
                .IsRequired();
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
                .HasForeignKey(a => a.ManagerName)
                .IsRequired();

            // Entry
            modelBuilder.Entity<Entry>()
                .HasKey(e => new { e.ReportMonth, e.UserName, e.ActivityCode, e.EntryPid });
            modelBuilder.Entity<Entry>()
                .Property(e => e.EntryPid)
                .ValueGeneratedOnAdd();
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
                .HasOne(e => e.Activity)
                .WithMany(s => s.Entries)
                .HasForeignKey(e => e.ActivityCode)
                .IsRequired();
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Report)
                .WithMany(r => r.Entries)
                .HasForeignKey(e => new { e.ReportMonth, e.UserName })
                .IsRequired();
            modelBuilder.Entity<Entry>()
                .Property(e => e.Timestamp)
                .IsRowVersion()
                .IsRequired();

            // Report
            modelBuilder.Entity<Report>()
                .HasKey(r => new { r.ReportMonth, r.UserName });
            modelBuilder.Entity<Report>()
                .Property(r => r.Frozen);
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UserName)
                .IsRequired();

            // Subcode
            modelBuilder.Entity<Subcode>()
                .HasKey(s => new { s.ActivityCode, s.SubactivityCode });
            modelBuilder.Entity<Subcode>()
                .HasOne(s => s.Activity)
                .WithMany(a => a.Subcodes)
                .HasForeignKey(s => s.ActivityCode)
                .IsRequired();

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserName);
        }
    }
}
