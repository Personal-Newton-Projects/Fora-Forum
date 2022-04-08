using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IEnumerable<UserModel>> Get()
        {
            return await GetFullyIncludedUsers();
        }

        async Task<IEnumerable<UserModel>> GetFullyIncludedUsers()
        {
            List<UserModel> dbUsers = await appDbContext.Users
                .Include(u => u.UserInterests)
                .Include(u => u.Interests)
                .Include(u => u.Threads)
                .Include(u => u.Messages).ToListAsync<UserModel>();

            foreach (UserModel user in dbUsers)
            {
                foreach(UserInterestModel userInterests in user.UserInterests)
                {
                    foreach (InterestModel interest in appDbContext.Interests)
                    {
                        if(userInterests.InterestId == interest.Id)
                        {
                            userInterests.Interest = interest; // Set Interest to interest
                        }
                    }
                    userInterests.User = user; //Set userInterests user to this user
                }

                foreach(InterestModel interest in user.Interests)
                {
                    interest.User = user;
                }

            }

            return dbUsers;

        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<UserModel> Get(int id)
        {
            var result = await GetFullyIncludedUsers();
            return result.SingleOrDefault(u => u.Id == id);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult> Post(UserModel user)
        {
            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UserModel user)
        {
            var dbUser = appDbContext.Users.SingleOrDefault(u => u.Id == user.Id);
            if(dbUser != null)
            {
                dbUser = user;
                await appDbContext.SaveChangesAsync();
                return Ok(dbUser);
            }
            return NoContent();
        }

        [HttpPut("interest/{id}")]
        public async Task<ActionResult> PutUserInterests(int id, List<UserInterestModel> userInterests)
        {
            var dbUser = await Get(id);
            if(dbUser != null)
            {
                dbUser.UserInterests = userInterests;
                await appDbContext.SaveChangesAsync();
                return Ok(dbUser);
            }
            return NoContent();
        }

        //// POST: UserController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
