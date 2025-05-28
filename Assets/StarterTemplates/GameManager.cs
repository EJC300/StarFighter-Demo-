using UnityEngine;
public class GameManager : MonoBehaviour
{
    //This is a small scale game manager that handles exit and starting the game
    [SerializeField] private string firstLevelSceneName = "FirstLevel";
    [SerializeField] private string mainMenuName = "MainMenu";
    //Can be updated to include a loading screen
    public string FirstLevelSceneName
    {
        get { return firstLevelSceneName; }
        set { firstLevelSceneName = value; }
    }
    public string MainMenuName
    {
        get { return mainMenuName; }
        set { mainMenuName = value; }
    }
    public void ExitGame()
    {
        // This method is called when the player wants to exit the game.
        // It quits the application.
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    public void ExitToMenu()
    {
        // This method is called when the player wants to exit to the main menu.
        // It loads the main menu scene.
        Debug.Log("Exiting to menu...");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(mainMenuName);
    }

    public void LoadFirstLevel()
    {
        // This method is called when the player wants to start the game.
        // It loads the first level scene.
        Debug.Log("Loading first level...");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(firstLevelSceneName);
    }
    private void Awake()
    {
        Cursor.visible = false;
       
        FirstLevelSceneName = firstLevelSceneName;
    }
}
