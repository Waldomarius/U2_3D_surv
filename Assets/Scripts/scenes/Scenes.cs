using UnityEngine;
using UnityEngine.SceneManagement;

namespace scenes
{
    public class Scenes : MonoBehaviour
    {
        public static void OpenMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public static void OpenGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        public static void Dead()
        {
            SceneManager.LoadScene("Dead");
        }
    }
}