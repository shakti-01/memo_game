using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighLowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctText;
    [SerializeField] private TextMeshProUGUI incorrectText;
    private int correctAmt, incorrectAmt;
    [SerializeField] private Timer timer;
    private bool gameIsOn;

    [SerializeField] private GameObject numTile;
    [SerializeField] private TextMeshProUGUI tileNum;

    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject gameoverPanel;

    //difficulty
    public int numUpper,numLower;

    private int currentNum, prevNum;
    private bool firstTile;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered


    void Start()
    {
        dragDistance = Screen.height * 8 / 100; //dragDistance is 15% height of the screen
        prevNum = 0;
        correctText.text = "Correct : 0";
        incorrectText.text = "Incorrect : 0";
    }

    void Update()
    {
        if(gameIsOn && timer.timeRemaining == 0)
        {
            GameOver();return;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(LoadYourAsyncScene(0));

            return;
        }
        if (!gameIsOn) return;
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        return;
                        //if ((lp.x > fp.x))  //If the movement was to the right)
                        //{   //Right swipe
                        //    Debug.Log("Right Swipe");
                        //}
                        //else
                        //{   //Left swipe
                        //    Debug.Log("Left Swipe");
                        //}
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            if(currentNum > prevNum) { CorrectChoice(); }
                            else { IncorrectChoice(); }
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            if (currentNum < prevNum) { CorrectChoice(); }
                            else { IncorrectChoice(); }
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    public void GameStart()
    {
        
        firstTile = true;
        incorrectText.text = "Incorrect : 0";
        correctText.text = "Correct : 0";
        correctAmt = incorrectAmt = 0;
        //1st tile will come
        ShowNextTile();
        StartCoroutine(DelayAction(1.2f));

        //next tile
        //check for input
        //correct input? then ++ next tile
        //else incor ++ 
        //timer end game end

    }
    public void GameOver()
    {
        gameIsOn = false;
        winnerText.text = correctAmt.ToString() + " correct\nand\n" + incorrectAmt.ToString() + " incorrect attempts";
        gameoverPanel.SetActive(true);
        //add req xp
        LevelManager.AddXP(20 + (correctAmt / 2));
    }
    void ShowNextTile()
    {
        numTile.SetActive(false);
        int num = Random.Range(20, 100);
        tileNum.text = num.ToString();
        if (firstTile) { prevNum = num; firstTile = false; }
        else { prevNum = currentNum; }
        currentNum = num;
        numTile.SetActive(true);
    }
    void CorrectChoice()
    {
        correctAmt++;
        correctText.text = "Correct : " + correctAmt.ToString();
        ShowNextTile();
    }
    void IncorrectChoice()
    {
        incorrectAmt++;
        incorrectText.text = "Incorrect : " + incorrectAmt.ToString();
    }

    public void PlayAgain()
    {
        gameoverPanel.SetActive(false);
        GameStart();
    }

    IEnumerator DelayAction(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        timer.startTimer();
        gameIsOn = true;
        ShowNextTile();
    }
    IEnumerator LoadYourAsyncScene(int gameID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameID);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
