using Egress.Application.Queries.Responses;
using Egress.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.Application.Queries.Person.GetPersonByDocument;

public record GetPersonByDocumentCommand : IRequest<PersonCommandResponse>
{
    [BindProperty(Name = "document")]
    public string? Document { get; set; }

    [BindProperty(Name = "document_type")]
    public DocumentType? DocumentType { get; set; }
}
