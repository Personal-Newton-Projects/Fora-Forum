﻿using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<InterestModel> GetInterests()
    {
        return appDbContext.Interests;
    }

    [HttpGet("{id}")]
    public InterestModel GetInterest(int id)
    {
        return appDbContext.Interests.Where(i => i.Id == id).FirstOrDefault();
    }

    [HttpGet("/name/{name}")]
    [Obsolete("Does not work")]
    public InterestModel GetInterestByName(string name)
    {
        return appDbContext.Interests.Where(i => i.Name.ToLower() == name.ToLower()).FirstOrDefault();
    }
    [HttpPut]
    public async Task<ActionResult> UpdateUser(UserModel user)
    {
        // var dbUser = await appDbContext.Users
        //     .Include(u => u.UserInterests)
        //     .FirstOrDefaultAsync(u => u.Id == id);
        // dbUser = user;
        var dbUser = await appDbContext.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
        dbUser = user;
        await appDbContext.SaveChangesAsync();
        return Ok(dbUser);
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