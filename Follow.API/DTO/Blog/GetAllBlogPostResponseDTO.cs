namespace Follow.API.DTO.Blog
{
    public class GetAllBlogPostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public BlogPostCategoryDTO Category { get; set; }
    }

    public class  BlogPostCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
