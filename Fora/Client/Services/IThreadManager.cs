namespace Fora.Client.Services
{
    public interface IThreadManager
    {
        Task<List<ThreadModel>> GetThreads();
        Task<ThreadModel> PostThread(ThreadModel thread);
    }
}
