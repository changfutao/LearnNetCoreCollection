using LearnNetCore.Basic.DbContexts;
using LearnNetCore.Basic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;
using ExpressionTreeToString;
using System.ComponentModel;

namespace LearnNetCore.Basic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TestDbContext _testDb;

        public ValuesController(TestDbContext testDb)
        {
            _testDb = testDb;
        }
        [HttpGet]
        public async Task<string> Get()
        {
            Log.Error("111");
            var sql = _testDb.Books.AsNoTracking().ToQueryString();
            var list = await _testDb.Books.AsNoTracking().ToListAsync();
            return "OK";
        }
        [HttpGet]
        public async Task Add()
        {
            BookEntity book = new BookEntity();
            book.Price = 10;
            book.AuthorName = "Ross";
            book.PubTime = DateTime.Now;
            book.Title = "C#程序设计";
            _testDb.Books.Add(book);
            await _testDb.SaveChangesAsync();
        }

        public async Task Edit()
        {
            BookEntity book = new BookEntity() { Id = 1 };
            book.Title = "CLR via C#";
            var entry= _testDb.Entry(book);
            entry.Property(a => a.Title).IsModified = true;
            Console.WriteLine(entry.DebugView.LongView);
            await _testDb.SaveChangesAsync();
        }

        public IActionResult GetInfo()
        {
            Expression<Func<BookEntity, bool>> e = b => b.Price == 10;
            var a = e.ToString("Object notation", "C#");
            var books = _testDb.Books.Where(e).ToList();
            return Ok();
        }
        [HttpGet]
        public string GetA(Color type)
        {
            return ((int)type).ToString();
        }
    }

    public enum Color
    {
        Red = 1,
        Green,
        Blue
    }
}
