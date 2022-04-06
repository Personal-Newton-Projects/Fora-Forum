using Microsoft.AspNetCore.Mvc;

namespace Fora.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController : Controller
    {
        private readonly AppDbContext appDbContext;

        public ThreadController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public IEnumerable<ThreadModel> Get()
        {
            return appDbContext.Threads;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ThreadModel thread)
        {
            await appDbContext.Threads.AddAsync(thread);
            await appDbContext.SaveChangesAsync();
            return Ok(thread);
        }
      
    }
}
