namespace Infrastructure.Services.RandomService
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}