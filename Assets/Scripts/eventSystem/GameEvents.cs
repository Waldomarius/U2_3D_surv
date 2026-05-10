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
        
        public static event Action<float> OnUpdateActiveMenuSlot;
        
        public static void UpdateActiveMenuSlot(float activeMenuSlot) => OnUpdateActiveMenuSlot?.Invoke(activeMenuSlot);
        
        public static event Action<float> OnDisableActiveMenuSlot;
        
        public static void UpdateDissableActiveMenuSlot(float activeMenuSlot) => OnDisableActiveMenuSlot?.Invoke(activeMenuSlot);

        public static event Action<bool> OnAxeActive;
        
        public static void UpdateAxeActive(bool axeActive) => OnAxeActive?.Invoke(axeActive);

        public static event Action<float> OnHealthInfo;
        
        public static void UpdateHealthInfo(float health) => OnHealthInfo?.Invoke(health);
    }
}