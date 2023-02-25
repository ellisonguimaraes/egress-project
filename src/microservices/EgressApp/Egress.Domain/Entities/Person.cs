using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class Person : BaseEntity
{
    public string Cpf { get; set; }

    public string Name { get; set; }

    public DateTime? BirthDate { get; set; }
    
    public Sex? Sex { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
    
    public string? PerfilImage { get; set; }

    public bool ExposeData { get; set; }

    public PersonType PersonType { get; set; }

    #region Relationship
    public virtual Address? Address { get; set; }
    
    public List<PersonCourse> PersonCourses { get; set; }

    public List<Employment> Employments { get; set; }

    public List<Highlights> Highlights { get; set; }
    
    public List<Specialization> Specializations { get; set; }
    
    public List<Testimony> Testimonies { get; set; }
    #endregion
}