﻿using Microsoft.AspNetCore.Mvc;

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
    // GET: api/interest
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
    
}