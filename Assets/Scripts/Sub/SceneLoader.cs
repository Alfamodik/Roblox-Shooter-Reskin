using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenu,
    GamePlay,
}

public static class SceneLoader
{
    public static void LoadScene(SceneName sceneName)
    {
        DisposeHandler.DisposeAll();
        SceneManager.LoadScene((int)sceneName);
    }
}
