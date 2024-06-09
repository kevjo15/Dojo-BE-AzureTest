using AutoMapper;
using Domain_Layer.CommandOperationResult;
using Domain_Layer.Models.Content;
using Infrastructure_Layer.Repositories.Content;
using MediatR;

namespace Application_Layer.Commands.ContentCommands
{
    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, OperationResult<bool>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;
        public CreateContentCommandHandler(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<bool>> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contentModel = _mapper.Map<ContentModel>(request.ContentDTO);
                await _contentRepository.CreateContentAsync(contentModel);
                return new OperationResult<bool> { Success = true, Message = "Content successfully created" };
            }
            catch (Exception ex)
            {
                return new OperationResult<bool> { Success = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
