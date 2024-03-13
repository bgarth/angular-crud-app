using angular_crud_app.Server.Data;
using angular_crud_app.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angular_crud_app.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorialsController : Controller
    {
        // Connects to the database
        private readonly Angular_crud_appDbContext _context;
        public TutorialsController(Angular_crud_appDbContext angular_Crud_AppDbContext)
        {
            _context = angular_Crud_AppDbContext;
        }

        // Get all tutorials (get request)
        [HttpGet]
        public async Task<IActionResult> GetAllTutorials()
        {
            var tutorials = await _context.Tutorials.ToListAsync();
            return Ok(tutorials);
        }

        // Create tutorial (post request)
        [HttpPost]
        public async Task<IActionResult> AddTutorial([FromBody] Tutorial tutorialRequest)
        {
            tutorialRequest.Id = Guid.NewGuid();

            await _context.Tutorials.AddAsync(tutorialRequest);
            await _context.SaveChangesAsync();

            return Ok(tutorialRequest);
        }

        // Get request (by id)
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTutorial([FromRoute] Guid id)
        {
            var tutorial = await _context.Tutorials.FirstOrDefaultAsync(x => x.Id == id);

            if (tutorial == null)
            {
                return NotFound();
            }

            return Ok(tutorial);
        }

        // Edit tutorial
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTutorial([FromRoute] Guid id, Tutorial tutorialRequest)
        {
            var tutorial = await _context.Tutorials.FindAsync(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            tutorial.Title = tutorialRequest.Title;
            tutorial.Description = tutorialRequest.Description;
            tutorial.Published = tutorialRequest.Published;

            await _context.SaveChangesAsync();

            return Ok(tutorial);
        }

        // Delete a specific tutorial
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTutorial([FromRoute] Guid id)
        {
            var tutorial = await _context.Tutorials.FindAsync(id);

            if (tutorial == null)
            {
                return NotFound();
            }
            _context.Tutorials.Remove(tutorial);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Delete all tutorials
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTutorials()
        {
            var tutorials = await _context.Tutorials.ToListAsync();
            foreach(var tutorial in tutorials)
            {
                _context.Tutorials.Remove(tutorial);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        
        // Used to search for tutorials
        [HttpGet]
        [Route("{title}")]
        public async Task<IActionResult> GetTutorialByTitle([FromRoute] String title)
        {
            // Get all tutorials
            var allTutorials = await _context.Tutorials.ToListAsync();

            // Used to return search results
            List<Tutorial> tutorials = [];

            if (allTutorials == null)
            {
                return NotFound();
            }

            // Loop through all tutorials
            foreach (var tutorial in allTutorials)
            {
                // Add tutorial to search results if the title contains the search query
                if (tutorial.Title.ToLower().Contains(title.ToLower()))
                {
                    tutorials.Add(tutorial);
                }
            }
            // Return search results
            return Ok(tutorials);
        }
        
    }
}
