namespace PensionManager.PensionManager.Application.BackgroundJob
{
    public interface IPensionJob
    {
        Task CalculateInterest();
    }
}
