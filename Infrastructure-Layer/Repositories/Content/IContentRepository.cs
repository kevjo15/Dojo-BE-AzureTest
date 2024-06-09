using Domain_Layer.Models.Content;

namespace Infrastructure_Layer.Repositories.Content
{
    public interface IContentRepository
    {
        Task CreateContentAsync(ContentModel contentModel);
    }
}
