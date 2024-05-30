using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesManager : MonoBehaviour
{
    public static FeaturesManager instance { get; set; }

    public static bool nightModeIsOn;

    [SerializeField] private Toggle nightModeToggle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        int hour = int.Parse(System.DateTime.Now.Hour.ToString());
        if (hour > 17 || hour < 5)
        {
            nightModeIsOn = true;
            nightModeToggle.isOn = true;
        }
        else
        {
            nightModeIsOn = false;
            nightModeToggle.isOn = false;
        }
    }
}
