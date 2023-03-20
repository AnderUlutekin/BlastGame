using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public int NumberOfRows, NumberOfColumns, NumberOfColors;
    public int A, B, C;
    public int NumberOfMoves;

    public GameObject HighscoreScene;
    public Text HighscoreText;
    public Text CurrentHighscoreText;

    private void Awake()
    {
        PlayerPrefs.SetInt("Columns", NumberOfColumns);
        PlayerPrefs.SetInt("Rows", NumberOfRows);
        PlayerPrefs.SetInt("Colors", NumberOfColors);
        PlayerPrefs.SetInt("A", A);
        PlayerPrefs.SetInt("B", B);
        PlayerPrefs.SetInt("C", C);
        PlayerPrefs.SetInt("Moves", NumberOfMoves);

        if (PlayerPrefs.HasKey("Highscore"))
        {
            CurrentHighscoreText.text = "Highscore = " + PlayerPrefs.GetInt("Highscore").ToString();
        }
        else
        {
            CurrentHighscoreText.text = "Highscore = 0";
        }

        if (!PlayerPrefs.HasKey("NewHighscore"))
        {
            PlayerPrefs.SetInt("NewHighscore", 0);
        }

        if (PlayerPrefs.GetInt("NewHighscore") == 1)
        {
            PlayerPrefs.SetInt("NewHighscore", 0);
            StartCoroutine(HighscorePanelControl());
        }
        else
        {
            HighscoreScene.SetActive(false);
        }
        //PlayerPrefs.DeleteAll();
    }

    public void EnterLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator HighscorePanelControl()
    {
        HighscoreText.text = "New Highscore " + PlayerPrefs.GetInt("Highscore").ToString();
        HighscoreScene.SetActive(true);
        yield return new WaitForSeconds(5);
        HighscoreScene.SetActive(false);
    }
}
