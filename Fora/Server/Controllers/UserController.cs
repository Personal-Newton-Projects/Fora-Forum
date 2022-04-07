using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Server.Data;
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
        public IEnumerable<UserModel> Get()
        {
            return appDbContext.Users;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return appDbContext.Users.Where(u => u.Id == id).FirstOrDefault();
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
        public async Task<ActionResult> Put(int id, UserModel user)
        {
            //var dbUser = await appDbContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            //dbUser = user;
            appDbContext.Users.Update(user);
            await appDbContext.SaveChangesAsync();
            return Ok(user);
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
