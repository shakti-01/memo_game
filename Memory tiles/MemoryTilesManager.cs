using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MemoryTilesManager : MonoBehaviour
{
    private System.Random random = new System.Random();
    ArrayList list = new ArrayList();
    [SerializeField] private int numOfTiles;
    [SerializeField] private Image[] tileImages;
    //[SerializeField] private Images tileImages;
    [SerializeField] private Image[] tiles;
    [SerializeField] private Image[] covers;

    [SerializeField] private int currentImg;
    private int prevTileIndex, currentTileIndex;
    [SerializeField] private float closeBothTilesDelay;

    [SerializeField] private TextMeshProUGUI attempsText;
    private int attempts;

    private bool CRisRunning;

    [SerializeField] private TimeCounter timeCounter;

    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject gameoverPanel;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Exit();return;
            }
        }
    }
    public void Exit()
    {
        StartCoroutine(LoadYourAsyncScene(0));
    }
    IEnumerator LoadYourAsyncScene(int gameID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameID);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void StartGame()
    {
        AddIndices(numOfTiles - 1);

        Vector3 zero = new Vector3(0, 0, 0);
        
        foreach (Image img in tileImages)
        {
            int i = random.Next(list.Count);
            int index = (int)list[i];
            Logger.Log("random index: " + index);

            img.transform.SetParent(tiles[index].transform);
            img.transform.GetComponent<RectTransform>().anchoredPosition = zero;
            
            UpdateList(index);

        }
        for(int j = 0; j < numOfTiles; j++)
        {
            covers[j].transform.SetParent(tiles[j].transform);
            covers[j].transform.GetComponent<RectTransform>().anchoredPosition = zero;
        }
        currentImg = -1;
        attempts = 0;
        timeCounter.StartTimer();
    }

    void UpdateList(int itemToRemove)
    {
        list.Remove(itemToRemove);
    }

    void AddIndices(int n)
    {
        for (int i = 0; i <= n; i++) list.Add(i);
    }

    public void ChoosenTile(int i)
    {
        if (CRisRunning) return;
        attempts++;
        attempsText.text = "Attempts : " + attempts.ToString();
        Logger.Log(currentImg);
        if(currentImg == -1)
        {
            Logger.Log("Current sprite is null");
            covers[i].gameObject.SetActive(false);
            currentImg = covers[i].gameObject.GetComponentInParent<RectTransform>().gameObject.GetComponentInChildren<ImageValue>().value;
            currentTileIndex = i;
        }
        else
        {
            Logger.Log("current sprite is not null");
            covers[i].gameObject.SetActive(false);
            if (currentImg == covers[i].gameObject.GetComponentInParent<RectTransform>().gameObject.GetComponentInChildren<ImageValue>().value)
            {
                Logger.Log("equal");
            }
            else
            {
                Logger.Log("not equal");
                prevTileIndex = currentTileIndex;
                currentTileIndex = i;
                StartCoroutine(CloseBothTiles(closeBothTilesDelay));
            }
            currentImg = -1;
        }
        if (CheckWin()) { GameOver(); }
    }
    bool CheckWin()
    {
        byte flag = 0;
        foreach (Image image in covers)
        {
            if (image.gameObject.activeSelf)
            {
                flag = 1;break;
            }
        }
        if (flag == 0) return true;
        else return false;
    }
    void GameOver()
    {
        Logger.Log("Game ends!");
        timeCounter.StopTimer();
        winnerText.text = "Completed in " + timeCounter.GetTimeValue()+"\n\n"+attempts.ToString()+" attemps taken";
        gameoverPanel.SetActive(true);
        //panel active with play again btn n show score
        //add req xp here
        int base_xp = 60;
        base_xp -= attempts / 2;
        if (base_xp < 0) base_xp = 0;
        LevelManager.AddXP(base_xp);
    }
    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    IEnumerator CloseBothTiles(float delay)
    {
        CRisRunning = true;
        yield return new WaitForSecondsRealtime(delay);
        covers[prevTileIndex].gameObject.SetActive(true);
        covers[currentTileIndex].gameObject.SetActive(true);
        CRisRunning = false;
    }
    
}