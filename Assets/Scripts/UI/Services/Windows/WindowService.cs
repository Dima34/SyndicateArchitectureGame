using UI.Services.Factory;

namespace UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.None:
                    break;
                case WindowID.Shop:
                    _uiFactory.CreateShop();
                    break;
            }
        }
    }
}