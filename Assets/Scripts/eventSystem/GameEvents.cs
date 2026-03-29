using System;
using UnityEngine;

namespace eventSystem
{
    public class GameEvents
    {
        public static event Action<bool> OnCloseUI;
        
        public static void CloseUI(bool closeUI) => OnCloseUI?.Invoke(closeUI);
        
        public static event Action<bool> OnOpenedUI;
        
        public static void OpenedUI(bool openedUI) => OnOpenedUI?.Invoke(openedUI);
        
        public static event Action<Transform> OnPlayerPosition;
        
        public static void PlayerPosition(Transform playerPosition) => OnPlayerPosition?.Invoke(playerPosition);
    }
}