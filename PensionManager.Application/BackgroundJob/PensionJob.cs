
namespace PensionManager.PensionManager.Application.BackgroundJob
{
    public class PensionJob : IPensionJob
    {
        public Task CalculateInterest()
        {
            Console.WriteLine("Glory to Jesus!!");
            // Implement logic for interest calculation. This was not done because of no calculation formular and requirement was provided
            return Task.CompletedTask;
        }
    }
}
