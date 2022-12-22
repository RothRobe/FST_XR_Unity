using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using CityAR;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class ChangeAccordingToSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro text;
    public GameObject platform;
    private VisualizationCreator _visualizationCreator;
    void Start()
    {
        _visualizationCreator = platform.GetComponent<VisualizationCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText()
    {
        float value = GetComponent<StepSlider>().SliderValue + 0.5f;
        text.text = value.ToString(CultureInfo.CurrentCulture);
    }

    public void Rescale()
    {
        _visualizationCreator.Rescale(GetComponent<StepSlider>().SliderValue + 0.5f);
    }
}
