using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMode : MonoBehaviour
{
    [SerializeField] GameObject nightLight;
    void Start()
    {
        if (FeaturesManager.nightModeIsOn)
        {
            Logger.Log("Night filter applying");
            nightLight.SetActive(true);
        }
    }
    public void UpdateNightMode()
    {
        if (FeaturesManager.nightModeIsOn)
        {
            Logger.Log("Night filter applying");
            nightLight.SetActive(true);
        }
        else
        {
            nightLight.SetActive(false);
            Logger.Log("night filter removed");
        }
    }
   
}
