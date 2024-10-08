namespace Follow.API.DTO.Blog
{
    public class CreateBlogRequestDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
