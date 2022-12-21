using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Color originalColor;
    void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLooking()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    public void StopLooking()
    {
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }
}
