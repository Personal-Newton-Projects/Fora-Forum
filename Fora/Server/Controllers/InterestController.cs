using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InterestController : Controller
{
    private readonly AppDbContext appDbContext;

    public InterestController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
    List<UserInterestModel> UserInterests { get; set; }
    
    [HttpGet]
    public async Task<IEnumerable<InterestModel>> GetInterests()
    {
        return await GetFullyIncludedInterests();
    }

    async Task<IEnumerable<InterestModel>> GetFullyIncludedInterests()
    {
        List<InterestModel> dbInterests = await appDbContext.Interests
            .Include(i => i.UserInterests)
            .Include(i => i.User)
            .Include(i => i.Threads)
            .ToListAsync();

        foreach (InterestModel interest in dbInterests)
        {

            foreach (ThreadModel thread in appDbContext.Threads)
            {
                if(thread.Interest == interest)
                {
                    interest.Threads.Add(thread);
                }
            }

        }

        return dbInterests;
    }

    [HttpGet("{id}")]
    public async Task<InterestModel> GetInterest(int id)
    {
        var result = await GetFullyIncludedInterests();
        return result.SingleOrDefault(i => i.Id == id);
    }

    [HttpGet("/name/{name}")]
    [Obsolete("Does not work")]
    public InterestModel GetInterestByName(string name)
    {
        return appDbContext.Interests.Where(i => i.Name.ToLower() == name.ToLower()).FirstOrDefault();
    }
    
    //[HttpPost]
    //public async Task<IActionResult> AddInterestForUser(UserInterestModel userInterest)
    //{
    //    // interest = appDbContext.Interests.FirstOrDefault(i => i.Id == id);
    //    // await appDbContext.Interests.AddAsync(interest);
    //    // await appDbContext.SaveChangesAsync();
    //    // return interest
    //    UserInterests.Add(userInterest);
    //    userInterest.User.UserInterests.Add(userInterest);
    //    return Ok(await GetDbInterests());
    //}
    
    
}