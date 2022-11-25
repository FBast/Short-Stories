using Script.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void LoadGame() {
        SceneManager.LoadScene(Properties.Scene.Game);
        SceneManager.UnloadSceneAsync(Properties.Scene.Menu);
    }

    public void LoadEditor() {
        SceneManager.LoadScene(Properties.Scene.Editor);
        SceneManager.UnloadSceneAsync(Properties.Scene.Menu);
    }

    public void Exit() {
        Application.Quit();
    }
    
}
