using scenes;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public void StartGame()
    {
        Scenes.OpenGame();
    }
        
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void MainMenu()
    {
        Scenes.OpenMenu();
    }
}