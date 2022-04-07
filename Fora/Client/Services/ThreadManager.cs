namespace Fora.Client.Services
{
    public class ThreadManager : IThreadManager
    {
        private readonly HttpClient _httpClient;
        public ThreadManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ThreadModel>> GetThreads()
        {
            return await _httpClient.GetFromJsonAsync<List<ThreadModel>>("/api/thread");
        }

        public async Task<ThreadModel> PostThread(ThreadModel thread)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/thread/", thread);
            return thread;
        }
    }
}
