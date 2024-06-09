using Application_Layer.DTO_s.Content;
using Domain_Layer.CommandOperationResult;
using MediatR;

namespace Application_Layer.Commands.ContentCommands
{
    public class CreateContentCommand : IRequest<OperationResult<bool>>
    {
        public CreateContentDTO ContentDTO { get; }
        public CreateContentCommand(CreateContentDTO contentDTO)
        {
            ContentDTO = contentDTO;
        }
    }
}
