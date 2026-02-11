using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    
    public void updateValue()
    {
        text.text = ((int)slider.value).ToString();
        text.text = text.text + "%";
    }

}
