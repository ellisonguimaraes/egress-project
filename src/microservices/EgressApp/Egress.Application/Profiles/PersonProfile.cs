using AutoMapper;
using Egress.Application.Queries.Person;
using Egress.Application.Queries.Responses;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonCommandResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ExposeData, opt => opt.MapFrom(src => src.ExposeData))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PerfilImage, opt => opt.MapFrom(src => src.PerfilImage))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType.ToString()))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressCommandResponse
            {
                Id = src.Address.Id,
                Street = src.Address.Street,
                City = src.Address.City,
                Country = src.Address.Country,
                State = src.Address.State,
                District = src.Address.District,
                CreatedAt = src.Address.CreatedAt,
                UpdatedAt = src.Address.UpdatedAt
            }))
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.PersonCourses.Select(pc => new CourseCommandResponse
            {
                Id = pc.Course.Id,
                CourseName = pc.Course.CourseName
            })))
            .ForMember(dest => dest.Employments, opt => opt.MapFrom(src => src.Employments.Select(e => new EmploymentCommandResponse
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                EndDate = e.EndDate,
                Enterprise = e.Enterprise,
                Initiative = e.Initiative.ToString(),
                Role = e.Role,
                SalaryRange = e.SalaryRange,
                Section = e.Section,
                StartDate = e.StartDate,
                Status = e.Status
            })))
            .ForMember(dest => dest.Highlights, opt => opt.MapFrom(src => src.Highlights.Select(h => new HighlightsCommandResponse
            {
                Id = h.Id,
                CreatedAt = h.CreatedAt,
                UpdatedAt = h.UpdatedAt,
                Description = h.Description,
                Link = h.Link,
                Title = h.Title
            })))
            .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src => src.Specializations.Select(e => new SpecializationCommandResponse
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                Status = e.Status,
                StartDate = e.StartDate,
                CourseName = e.CourseName,
                EndDate = e.EndDate,
                InstitutionName = e.InstitutionName,
                Modality = e.Modality.ToString()
            })))
            .ForMember(dest => dest.Testimonies, opt => opt.MapFrom(src => src.Testimonies.Select(e => new TestimonyCommandResponse
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                Content = e.Content,
                WasAccepted = e.WasAccepted
            })));

    }
}
