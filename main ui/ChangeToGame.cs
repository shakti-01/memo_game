using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToGame : MonoBehaviour
{
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Insert Code Here (I.E. Load Scene, Etc)
                Application.Quit();

                return;
            }
        }
    }
    /// <summary> Start game scene by taking build index of scene </summary>
    public void StartGame(int gameID)
    {
        StartCoroutine(LoadYourAsyncScene(gameID));
    }

    IEnumerator LoadYourAsyncScene(int gameID)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //gameID should match build index of game scene !!
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameID);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
