using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Server.Data;
using Microsoft.AspNetCore.Identity;

namespace Fora.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityUserController : Controller
    {
        private readonly AuthDbContext authDbContext;
        private readonly SignInManager<IdentityUser> signInManager;

        public IdentityUserController(AuthDbContext authDbContext, SignInManager<IdentityUser> signInManager)
        {
            this.authDbContext = authDbContext;
            this.signInManager = signInManager;
        }

        // GET: /api/identityuser
        [HttpGet]
        public IEnumerable<IdentityUser> Get()
        {
            return authDbContext.IdentityUsers;
        }

        // GET: api/identityuser/{name}
        [HttpGet("{name}")]
        public IdentityUser Get(string name)
        {
            return signInManager.UserManager.FindByNameAsync(name).Result;
        }

        // GET: api/identityuser/ID/{id}
        [HttpGet("ID/{id}")]
        public ActionResult GetUserById(string id)
        {
            return Ok($"\"{signInManager.UserManager.FindByIdAsync(id).Result.UserName}\"");
        }

        // GET: api/identityuser/verify/{login}
        [HttpGet("verify/{username}/{password}")]
        public async Task<ActionResult> VerifyLogin(string username, string password)
        {
            var loginAttemptResult = await signInManager.PasswordSignInAsync(Get(username), password, false, false);
            if (loginAttemptResult.Succeeded) // If login succeeded
            {
                var result = Get(username);
                return Ok($"\"{result.Id}\""); // Return the ID
            }
            return NotFound();
        }

        // Post: api/identityuser
        [HttpPost]
        public async Task<ActionResult> Post(LoginModel login)
        { 
            if(login.Username != null)
            {
                if(login.Password != null)
                {
                    IdentityUser identityUser = new IdentityUser()
                    {
                        UserName = login.Username, //Create IdentityUser Object with this username
                    };

                    var createUserResult = await signInManager.UserManager
                        .CreateAsync(identityUser, login.Password); // Finalize creation with password

                    if(createUserResult.Succeeded) 
                    {
                        var result = await signInManager.UserManager
                            .FindByNameAsync(identityUser.UserName);
                        return Ok(result.Id);
                        // Return id if successfull login
                    }
                }
            }

            return NotFound();
            
        }

        [HttpPut("ID/{id}")]
        public async Task<IActionResult> UpdatePasswordById(string id, UpdateUserInfoModel updateUserInfo)
        {
            IdentityUser user = await signInManager.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.PasswordHash = signInManager.UserManager.PasswordHasher.HashPassword(null, updateUserInfo.NewPassword);
                await signInManager.UserManager.UpdateAsync(user);
                return Ok();
            }
            else
            {
                return NotFound();
            }
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
