using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject winPanel;

    public void ShowWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideWin()
    {
        winPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}