using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderStepsDisplay : MonoBehaviour
{
    [SerializeField] private Image[] dots = null;
    private Slider slider;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
        int slidersPossibilities = Mathf.RoundToInt(slider.maxValue - slider.minValue);

        if (slidersPossibilities != dots.Length - 1)
        {
            Debug.LogError("sliders possibilities need to be exactly equal to dots array size");
        }
        UpdateStepsDisplay();
    }

    public void UpdateStepsDisplay()
    {
        int current = Mathf.RoundToInt(slider.value);
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].enabled = i >= current;
        }
    }

}
