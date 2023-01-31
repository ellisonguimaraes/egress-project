using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Specialization ef configuration
/// </summary>
public class SpecializationEntityConfiguration : BaseEntityConfiguration<Specialization>
{
    #region Constants
    private const string TABLE_NAME = "Specialization";
    private const string COURSE_DB_PROPERTY_NAME = "course_name";
    private const byte COURSE_DB_PROPERTY_LENGTH = 100;
    private const string INSTITUTION_DB_PROPERTY_NAME = "institution_name";
    private const byte INSTITUTION_DB_PROPERTY_LENGTH = 100;
    private const string STATUS_DB_PROPERTY_NAME = "status";
    private const string MODALITY_DB_PROPERTY_NAME = "modality";
    private const string START_DATE_DB_PROPERTY_NAME = "start_date";
    private const string END_DATE_DB_PROPERTY_NAME = "end_date";
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    #endregion
    
    public override void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.CourseName)
            .HasColumnName(COURSE_DB_PROPERTY_NAME)
            .HasMaxLength(COURSE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.InstitutionName)
            .HasColumnName(INSTITUTION_DB_PROPERTY_NAME)
            .HasMaxLength(INSTITUTION_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Status)
            .HasColumnName(STATUS_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.Modality)
            .HasColumnName(MODALITY_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.StartDate)
            .HasColumnName(START_DATE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.EndDate)
            .HasColumnName(END_DATE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.Person)
            .WithMany(p => p.Specializations)
            .HasForeignKey(e => e.PersonId);
        
        base.Configure(builder);
    }
}