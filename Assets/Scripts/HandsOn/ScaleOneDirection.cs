using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOneDirection : MonoBehaviour
{
    private bool moveBack;
    private bool started;

    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        moveBack = false;
        started = false;
        scale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (!moveBack)
            {
                scale += 0.001f;
            }
            else
            {
                scale -= 0.001f;
            }

            if (scale > 0.5)
            {
                moveBack = true;
            }
            if (scale <= 0.2)
            {
                moveBack = false;
                started = false;
            }
            transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
        }
       
    }

    public void startScaling()
    {
        started = true;
    }
}
