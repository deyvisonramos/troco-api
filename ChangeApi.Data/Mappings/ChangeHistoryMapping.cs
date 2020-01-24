using ChangeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeApi.Data.Mappings
{
    public class ChangeHistoryMapping : IEntityTypeConfiguration<ChangeHistory>
    {
        public void Configure(EntityTypeBuilder<ChangeHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Itens).WithOne();
        }
    }
}
