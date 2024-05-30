using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText,xpText;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private TextMeshProUGUI xpForNextLevelText;
    [SerializeField] private NightMode nightMode;

    [SerializeField] private GameObject loadingPanel;
    public void OpenProfile()
    {
        int level = LevelManager.level;
        int xp = LevelManager.xp;
        int totxp = LevelManager.totalXP;

        profilePanel.SetActive(true);
        levelText.text = level.ToString();
        xpText.text = "Accumulated XP : " + totxp.ToString();
        xpForNextLevelText.text = "XP requirement : " + (LevelManager.xpPerLevel - xp).ToString();
    }
    public void NightModeToggle(bool value)
    {
        FeaturesManager.nightModeIsOn = value;
        nightMode.UpdateNightMode();
    }

    private void Start()
    {
        StartCoroutine(CloseLoadingScreen(6));
    }
    IEnumerator CloseLoadingScreen(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        loadingPanel.SetActive(false);
    }
}
