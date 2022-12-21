using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class Reorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReorderCapsules()
    {
        GridObjectCollection grid = GetComponent<GridObjectCollection>();
        grid.Columns = 2;
        grid.Distance = 0.2f;
        grid.UpdateCollection();
    }
}
