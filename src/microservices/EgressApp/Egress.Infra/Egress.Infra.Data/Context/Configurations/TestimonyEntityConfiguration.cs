using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Testimony ef configuration
/// </summary>
public class TestimonyEntityConfiguration : BaseEntityConfiguration<Testimony>
{
    #region Constants
    private const string TABLE_NAME = "Testimony";
    private const string CONTENT_DB_PROPERTY_NAME = "content";
    private const byte CONTENT_DB_PROPERTY_LENGTH = 100;
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    private const string WAS_ACCEPTED_DB_PROPERTY_NAME = "was_accepted";
    #endregion
    
    public override void Configure(EntityTypeBuilder<Testimony> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.Content)
            .HasColumnName(CONTENT_DB_PROPERTY_NAME)
            .HasMaxLength(CONTENT_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.Property(e => e.WasAccepted)
            .HasColumnName(WAS_ACCEPTED_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.Person)
            .WithMany(p => p.Testimonies)
            .HasForeignKey(e => e.PersonId);
        
        base.Configure(builder);
    }
}