namespace Infrastructure.Services.Random
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}