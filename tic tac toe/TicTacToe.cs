using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    public char[] game_array;
    //[SerializeField] private string player1;
    //[SerializeField] private string player2;
    [SerializeField] private char nextSymbol = 'X';
    [SerializeField] private int currentPlayer = 1;//1or 2

    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;

    [SerializeField] private GameObject namesPanel;
    [SerializeField] private TextMeshProUGUI p1_display;
    [SerializeField] private TextMeshProUGUI p2_display;

    [SerializeField] private TextMeshProUGUI winnertext;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Image[] tileSymbols;
    [SerializeField] private Sprite cross;
    [SerializeField] private Sprite circle;

    //private TouchScreenKeyboard keyboard;

    private void Start()
    {
        Logger.Log("start");
        game_array = new char[14];
        namesPanel.SetActive(true);
    }
    /*public void showKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }*/
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Exit();

                return;
            }
        }
    }
    public void StartGame()
    {
        namesPanel.SetActive(false);
        p1_display.text = player1.text;
        p2_display.text = player2.text;
        currentPlayer = 1;
        nextSymbol = 'X';
    }
    public void PlayAgain()
    {
        gameOverPanel.SetActive(false);
        namesPanel.SetActive(true);
        player1.text = "";
        player2.text = "";
        int len = game_array.Length;
        for(int i=0; i < len; i++)
        {
            game_array[i] = '\0';
        }
        //-------------
        for(int i = 0; i < 9; i++)
        {
            tileSymbols[i].GetComponent<Image>().overrideSprite = null;
        }
    }
    public void Exit()
    {
        StartCoroutine(LoadYourAsyncScene(0));
    }
    void GameOver(int wins,int player)
    {
        StartCoroutine(DelayAction(1));
        if(wins == -1)
        {
            winnertext.text = "Match draw !";
        }
        else if(wins == 1)
        {
            if(player == 1)
            {
                winnertext.text = p1_display.text + " won the match !!";
            }
            else
            {
                winnertext.text = p2_display.text + " won the match !!";
            }
        }
        //add reqired xp
        LevelManager.AddXP(60);
    }
    void ShowSymbol(int squareIndex, char nextSymbol)
    {
        tileSymbols[squareIndex].GetComponent<Image>().overrideSprite = (nextSymbol == 'X') ? cross : circle;
    }
    public void choiseMade(int squareIndex)
    {
        if (game_array[squareIndex] != '\0') return;
        if(squareIndex == 0)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if(squareIndex == 1)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 2)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 3)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 4)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 5)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 6)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 7)
        {
            game_array[squareIndex] = nextSymbol;
        }
        else if (squareIndex == 8)
        {
            game_array[squareIndex] = nextSymbol;
        }
        ShowSymbol(squareIndex, nextSymbol);
        //foreach (char item in game_array)
        //{
        //    Logger.Log(item);
        //}
        int wins = checkWin();

        if(wins == 1)
        {
            if(currentPlayer == 1)
            {
                Logger.Log("Player 1 wins" +player1.text.ToString());
            }
            else
            {
                Logger.Log("Player 2 wins" + player2.text.ToString());
            }
            GameOver(wins, currentPlayer);
            return;
        }
        else if(wins == -1)
        {
            Logger.Log("Match draw");
            GameOver(wins, currentPlayer);
            return;
        }
        nextSymbol = (nextSymbol == 'X') ? 'O' : 'X';//update next symbol
        currentPlayer = (currentPlayer == 1) ? 2 : 1;//update current player
    }

    int checkWin()
    {
        if (game_array[0] == game_array[1] && game_array[1] == game_array[2] && game_array[0] != '\0')
        {
            return 1;
        }
        //Winning Condition For Second Row
        else if (game_array[3] == game_array[4] && game_array[4] == game_array[5] && game_array[3] != '\0')
        {
            return 1;
        }
        //Winning Condition For Third Row
        else if (game_array[6] == game_array[7] && game_array[7] == game_array[8] && game_array[6] != '\0')
        {
            return 1;
        }

        //Winning Condition For First Column
        else if (game_array[0] == game_array[3] && game_array[3] == game_array[6] && game_array[0] != '\0')
        {
            return 1;
        }
        //Winning Condition For Second Column
        else if (game_array[1] == game_array[4] && game_array[4] == game_array[7] && game_array[1] != '\0')
        {
            return 1;
        }
        //Winning Condition For Third Column
        else if (game_array[2] == game_array[5] && game_array[5] == game_array[8] && game_array[2] != '\0')
        {
            return 1;
        }
        //diagonal
        else if (game_array[0] == game_array[4] && game_array[4] == game_array[8] && game_array[0] != '\0')
        {
            return 1;
        }
        else if (game_array[2] == game_array[4] && game_array[4] == game_array[6] && game_array[2] != '\0')
        {
            return 1;
        }
        //draw check
        else if ((game_array[0] == 'X' || game_array[0] == 'O')
              && game_array[1] != '\0' && game_array[2] != '\0' && game_array[3] != '\0' && game_array[4] != '\0' && game_array[5] != '\0' 
              && game_array[6] != '\0' && game_array[7] != '\0' && game_array[8] != '\0')
        {
            return -1;
        }
        else
        {
            return 0;//game is going on still
        }
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

    IEnumerator DelayAction(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        gameOverPanel.SetActive(true);
    }
}
