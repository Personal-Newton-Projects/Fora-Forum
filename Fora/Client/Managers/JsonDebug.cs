namespace Fora.Client.Managers
{
    public class JsonDebug
    {
        public JsonDebug(HttpResponseMessage message)
        {
            Console.WriteLine(message.Content.ReadAsStringAsync().Result);
        }
    }
}
