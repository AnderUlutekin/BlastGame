using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    private int NumberOfRows, NumberOfColumns, NumberOfColors;
    public int A, B, C;
    private int NumberOfMoves;
    private Board board;
    public Text score;
    public Text movesText;
    private PopupWindowController PopupWindow;
    public bool isPopupActive = false;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Rows"))
            NumberOfRows = PlayerPrefs.GetInt("Rows");
        else
            NumberOfRows = 10;
        if (PlayerPrefs.HasKey("Columns"))
            NumberOfColumns = PlayerPrefs.GetInt("Columns");
        else
            NumberOfColumns = 10;
        if (PlayerPrefs.HasKey("Colors"))
            NumberOfColors = PlayerPrefs.GetInt("Colors");
        else
            NumberOfColors = 6;
        if (PlayerPrefs.HasKey("A"))
            A = PlayerPrefs.GetInt("A");
        else
            A = 4;
        if (PlayerPrefs.HasKey("B"))
            B = PlayerPrefs.GetInt("B");
        else
            B = 7;
        if (PlayerPrefs.HasKey("C"))
            C = PlayerPrefs.GetInt("C");
        else
            C = 9;
        if (PlayerPrefs.HasKey("Moves"))
            NumberOfMoves = PlayerPrefs.GetInt("Moves");
        else
            NumberOfMoves = 20;

        PopupWindow = FindObjectOfType<PopupWindowController>();
        ClosePopup();
        board = FindObjectOfType<Board>();
        board.SetDimensions(NumberOfRows, NumberOfColumns, NumberOfColors);
        board.currentBoard = new GameObject[board.GetColumnNum(), board.GetRowNum()];
        board.A = A;
        board.B = B;
        board.C = C;
        movesText.text = "Moves " + NumberOfMoves.ToString();
    }

    private void Start()
    {
        board.SetUp();
    }

    private void LateUpdate()
    {
        board.FillBoard();
    }

    public void MakeMove()
    {
        NumberOfMoves--;
        movesText.text = "Moves " + NumberOfMoves.ToString();
        if (NumberOfMoves == 0)
        {
            CheckHighScore();
            ReturnToMain();
        }
    }

    private void CheckHighScore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            int highscore = PlayerPrefs.GetInt("Highscore");
            if (int.Parse(score.text) > highscore)
            {
                PlayerPrefs.SetInt("Highscore", int.Parse(score.text));
                PlayerPrefs.SetInt("NewHighscore", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", int.Parse(score.text));
            PlayerPrefs.SetInt("NewHighscore", 1);
        }
    }

    public void OpenPopup()
    {
        isPopupActive = true;
        PopupWindow.OpenPopup();
    }

    public void ClosePopup()
    {
        isPopupActive = false;
        PopupWindow.ClosePopup();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ResetVisitedInfo()
    {
        for (int x = 0; x < board.GetColumnNum(); x++)
        {
            for (int y = 0; y < board.GetRowNum(); y++)
            {
                if (board.currentBoard[x, y] != null)
                {
                    board.currentBoard[x, y].GetComponent<Tile>().visited = false;
                }
            }
        }
    }
}
