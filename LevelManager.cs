using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; set; }

    public static int level;
    public static int xp;
    public static int totalXP;
    public static int xpPerLevel = 200;

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
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("level")) level = PlayerPrefs.GetInt("level");
        else level = 0;
        if (PlayerPrefs.HasKey("xp")) xp = PlayerPrefs.GetInt("xp");
        else xp = 0;
        if (PlayerPrefs.HasKey("totalxp")) totalXP = PlayerPrefs.GetInt("totalxp");
        else totalXP = 0;
    }
    public static void AddXP(int xp_value)
    {
        Logger.Log(xp_value + " xp added");
        xp += xp_value;
        totalXP += xp_value;
        UpdateSaveData();
        CheckForLevelIncrease();
    }
    static void CheckForLevelIncrease()
    {
        if(xp > xpPerLevel)
        {
            xp %= xpPerLevel;
            level++;
            UpdateSaveData();
            Logger.Log("level increased");
        }
    }
    static void UpdateSaveData()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("xp", xp);
        PlayerPrefs.SetInt("totalxp", totalXP);
        PlayerPrefs.Save();
    }
}
