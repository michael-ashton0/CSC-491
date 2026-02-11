using UnityEngine;

public class QuitScreen : MonoBehaviour
{
    public void ConfirmQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    
    public void CancelQuit()
    {
        gameObject.SetActive(false);
    }
    
}