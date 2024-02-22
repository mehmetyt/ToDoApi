using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data;
using ToDoApi.Dtos;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _toDoContext;

        public TodoItemsController(TodoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetItem()
        {
            return _toDoContext.ToDoItems.OrderBy(x => x.Done).ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<TodoItem> GetItem(int id)
        {
            var item = _toDoContext.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //Ekleme
        [HttpPost]
        public ActionResult<PostTodoItemDto> PostItem(PostTodoItemDto dto)
        {

            if (ModelState.IsValid)
            {
                var newTodoItem = new TodoItem()
                {
                    Title = dto.Title,
                    Done = dto.Done,
                };
                _toDoContext.Add(newTodoItem);
                _toDoContext.SaveChanges();
                return CreatedAtAction("GetItem", new { id = newTodoItem.Id }, newTodoItem);
            }
            return BadRequest(ModelState);
        }

        //Guncelleme

        [HttpPut("{id:int}")]
        public IActionResult PutItem(int id, TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();

            if (!_toDoContext.ToDoItems.Any(x => x.Id == id))
                return NotFound();

            if (ModelState.IsValid)
            {
                _toDoContext.Update(item);
                _toDoContext.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        //Silme
        [HttpDelete("{id:int}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _toDoContext.ToDoItems.Find(id);
            if (item == null)
                return NotFound();
            _toDoContext.Remove(item);
            _toDoContext.SaveChanges();
            return NoContent();
        }
    }
}
