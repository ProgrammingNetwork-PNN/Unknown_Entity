using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    bool zero_start = false;
    Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        Light_Down();
    }

    void Update()
    {

    }

    void Light_Up()
    {
        zero_start = true;
        InvokeRepeating("IncreaseIntensity", 0.0f, 0.1f);
    }

    void Light_Down()
    {
        zero_start = true;
        InvokeRepeating("DecreaseIntensity", 0.0f, 0.1f);
    }

    void IncreaseIntensity()
    {
        lightComponent.intensity += 0.1f;
        if (lightComponent.intensity >= 1.0f)
        {
            CancelInvoke("IncreaseIntensity");
            Light_Down();
        }
    }

    void DecreaseIntensity()
    {
        lightComponent.intensity -= 0.1f;
        if (lightComponent.intensity <= 0.0f)
        {
            CancelInvoke("DecreaseIntensity");
            Light_Up();
        }
    }
}