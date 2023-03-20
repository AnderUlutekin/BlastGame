using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private int targetX, targetY;

    private Board board;
    public GameObject leftTile, rightTile, upTile, downTile;
    private LevelController levelController;

    private SpriteRenderer spriteRenderer;

    public Sprite spriteDefault;
    public Sprite spriteA;
    public Sprite spriteB;
    public Sprite spriteC;

    public bool visited = false;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteDefault;
        board = FindObjectOfType<Board>();
        levelController = FindObjectOfType<LevelController>();
        SetDimesions();
        SetNeighbors();
    }

    private void Update()
    {
        if (CanFall())
        {
            Fall();
        }
        ChangeSymbol();
    }

    public void SetDimesions()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (board.GetRowNum() % 2 == 0 && board.GetColumnNum() % 2 == 0)
        {
            x = x - 0.5f;
            y = y - 0.5f;
            targetX = (int)(x + (board.GetColumnNum() / 2));
            targetY = (int)(y + (board.GetRowNum() / 2));
        }
        else if (board.GetRowNum() % 2 != 0 && board.GetColumnNum() % 2 == 0)
        {
            x = x - 0.5f;
            targetX = (int)(x + (board.GetColumnNum() / 2));
            targetY = (int)(y + (board.GetRowNum() / 2));
        }
        else if (board.GetRowNum() % 2 == 0 && board.GetColumnNum() % 2 != 0)
        {
            y = y - 0.5f;
            targetX = (int)(x + (board.GetColumnNum() / 2));
            targetY = (int)(y + (board.GetRowNum() / 2));
        }
        else if (board.GetRowNum() % 2 != 0 && board.GetColumnNum() % 2 != 0)
        {
            targetX = (int)(x + (board.GetColumnNum() / 2));
            targetY = (int)(y + (board.GetRowNum() / 2));
        }
    }

    public void SetNeighbors()
    {
        leftTile = null;
        rightTile = null;
        upTile = null;
        downTile = null;

        if (targetX > 0)
        {
            //There is left
            leftTile = board.currentBoard[targetX - 1, targetY];
            leftTile.GetComponent<Tile>().rightTile = gameObject;
        }
        if (targetY > 0)
        {
            //There is down
            downTile = board.currentBoard[targetX, targetY - 1];
            downTile.GetComponent<Tile>().upTile = gameObject;
        }
        if (targetX < board.GetColumnNum() - 1)
        {
            //There is right
            rightTile = board.currentBoard[targetX + 1, targetY];
            rightTile.GetComponent<Tile>().leftTile = gameObject;
        }
        if (targetY < board.GetRowNum() - 1)
        {
            //There is up
            upTile = board.currentBoard[targetX, targetY + 1];
            upTile.GetComponent<Tile>().downTile = gameObject;
        }
    }

    private void OnMouseDown()
    {
        if (levelController.isPopupActive == false)
        {
            if (CheckIfCanBePopped())
            {
                Pop();
                levelController.MakeMove();
            }
        }
    }

    private void Pop()
    {
        if (leftTile != null)
            leftTile.GetComponent<Tile>().rightTile = null;
        if (rightTile != null)
            rightTile.GetComponent<Tile>().leftTile = null;
        if (upTile != null)
            upTile.GetComponent<Tile>().downTile = null;
        if (downTile != null)
            downTile.GetComponent<Tile>().upTile = null;

        if (rightTile != null && rightTile.CompareTag(gameObject.tag))
        {
            rightTile.GetComponent<Tile>().Pop();
        }
        if (leftTile != null && leftTile.CompareTag(gameObject.tag))
        {
            leftTile.GetComponent<Tile>().Pop();
        }
        if (upTile != null && upTile.CompareTag(gameObject.tag))
        {
            upTile.GetComponent<Tile>().Pop();
        }
        if (downTile != null && downTile.CompareTag(gameObject.tag))
        {
            downTile.GetComponent<Tile>().Pop();
        }
        Destroy(gameObject);
        board.currentBoard[targetX, targetY] = null;
        levelController.score.text = (int.Parse(levelController.score.text) + CalculatePoints()).ToString();
    }

    private bool CheckIfCanBePopped()
    {
        if (rightTile != null && rightTile.CompareTag(gameObject.tag))
        {
            return true;
        }
        if (leftTile != null && leftTile.CompareTag(gameObject.tag))
        {
            return true;
        }
        if (upTile != null && upTile.CompareTag(gameObject.tag))
        {
            return true;
        }
        if (downTile != null && downTile.CompareTag(gameObject.tag))
        {
            return true;
        }
        return false;
    }

    private bool CanFall()
    {
        if (targetY > 0 && board.currentBoard[targetX, targetY - 1] == null)
        {
            return true;
        }
        return false;
    }

    private void Fall()
    {
        int fallY = -1;
        int y = targetY - 1;
        while (y >= 0 && board.currentBoard[targetX, y] == null)
        {
            fallY = y;
            y--;
        }

        if (fallY != -1)
        {
            float newY = fallY - board.GetRowNum() / 2 + 0.5f;
            if (board.GetRowNum() % 2 == 1)
                newY = newY - 0.5f;
            Vector2 tempPos = new Vector2(transform.position.x, newY);
            transform.position = Vector2.Lerp(transform.position, tempPos, 1);

            board.currentBoard[targetX, fallY] = gameObject;
            board.currentBoard[targetX, targetY] = null;
            targetY = fallY;

            gameObject.GetComponent<Renderer>().sortingOrder = fallY;
            gameObject.name = "(" + transform.position.x + ", " + transform.position.y + ")";

            if (targetX > 0)
            {
                //There is left
                leftTile = board.currentBoard[targetX - 1, targetY];
                if (leftTile != null)
                    leftTile.GetComponent<Tile>().rightTile = gameObject;
            }
            if (targetY > 0)
            {
                //There is down
                downTile = board.currentBoard[targetX, targetY - 1];
                if (downTile != null)
                    downTile.GetComponent<Tile>().upTile = gameObject;
            }
            if (targetX < board.GetColumnNum() - 1)
            {
                //There is right
                rightTile = board.currentBoard[targetX + 1, targetY];
                if (rightTile != null)
                    rightTile.GetComponent<Tile>().leftTile = gameObject;
            }
        }
    }

    private void CountNeighbors(ref int counter)
    {
        visited = true;
        if (rightTile != null && rightTile.CompareTag(gameObject.tag) && rightTile.GetComponent<Tile>().visited == false)
        {
            counter++;
            rightTile.GetComponent<Tile>().CountNeighbors(ref counter);
        }
        if (leftTile != null && leftTile.CompareTag(gameObject.tag) && leftTile.GetComponent<Tile>().visited == false)
        {
            counter++;
            leftTile.GetComponent<Tile>().CountNeighbors(ref counter);
        }
        if (upTile != null && upTile.CompareTag(gameObject.tag) && upTile.GetComponent<Tile>().visited == false)
        {
            counter++;
            upTile.GetComponent<Tile>().CountNeighbors(ref counter);
        }
        if (downTile != null && downTile.CompareTag(gameObject.tag) && downTile.GetComponent<Tile>().visited == false)
        {
            counter++;
            downTile.GetComponent<Tile>().CountNeighbors(ref counter);
        }
    }

    private void ChangeSymbol()
    {
        int c = 0;
        CountNeighbors(ref c);
        levelController.ResetVisitedInfo();
        c++;
        if (c <= levelController.A)
        {
            //Default
            spriteRenderer.sprite = spriteDefault;
        }
        else if (c <= levelController.B)
        {
            //A
            spriteRenderer.sprite = spriteA;

        }
        else if (c <= levelController.C)
        {
            //B
            spriteRenderer.sprite = spriteB;

        }
        else
        {
            //C
            spriteRenderer.sprite = spriteC;
        }
    }

    private int CalculatePoints()
    {
        if (spriteRenderer.sprite == spriteDefault)
        {
            return 50;
        }
        else if (spriteRenderer.sprite == spriteA)
        {
            return 75;
        }
        else if (spriteRenderer.sprite == spriteB)
        {
            return 100;
        }
        else
        {
            return 125;
        }
    }
}
