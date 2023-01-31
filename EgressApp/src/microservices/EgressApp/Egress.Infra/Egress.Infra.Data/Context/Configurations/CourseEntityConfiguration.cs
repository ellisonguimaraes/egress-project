using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Course ef configuration
/// </summary>
public class CourseEntityConfiguration : BaseEntityConfiguration<Course>
{
    #region Constants
    private const string TABLE_NAME = "Course";
    private const string COURSE_NAME_DB_PROPERTY_NAME = "course_name";
    private const byte COURSE_NAME_DB_PROPERTY_LENGTH = 100;
    #endregion
    
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.CourseName)
            .HasColumnName(COURSE_NAME_DB_PROPERTY_NAME)
            .HasMaxLength(COURSE_NAME_DB_PROPERTY_LENGTH)
            .IsRequired();

        base.Configure(builder);
    }
}