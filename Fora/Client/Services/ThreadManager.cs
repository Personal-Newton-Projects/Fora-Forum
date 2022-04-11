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
            return await _httpClient.GetFromJsonAsync<List<ThreadModel>>("/api/thread/");
        }

        public async Task<ThreadModel> GetThread(int id)
        {
            return await _httpClient.GetFromJsonAsync<ThreadModel>($"/api/thread/{id}");
        }

        public async Task<List<ThreadModel>> GetThreadsByInterest(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<ThreadModel>>($"/api/thread/interest/{id}");
        }

        public async Task<ThreadModel> PostThread(PostThreadModel thread)
        {
            //new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles }
            // hackfix

            var result = await _httpClient.PostAsJsonAsync("/api/thread/", thread);
            new JsonDebug(result);
            return await result.Content.ReadFromJsonAsync<ThreadModel>();
        }

        public async Task<MessageModel> PutMessage(PostMessageModel message)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/thread", message);
            new JsonDebug(result);
            return await result.Content.ReadFromJsonAsync<MessageModel>();
        }

        public async Task<MessageModel> UpdateMessage(UpdateMessageModel updateMessage)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/thread/message", updateMessage);
            return await result.Content.ReadFromJsonAsync<MessageModel>();
        }

        public async Task<ThreadModel> UpdateThread(UpdateThreadModel updateThread)
        {
            var result = await _httpClient.PutAsJsonAsync("api/thread/thread", updateThread);
            return await result.Content.ReadFromJsonAsync<ThreadModel>();
        }
    }
}
