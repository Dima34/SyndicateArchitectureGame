using System;

namespace Infrastructure.Services.RandomService
{
    class RandomService : IRandomService
    {
        private readonly Random _random;

        public int Next(int minValue, int maxValue) =>
            _random.Next(minValue, maxValue);

        public RandomService() =>
            _random = new Random();
    }
}