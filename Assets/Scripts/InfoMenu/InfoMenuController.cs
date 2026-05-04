using eventSystem;
using TMPro;
using UnityEngine;

namespace InfoMenu
{
    public class InfoMenuController : MonoBehaviour
    {
        private TextMeshProUGUI _scoreGo;
        private const string ScoreText = "";
        
        private void Awake()
        {
            _scoreGo = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        private void OnEnable()
        {
            GameEvents.OnHealthInfo += UpdateHealth;
            GameEvents.OnOpenedUI += OpenedUI;
        }

        private void OnDisable()
        {
            GameEvents.OnHealthInfo -= UpdateHealth;
        }
        
        private void UpdateHealth(float value)
        {
            _scoreGo.text = ScoreText + value;
        }

        private void OpenedUI(bool isOpenUI)
        {
            gameObject.SetActive(!isOpenUI);
        }
    }
}