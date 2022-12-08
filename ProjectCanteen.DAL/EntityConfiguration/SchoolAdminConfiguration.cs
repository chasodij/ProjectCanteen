﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class SchoolAdminConfiguration : IEntityTypeConfiguration<SchoolAdmin>
    {
        public void Configure(EntityTypeBuilder<SchoolAdmin> builder)
        {
            builder.HasKey(admin => admin.Id);

            builder.HasOne(admin => admin.User);
            builder.HasOne(admin => admin.School).WithMany();
        }
    }
}
