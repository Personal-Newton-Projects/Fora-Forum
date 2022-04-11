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
                .Include(u => u.Messages)
                .Include(u => u.UserRole)
                .ToListAsync<UserModel>();

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

                foreach(RoleModel role in appDbContext.Roles)
                {
                    if(user.UserRole.RoleId == role.Id)
                    {
                        user.UserRole.Role = role;
                    }
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
        public async Task<ActionResult> Post(LoginModel login)
        {
            UserModel user = new UserModel()
            {
                Username = login.Username,
                UserRole = new UserRoleModel()
                {
                    RoleId = 1
                }
            };

            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PostUserUpdateModel postUser)
        {
            var dbUser = await Get(postUser.Id);
            if(dbUser != null)
            {
                if(postUser.NewName != null)
                {
                    dbUser.Username = postUser.NewName;
                }
                dbUser.Deleted = postUser.DeleteUser;
                dbUser.Banned = postUser.BanUser;

                await appDbContext.SaveChangesAsync();
                return Ok(dbUser);
            }
            return NoContent();
        }

        [HttpPut("interest/{id}")]
        public async Task<ActionResult> PutUserInterests(List<PostUserInterestsModel> chosenInterests, int id)
        {
            if (chosenInterests != null)
            {
                var dbUser = await Get(id);
                List<UserInterestModel> IntereststoAdd = new List<UserInterestModel>();
                if(dbUser != null)
                {
                    foreach (var chosenInterest in chosenInterests)
                    {

                        UserInterestModel userInterest = new UserInterestModel()
                        {
                            UserId = chosenInterest.UserID,
                            User = dbUser,
                            InterestId = chosenInterest.InterestID
                        };
                        IntereststoAdd.Add(userInterest);

                    }
                }
                dbUser.UserInterests = IntereststoAdd;
                await appDbContext.SaveChangesAsync();
                return Ok(chosenInterests);
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
