namespace Domain_Layer.Models.Tag
{
    public class TagModel
    {
        public string TagId { get; set; } = Guid.NewGuid().ToString();
        public string TagName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
