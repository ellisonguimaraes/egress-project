using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Address ef configuration
/// </summary>
public class AddressEntityConfiguration : BaseEntityConfiguration<Address>
{
    #region Constants
    private const string TABLE_NAME = "Address";
    private const string STREET_DB_PROPERTY_NAME = "street";
    private const byte STREET_DB_PROPERTY_LENGTH = 45;
    private const string DISTRICT_DB_PROPERTY_NAME = "district";
    private const byte DISTRICT_DB_PROPERTY_LENGTH = 45;
    private const string CITY_DB_PROPERTY_NAME = "city";
    private const byte CITY_DB_PROPERTY_LENGTH = 45;
    private const string STATE_DB_PROPERTY_NAME = "state";
    private const byte STATE_DB_PROPERTY_LENGTH = 45;
    private const string COUNTRY_DB_PROPERTY_NAME = "country";
    private const byte COUNTRY_DB_PROPERTY_LENGTH = 45;
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    #endregion

    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.Street)
            .HasColumnName(STREET_DB_PROPERTY_NAME)
            .HasMaxLength(STREET_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.District)
            .HasColumnName(DISTRICT_DB_PROPERTY_NAME)
            .HasMaxLength(DISTRICT_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.City)
            .HasColumnName(CITY_DB_PROPERTY_NAME)
            .HasMaxLength(CITY_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.State)
            .HasColumnName(STATE_DB_PROPERTY_NAME)
            .HasMaxLength(STATE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Country)
            .HasColumnName(COUNTRY_DB_PROPERTY_NAME)
            .HasMaxLength(COUNTRY_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasIndex(e => e.PersonId)
            .IsUnique();

        builder.HasOne(e => e.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(e => e.PersonId);
        
        base.Configure(builder);
    }
}