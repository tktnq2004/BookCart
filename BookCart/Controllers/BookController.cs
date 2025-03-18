using BookCart.Data;
using BookCart.DTO;
using BookCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCart.Controllers
{
    public class BookController : Controller
    {
        readonly BookCartDbContext _ctx;
        readonly IWebHostEnvironment _env;
        private const string IMG_FOLDER = "images";

        public BookController(BookCartDbContext ctx, IWebHostEnvironment env)
        {
            _env = env;
            _ctx = ctx;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _ctx.Books.ToListAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto book)
        {
            if (ModelState.IsValid)
            {
                Book item = new Book
                {
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    PriceDiscount = book.PriceDiscount,
                };

                if (book.Image != null && book.Image.Length > 0)
                {
                    string imgFolder = Path.Combine(_env.WebRootPath, IMG_FOLDER);
                    if (!Directory.Exists(imgFolder))
                    {
                        Directory.CreateDirectory(imgFolder);
                    }
                    string imgPath = Path.Combine(imgFolder, book.Image.FileName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        await book.Image.CopyToAsync(stream);
                    }
                    item.Image = book.Image.FileName;
                }

                _ctx.Books.Add(item);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}
