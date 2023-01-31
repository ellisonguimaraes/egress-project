using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Person ef configuration
/// </summary>
public class PersonEntityConfiguration : BaseEntityConfiguration<Person>
{
    #region Constants
    private const string TABLE_NAME = "Person";
    private const string CPF_DB_PROPERTY_NAME = "cpf";
    private const byte CPF_DB_PROPERTY_LENGTH = 11;
    private const string NAME_DB_PROPERTY_NAME = "name";
    private const byte NAME_DB_PROPERTY_LENGTH = 200;
    private const string BIRTH_DATE_DB_PROPERTY_NAME = "birth_date";
    private const string SEX_DB_PROPERTY_NAME = "sex";
    private const string EMAIL_DB_PROPERTY_NAME = "email";
    private const byte EMAIL_DB_PROPERTY_LENGTH = 80;
    private const string PHONE_NUMBER_DB_PROPERTY_NAME = "phone_number";
    private const byte PHONE_NUMBER_DB_PROPERTY_LENGTH = 45;
    private const string PERFIL_IMAGE_DB_PROPERTY_NAME = "perfil_image";
    private const byte PERFIL_IMAGE_DB_PROPERTY_LENGTH = 200;
    private const string EXPOSE_DATA_DB_PROPERTY_NAME = "expose_data";
    private const string PERSON_TYPE_DB_PROPERTY_NAME = "person_type";
    #endregion
    
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(TABLE_NAME);

        builder.Property(e => e.Cpf)
            .HasColumnName(CPF_DB_PROPERTY_NAME)
            .HasMaxLength(CPF_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Name)
            .HasColumnName(NAME_DB_PROPERTY_NAME)
            .HasMaxLength(NAME_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.BirthDate)
            .HasColumnName(BIRTH_DATE_DB_PROPERTY_NAME);
        
        builder.Property(e => e.Sex)
            .HasColumnName(SEX_DB_PROPERTY_NAME);
        
        builder.Property(e => e.Email)
            .HasColumnName(EMAIL_DB_PROPERTY_NAME)
            .HasMaxLength(EMAIL_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.PhoneNumber)
            .HasColumnName(PHONE_NUMBER_DB_PROPERTY_NAME)
            .HasMaxLength(PHONE_NUMBER_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.PerfilImage)
            .HasColumnName(PERFIL_IMAGE_DB_PROPERTY_NAME)
            .HasMaxLength(PERFIL_IMAGE_DB_PROPERTY_LENGTH);
        
        builder.Property(e => e.ExposeData)
            .HasColumnName(EXPOSE_DATA_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.PersonType)
            .HasColumnName(PERSON_TYPE_DB_PROPERTY_NAME)
            .IsRequired();
        
        base.Configure(builder);
    }
}