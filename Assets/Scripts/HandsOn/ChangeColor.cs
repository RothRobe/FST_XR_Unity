using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRed()
    {
        float value = GetComponent<PinchSlider>().SliderValue;
        Color color = cube.GetComponent<Renderer>().material.color;
        color = new Color(value, color.g, color.b);
        cube.GetComponent<Renderer>().material.color = color;
    }
    
    public void SetGreen()
    {
        float value = GetComponent<PinchSlider>().SliderValue;
        Color color = cube.GetComponent<Renderer>().material.color;
        color = new Color(color.r, value, color.b);
        cube.GetComponent<Renderer>().material.color = color;
    }
    
    public void SetBlue()
    {
        float value = GetComponent<PinchSlider>().SliderValue;
        Color color = cube.GetComponent<Renderer>().material.color;
        color = new Color(color.r, color.g, value);
        cube.GetComponent<Renderer>().material.color = color;
    }
}
