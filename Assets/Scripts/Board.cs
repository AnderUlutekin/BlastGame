using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    private int noOfRows;
    private int noOfColumns;
    private int noOfColors;

    public int A, B, C;

    [SerializeField]
    private GameObject[] tiles;

    public GameObject[,] currentBoard;

    public void SetDimensions(int rowNum, int columnNum, int colorNum)
    {
        noOfRows = rowNum;
        noOfColumns = columnNum;
        noOfColors = colorNum;
    }

    public int GetRowNum()
    {
        return noOfRows;
    }

    public int GetColumnNum()
    {
        return noOfColumns;
    }

    public void SetUp()
    {
        int rangeX = noOfColumns / 2;
        int rangeY = noOfRows / 2;

        if (noOfColumns % 2 == 0 && noOfRows % 2 == 0)
        {
            for (int m = rangeY * (-1); m < rangeY; m++)
            {
                float newM = m + 0.5f;
                for (int n = rangeX * (-1); n < rangeX; n++)
                {
                    float newN = n + 0.5f;
                    Vector2 pos = new Vector2(newN, newM);
                    int colorToUse = Random.Range(0, noOfColors);
                    GameObject tile = Instantiate(tiles[colorToUse], pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.name = "(" + newN + ", " + newM + ")";
                    tile.GetComponent<Renderer>().sortingOrder = m + rangeY;
                    currentBoard[n + (noOfColumns / 2), m + (noOfRows / 2)] = tile;
                }
            }
        }
        else if (noOfColumns % 2 != 0 && noOfRows % 2 == 0)
        {
            for (int m = rangeY * (-1); m < rangeY; m++)
            {
                float newM = m + 0.5f;
                for (int n = rangeX * (-1); n < rangeX + 1; n++)
                {
                    Vector2 pos = new Vector2(n, newM);
                    int colorToUse = Random.Range(0, noOfColors);
                    GameObject tile = Instantiate(tiles[colorToUse], pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.name = "(" + n + ", " + newM + ")";
                    tile.GetComponent<Renderer>().sortingOrder = m + rangeY;
                    currentBoard[n + ((noOfColumns - 1) / 2), m + (noOfRows / 2)] = tile;
                }
            }
        }
        else if (noOfColumns % 2 == 0 && noOfRows % 2 != 0)
        {
            for (int m = rangeY * (-1); m < rangeY + 1; m++)
            {
                for (int n = rangeX * (-1); n < rangeX; n++)
                {
                    float newN = n + 0.5f;
                    Vector2 pos = new Vector2(newN, m);
                    int colorToUse = Random.Range(0, noOfColors);
                    GameObject tile = Instantiate(tiles[colorToUse], pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.name = "(" + newN + ", " + m + ")";
                    tile.GetComponent<Renderer>().sortingOrder = m + rangeY;
                    currentBoard[n + (noOfColumns / 2), m + ((noOfRows - 1) / 2)] = tile;
                }
            }
        }
        else if (noOfColumns % 2 != 0 && noOfRows % 2 != 0)
        {
            for (int m = rangeY * (-1); m < rangeY + 1; m++)
            {
                for (int n = rangeX * (-1); n < rangeX + 1; n++)
                {
                    Vector2 pos = new Vector2(n, m);
                    int colorToUse = Random.Range(0, noOfColors);
                    GameObject tile = Instantiate(tiles[colorToUse], pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.name = "(" + n + ", " + m + ")";
                    tile.GetComponent<Renderer>().sortingOrder = m + rangeY;
                    currentBoard[n + ((noOfColumns - 1) / 2), m + ((noOfRows - 1) / 2)] = tile;
                }
            }
        }
    }

    public void FillBoard()
    {
        for (int x = 0; x < noOfColumns; x++)
        {
            for (int y = 0; y < noOfRows; y++)
            {
                if (currentBoard[x, y] == null)
                {
                    float posX = (x - (noOfColumns / 2)) + 0.5f;
                    float posY = (y - (noOfRows / 2)) + 0.5f;
                    if (noOfRows % 2 == 1)
                        posY = posY - 0.5f;
                    if (noOfColumns % 2 == 1)
                        posX = posX - 0.5f;
                    Vector2 pos = new Vector2(posX, posY);
                    int colorToUse = Random.Range(0, noOfColors);
                    GameObject tile = Instantiate(tiles[colorToUse], pos, Quaternion.identity);
                    tile.transform.parent = this.transform;
                    tile.name = "(" + posX + ", " + posY + ")";
                    tile.GetComponent<Renderer>().sortingOrder = y;
                    currentBoard[x, y] = tile;
                }
            }
        }
    }

}
