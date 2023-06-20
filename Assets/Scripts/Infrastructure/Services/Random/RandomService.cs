namespace Infrastructure.Services.Random
{
    class RandomService : IRandomService
    {
        private readonly System.Random _random;

        public int Next(int minValue, int maxValue) =>
            _random.Next(minValue, maxValue);

        public RandomService() =>
            _random = new System.Random();
    }
}