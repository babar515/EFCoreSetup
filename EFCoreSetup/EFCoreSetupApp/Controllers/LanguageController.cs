using EFCoreSetupApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSetupApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly AppDBContext _context;

        public LanguageController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetAll()
        {
            //var titles = "Urdu";  
            //var languages = await _context.Languages.FromSqlRaw($"SELECT top 1 * FROM Language where Title = {0}" , titles).AsNoTracking().ToListAsync();
            //return Ok(languages);

            var title = "Urdu";

            var languages = await _context.Languages
                .FromSqlRaw(
                    "SELECT TOP 1 * FROM Language WHERE Title = {0}",
                    title)
                .AsNoTracking()
                .ToListAsync();

            return Ok(languages);
        }

        [HttpGet("GetAllLanguages")]
        public IActionResult GetAllLanguages()
        {
            return Ok(_context.Languages.ToList());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Language>> GetById(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        [HttpGet("GetByName/{title}")]
        public async Task<ActionResult<Language>> GetByName(string title)
        {
            var language = await _context.Languages.Where(x => x.Title == title).FirstOrDefaultAsync();
                
            //var language = await _context.Languages.FirstOrDefaultAsync(l => l.Title == title);

            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        [HttpPost("GetByIds")]
        public async Task<ActionResult<IEnumerable<Language>>> GetByIds([FromBody] List<int> ids)
        {
            var languages = await _context.Languages.Where(x => ids.Contains(x.Id))
                .Select(x => new Language
                {
                    Id = x.Id,
                    Title = x.Title,
                    //Description = x.Description
                })  
                .ToListAsync();

            return Ok(languages);
            

        }
    }
}


