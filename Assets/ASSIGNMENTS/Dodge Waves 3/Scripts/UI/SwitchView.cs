using TMPro;
using UnityEngine;
public class SwitchView : MonoBehaviour
{
    public ManagerScript manager;
    public PlayerScript player;

    public void toggleView()
    {
        if (manager == null)
        {
            manager.gameObject.SetActive(!manager.gameObject.activeSelf);
        }
        if (player == null)
        {
            player.gameObject.SetActive(!player.gameObject.activeSelf);
        }
        
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
