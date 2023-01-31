using Egress.Domain.Entities;
using Egress.Infra.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Context;

/// <summary>
/// EF Database Context
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    public DbSet<Address> Addresses { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Employment> Employments { get; set; }
    
    public DbSet<Highlights> Highlights { get; set; }
    
    public DbSet<PersonCourse> PersonCourses { get; set; }
    
    public DbSet<Specialization> Specializations { get; set; }
    
    public DbSet<Testimony> Testimonies { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PersonEntityConfiguration().Configure(modelBuilder.Entity<Person>());
        new CourseEntityConfiguration().Configure(modelBuilder.Entity<Course>());
        new PersonCourseEntityConfiguration().Configure(modelBuilder.Entity<PersonCourse>());
        new AddressEntityConfiguration().Configure(modelBuilder.Entity<Address>());
        new EmploymentEntityConfiguration().Configure(modelBuilder.Entity<Employment>());
        new HighlightsEntityConfiguration().Configure(modelBuilder.Entity<Highlights>());
        new SpecializationEntityConfiguration().Configure(modelBuilder.Entity<Specialization>());
        new TestimonyEntityConfiguration().Configure(modelBuilder.Entity<Testimony>());
        
        base.OnModelCreating(modelBuilder);
    }
}