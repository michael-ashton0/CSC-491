using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resolution : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_Dropdown dropdown;
    
    public void updateResolution()
    {
        text.text = "Current: " + dropdown.options[dropdown.value].text;
    }
}
