using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsSystem : MonoBehaviour
{
    public float fuelLevel = 100;
    public float oilLevel = 100;

    void Start()
    {

    }

    void Update ()
    {
        EngineCheck();

        if (fuelLevel <= 0 && oilLevel <= 0)
        {
            
        }
	}

    public void EngineCheck()
    {
        if (fuelLevel > 0)
        {
            fuelLevel -= 1 * Time.deltaTime;
        }
        else
        {
            fuelLevel = 0;
        }
        if (oilLevel > 0)
        {
            oilLevel -= 2 * Time.deltaTime;
        }
        else
        {
            oilLevel = 0;
        }

    }
}
