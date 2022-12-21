using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class TouchScript : MonoBehaviour, IMixedRealityTouchHandler
{
    private Color originalColor;
    void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
       // throw new System.NotImplementedException();
    }
}
