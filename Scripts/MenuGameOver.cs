using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuGameOver : MonoBehaviour
{
    public void Restart(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    
    public void Exit()
    {
        #if UNITY_EDITOR
            
            EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
          
            Application.OpenURL("about:blank");
        #else
            
            Application.Quit();
        #endif
    }
}
