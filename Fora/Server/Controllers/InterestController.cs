using Fora.Client.Managers;
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
    
    [HttpPost]
    public async Task<ActionResult<InterestModel>> CreateInterest(CreateInterestsModel thisInterest)
    {
        if (thisInterest != null)
        {
            InterestModel interest = new InterestModel()
            {
                Name = thisInterest.Name,
                UserId = thisInterest.UserID
            };
            if(appDbContext.Interests.Any(i => i.Name == interest.Name))
            {
                return BadRequest();
            }
            await appDbContext.Interests.AddAsync(interest);
            await appDbContext.SaveChangesAsync();
            return Ok(interest);   
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<InterestModel>> RemoveInterest(int id)
    {
        if (id != 0)
        {
            InterestModel interest = appDbContext.Interests.FirstOrDefault(x => x.Id == id);
            if (interest == null) return BadRequest();
            appDbContext.Remove(interest);
            appDbContext.SaveChangesAsync();
            return Ok(interest);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPut]
    public async Task<ActionResult> UpdateInterest(UpdateCreatedInterestModel thisInterest)
    {
        if (thisInterest != null)
        {
            InterestModel interest = await GetInterest(thisInterest.InterestID);
            interest.Name = thisInterest.InterestName;
            appDbContext.Update(interest);
            appDbContext.SaveChanges();
            return Ok(interest);
        }
        return NotFound();
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