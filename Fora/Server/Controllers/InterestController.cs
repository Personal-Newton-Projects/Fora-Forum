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
    public async Task<IActionResult> GetInterests()
    {
        return base.Ok(await GetDbInterests());
    }
    [HttpGet]
    public async Task<List<InterestModel>> GetDbInterests()
    {
        return await appDbContext.Interests.Include(i => i.UserInterests).ToListAsync();
    }
    [HttpGet("{id}")]
    public InterestModel GetInterest(int id)
    {
        return appDbContext.Interests.Where(i => i.Id == id).FirstOrDefault();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddInterestForUser(UserInterestModel userInterest)
    {
        // interest = appDbContext.Interests.FirstOrDefault(i => i.Id == id);
        // await appDbContext.Interests.AddAsync(interest);
        // await appDbContext.SaveChangesAsync();
        // return interest
        UserInterests.Add(userInterest);
        userInterest.User.UserInterests.Add(userInterest);
        return Ok(await GetDbInterests());
    }
    
    
}