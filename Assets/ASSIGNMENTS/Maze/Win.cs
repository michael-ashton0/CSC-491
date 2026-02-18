using UnityEngine;

public class Goal : MonoBehaviour
{
    private WinUI winUI;
    private void Start()
    {
        winUI = FindFirstObjectByType<WinUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Time.timeScale = 0;

        winUI.ShowWin();
    }
}