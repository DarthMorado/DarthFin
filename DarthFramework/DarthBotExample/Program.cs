namespace DarthBotExample
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            var host = DarthFramework.Setup(typeof(MyBot));
            await host.Start();
        }
    }
}
