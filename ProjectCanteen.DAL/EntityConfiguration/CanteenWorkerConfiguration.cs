﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class CanteenWorkerConfiguration : IEntityTypeConfiguration<CanteenWorker>
    {
        public void Configure(EntityTypeBuilder<CanteenWorker> builder)
        {
            builder.HasKey(worker => worker.Id);

            builder.HasOne(worker => worker.User).WithOne().IsRequired();
            builder.HasOne(worker => worker.Canteen).WithMany(canteen => canteen.CanteenWorkers).IsRequired();
        }
    }
}
