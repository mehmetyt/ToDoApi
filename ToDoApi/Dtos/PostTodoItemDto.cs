using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Dtos
{
    public class PostTodoItemDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public bool Done { get; set; }
    }
}
