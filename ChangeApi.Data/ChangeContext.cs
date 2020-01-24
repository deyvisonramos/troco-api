using ChangeApi.Data.Mappings;
using ChangeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeApi.Data
{
    public class ChangeContext : DbContext
    {
        public ChangeContext(DbContextOptions<ChangeContext> options) : base(options)
        {
        }

        public DbSet<ChangeHistory> ChangeHistories { get; set; }
        public DbSet<ChangeHistoryItem> ChangeHistoryItens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ChangeHistoryMapping());
            base.OnModelCreating(builder);
        }
    }
}
