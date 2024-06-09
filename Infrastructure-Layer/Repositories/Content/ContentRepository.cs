using Domain_Layer.Models.Content;
using Infrastructure_Layer.Database;

namespace Infrastructure_Layer.Repositories.Content
{
    public class ContentRepository : IContentRepository
    {
        private readonly DojoDBContext _dojoDBContext;

        public ContentRepository(DojoDBContext dojoDBContext)
        {
            _dojoDBContext = dojoDBContext;
        }
        public async Task CreateContentAsync(ContentModel content)
        {
            _dojoDBContext.ContentModel.Add(content);
            await _dojoDBContext.SaveChangesAsync();
        }
    }
}
