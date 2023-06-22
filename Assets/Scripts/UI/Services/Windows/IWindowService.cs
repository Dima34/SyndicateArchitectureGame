using Infrastructure.Services;

namespace UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowID windowID);
    }
}