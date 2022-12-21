using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private float xrotation;
    private float rotation;

    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (xrotation + 90 > rotation)
            {
                rotation += 1;
                transform.rotation = Quaternion.Euler(rotation,0,0);
            }
            else
            {
                started = false;
            }
        }
    }

    public void StartRotation()
    {
        xrotation = transform.rotation.x;
        rotation = xrotation;
        Debug.Log(xrotation);
        started = true;
    }
}
