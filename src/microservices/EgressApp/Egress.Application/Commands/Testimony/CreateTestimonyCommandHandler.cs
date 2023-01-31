using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Testimony;

public class CreateTestimonyCommandHandler : IRequestHandler<CreateTestimonyCommand, Guid>
{
    
    public async Task<Guid> Handle(CreateTestimonyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}