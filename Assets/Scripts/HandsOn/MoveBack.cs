using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBack : MonoBehaviour
{
    private bool moveBack;
    private bool started;

    private float zposition;
    // Start is called before the first frame update
    void Start()
    {
        moveBack = false;
        started = false;
        zposition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (!moveBack)
            {
                zposition += 0.1f;
            }
            else
            {
                zposition -= 0.1f;
            }

            if (zposition > 5)
            {
                moveBack = true;
            }
            if (zposition <= 1)
            {
                moveBack = false;
                started = false;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, zposition);
        }
       
    }

    public void startMovingBacK()
    {
        started = true;
    }
}
