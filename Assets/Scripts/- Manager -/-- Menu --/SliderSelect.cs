using UnityEngine;
using UnityEngine.UI;

public class SliderSelect : MonoBehaviour
{
    public Slider sliderSelected; // each button will be assigned a slider

  public void OnSelectClick()
    {
        sliderSelected.Select();
    }
}
