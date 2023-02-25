using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Testimony;

public class CreateTestimonyCommandHandler : IRequestHandler<CreateTestimonyCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Testimony> _testimonyRepository;

    public CreateTestimonyCommandHandler(IRepository<Domain.Entities.Testimony> testimonyRepository)
    {
        _testimonyRepository = testimonyRepository;
    }

    public async Task<Guid> Handle(CreateTestimonyCommand request, CancellationToken cancellationToken)
    {
        var testimony = new Domain.Entities.Testimony
        {
            Content = "coco",
            WasAccepted = false
        };

        var result = await _testimonyRepository.CreateAsync(testimony);

        return result.Id;
    }
}