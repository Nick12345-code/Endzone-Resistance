using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // quits game when built
        Application.Quit();
        // quits play mode in editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
