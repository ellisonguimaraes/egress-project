using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Base entity ef configuration
/// </summary>
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    #region Constants
    private const string ID_DB_PROPERTY_NAME = "id";
    private const byte ID_DB_PROPERTY_LENGTH = 36;
    private const string CREATED_AT_DB_PROPERTY_NAME = "created_at";
    private const string UPDATED_AT_DB_PROPERTY_NAME = "updated_at";
    #endregion
    
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName(ID_DB_PROPERTY_NAME)
            .HasMaxLength(ID_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.CreatedAt)
            .HasColumnName(CREATED_AT_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.UpdatedAt)
            .HasColumnName(UPDATED_AT_DB_PROPERTY_NAME)
            .IsRequired();
    }
}