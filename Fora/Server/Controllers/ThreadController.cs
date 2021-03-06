using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<ThreadModel>> Get()
        {
            return await GetFullyIncludedThreads();
        }

        [HttpGet("{id}")]
        public async Task<ThreadModel> Get(int id)
        {
            var result = await GetFullyIncludedThreads();
            return result.SingleOrDefault(t => t.Id == id);
        }

        [HttpGet("interest/{id}")]
        public async Task<IEnumerable<ThreadModel>> GetThreadsbyInterest(int id)
        {
            var result = await GetFullyIncludedThreads();
            return result.Where(t => t.InterestId == id);
        }

        public async Task<IEnumerable<ThreadModel>> GetFullyIncludedThreads()
        {
            List<ThreadModel> dbThreads = await appDbContext.Threads
                .Include(t => t.Messages)
                .Include(t => t.Interest)
                .Include(t => t.User).ToListAsync();

            foreach (ThreadModel thread in dbThreads)
            {
                foreach (MessageModel message in appDbContext.Messages)
                {
                    if (message.ThreadId == thread.Id)
                    {
                        if(thread.Messages.Contains(message))
                        {
                            continue;
                        }
                        thread.Messages.Add(message);
                    }
                }

                foreach (InterestModel interest in appDbContext.Interests)
                {
                    if (thread.InterestId == interest.Id)
                    {
                        thread.Interest = interest;
                    }
                }

                foreach (UserModel user in appDbContext.Users)
                {
                    if (thread.UserId == user.Id)
                    {
                        thread.User = user;
                    }
                }

            }

            return dbThreads;
        }

        [HttpPost]
        public async Task<ActionResult<ThreadModel>> Post(PostThreadModel postThread)
        {
            if (postThread != null)
            {
                ThreadModel thread = new ThreadModel()
                {
                    UserId = postThread.CreatorId,
                    Name = postThread.Title,
                    Messages = new List<MessageModel>()
                    {
                        new MessageModel()
                        {
                            Message = postThread.Description,
                            UserId = postThread.CreatorId,
                            PostDate = DateTime.Now,
                        }
                    },
                    InterestId = postThread.InterestId

                };
                await appDbContext.Threads.AddAsync(thread);
                await appDbContext.SaveChangesAsync();
                return Ok(thread);
            }
            return BadRequest();

        }

        [HttpPut]
        public async Task<ActionResult<MessageModel>> PutMessage(PostMessageModel postMessage)
        {
            if(postMessage != null)
            {
                var thread = await Get(postMessage.ThreadId);
                thread.Messages.Add(new MessageModel()
                {
                    Message = postMessage.Message,
                    UserId = postMessage.PosterId,
                    PostDate = DateTime.Now,
                });

                await appDbContext.SaveChangesAsync();
                return Ok(thread.Messages.LastOrDefault()); 
            }
            return BadRequest();
        }

        [HttpPut("message")]
        public async Task<ActionResult<MessageModel>> UpdateMessage(UpdateMessageModel updateMessage)
        {
            if(updateMessage != null)
            {
                var thread = await Get(updateMessage.ThreadId);
                var message = thread.Messages.SingleOrDefault(m => m.Id == updateMessage.MessageId);
                message.Message = updateMessage.NewMessage;
                message.Deleted = updateMessage.RemoveMessage;
                if(message.Deleted == false)
                {
                    message.Edited = true;
                    message.EditDate = DateTime.Now;
                }
                await appDbContext.SaveChangesAsync();
                return Ok(thread.Messages.LastOrDefault());
            }
            return BadRequest();
        }

        [HttpPut("thread")]
        public async Task<ActionResult<ThreadModel>> UpdateThread(UpdateThreadModel updateThread)
        {
            if(updateThread != null)
            {
                var thread = await Get(updateThread.ThreadId);
                if(updateThread.NewName != null)
                {
                    thread.Name = updateThread.NewName;
                }
                if(updateThread.RemoveThread == true)
                {
                    thread.Messages.First().Message = "This post has been removed";
                    thread.Messages.First().Deleted = true;
                }
                if(updateThread.NewBody != null)
                {
                    thread.Messages.First().Message = updateThread.NewBody;
                }
                await appDbContext.SaveChangesAsync();
                return Ok(thread);
            }
            return BadRequest();
        }

    }
}
