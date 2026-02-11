using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public Toggle toggle;

    Text label;

    void Awake()
    {
        label = toggle.GetComponentInChildren<Text>();
    }

    public void ToggleFullscreen(bool value)
    {
        if (label.text == "OFF")
        {
            label.text = "ON";
        }
        else
        {
            label.text = "OFF";
        };
    }
}


