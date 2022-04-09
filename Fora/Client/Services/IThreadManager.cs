namespace Fora.Client.Services
{
    public interface IThreadManager
    {
        Task<List<ThreadModel>> GetThreads();
        Task<ThreadModel> PostThread(PostThreadModel thread);
        Task<MessageModel> PutMessage(PostMessageModel message);
        Task<ThreadModel> GetThread(int id);
        Task<List<ThreadModel>> GetThreadsByInterest(int id);
    }
}
