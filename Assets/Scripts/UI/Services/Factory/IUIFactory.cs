using Infrastructure.Services;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreatUIRoot();
        void CreateUIConsole();
    }
}