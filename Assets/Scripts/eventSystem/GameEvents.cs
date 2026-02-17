using System;

namespace eventSystem
{
    public class GameEvents
    {
        public static event Action<bool> OnCloseUI;
        
        public static void CloseUI(bool closeUI) => OnCloseUI?.Invoke(closeUI);
        
    }
}