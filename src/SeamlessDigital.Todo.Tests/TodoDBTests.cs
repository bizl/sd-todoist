using Microsoft.VisualBasic;
using NUnit.Framework;
using SeamlessDigital.Todo.Data;
using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;

namespace SeamlessDigital.Todo.Tests
{
    [TestFixture]
    public class TodoDBTests
    {
        private IRepository<TodoItem> _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new TodoRepository("Server=(localdb)\\mssqllocaldb;Database=Todoist;Trusted_Connection=True;MultipleActiveResultSets=true"); 
        }

        [Test]
        public void CanInsertTodoItem()
        {
            int affected = _repo.Insert(new TodoItem { UserId = 11, Title = "Do dishes" }, 99);
            Assert.That(1 == affected);
        }

        [Test]
        public void CanUpdateTodoItem()
        {
            int affected = _repo.Insert(new TodoItem { UserId = 11, Title = "Do dishes" }, 99);
            Assert.That(1 == affected);
        }


        [TestCase(2, "2024-12-11", 0)]
        [TestCase(null, "2024-12-13", 3)]
        [TestCase(5, null, 0)]
        public void CanFetchTodos(int priority, string dueDate, int expected)
        {
            var todo = new TodoItem 
            {
                Priority = priority
            };
            if( !string.IsNullOrEmpty(dueDate))
            {
                todo.DueDate = Convert.ToDateTime(dueDate);
            }

            var  items = _repo.Get(todo);
            Assert.That(expected == items.Count());
        }


    }
}
