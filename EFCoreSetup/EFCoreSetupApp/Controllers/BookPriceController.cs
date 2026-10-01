using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetupApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookPriceController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BookPriceController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookPrice>>> GetAll()
        {
            return Ok(await _context.BookPrices.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookPrice>> GetById(int id)
        {
            var bookPrice = await _context.BookPrices.FindAsync(id);

            if (bookPrice == null)
            {
                return NotFound();
            }

            return Ok(bookPrice);
        }
    }
}
