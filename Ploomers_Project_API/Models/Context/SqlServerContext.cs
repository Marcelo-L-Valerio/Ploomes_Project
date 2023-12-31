﻿using Microsoft.EntityFrameworkCore;
using Ploomers_Project_API.Models.Entities;

namespace Ploomers_Project_API.Models.Context
{
    public class SqlServerContext : DbContext
    {
        // Database mapping and migrations creator
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>(c =>
            {
                c.HasKey(cl => cl.Id);

                c.Property(cl => cl.Name).IsRequired().HasMaxLength(100);
                c.Property(cl => cl.Type).IsRequired();
                c.Property(cl => cl.Document).IsRequired().HasMaxLength(14);
                c.Property(cl => cl.Address).IsRequired().HasMaxLength(150);

                c.HasMany(cl => cl.Contacts);
                c.HasMany(cl => cl.Sales);
            });
            builder.Entity<Client>().HasIndex(x => new { x.Document }).IsUnique();

            builder.Entity<Contact>(c =>
            {
                c.HasKey(co => co.Id);

                c.Property(co => co.Type).IsRequired().HasMaxLength(20);
                c.Property(co => co.Info).IsRequired().HasMaxLength(100);
                c.Property(co => co.ClientId).IsRequired();
            });

            builder.Entity<Sale>(s =>
            {
                s.HasKey(sa => sa.Id);

                s.Property(sa => sa.Product).IsRequired().HasMaxLength(50);
                s.Property(sa => sa.Amount).IsRequired();
                s.Property(sa => sa.Value).IsRequired();
                s.Property(sa => sa.Date).IsRequired();
                s.Property(sa => sa.ClientId).IsRequired();
                s.Property(sa => sa.EmployeeId).IsRequired();
            });

            builder.Entity<Employee>(e =>
            {
                e.HasKey(em => em.Id);

                e.Property(em => em.Email).IsRequired().HasMaxLength(100);
                e.Property(em => em.FirstName).HasMaxLength(50);
                e.Property(em => em.LastName).HasMaxLength(50);
                e.Property(em => em.Password).IsRequired();
                e.Property(em => em.RefreshToken).IsRequired(false);

                e.HasMany(em => em.Sales);

            });
            builder.Entity<Employee>().HasIndex(x => new { x.Email }).IsUnique();
        }
    }
}
