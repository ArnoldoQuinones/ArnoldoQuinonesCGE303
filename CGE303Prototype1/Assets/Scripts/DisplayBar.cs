using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Must include this to use the Slider
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{

    //Slider for the health bar
    //Set this in the inspector
    public Slider slider;

    //Gradient for the health bar
    public Gradient gradient;

    //Image for the fill of the health bar
    public Image fill;

    //Function to set the current value of the slider
    public void SetValue(float value)
    {
        //Set the value of the slider
        slider.value = value;

        //Set the color of the fill of the slider
        fill.color = gradient.Evaluate (slider.normalizedValue);
    }

    //Function to set the max value of the slider
    public void SetMaxValue (float value)
    {
        //Set the max value of the slider
        slider.maxValue = value;

        //Set the current value of the slider to the max value
        slider.value = value;

        //Set the color of the fill of the slider
        fill.color = gradient.Evaluate(1f);
    }
}