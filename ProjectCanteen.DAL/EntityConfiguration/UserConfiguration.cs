using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName).HasMaxLength(Constants.MaxNameLength);
            builder.Property(user => user.LastName).HasMaxLength(Constants.MaxNameLength);
            builder.Property(user => user.Patronymic).HasMaxLength(Constants.MaxNameLength);
            builder.Property(user => user.Email).HasMaxLength(Constants.MaxEmailLength);
        }
    }
}
