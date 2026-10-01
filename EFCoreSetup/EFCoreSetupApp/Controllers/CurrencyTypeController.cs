using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetupApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyTypeController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CurrencyTypeController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyType>>> GetAll()
        {
            return Ok(await _context.CurrencyTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyType>> GetById(int id)
        {
            var currencyType = await _context.CurrencyTypes.FindAsync(id);

            if (currencyType == null)
            {
                return NotFound();
            }

            return Ok(currencyType);
        }
    }
}
