using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBlock : MonoBehaviour
{
    private float previosTime;
    public float FullTime = 0.8f;
    public static int Height =20;
    public static int Width = 10;
    public Vector3 rotation;
    private static Transform[,] grid = new Transform[Width, Height];
    private GameManager gameManag;

    void Start()
    {
        gameManag = GameManager.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1f, 0f, 0f);
            if (!MoveValid())
            {
                transform.position -= new Vector3(-1f, 0f, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0f, 0f);
            if (!MoveValid())
            {
                transform.position -= new Vector3(1f, 0f, 0f);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotation), new Vector3(0f,0f,1f), 90);
            if (!MoveValid())
            {
                transform.RotateAround(transform.TransformPoint(rotation), new Vector3(0f, 0f, 1f), -90);
            }
        }

        if (Time.time - previosTime > (Input.GetKey(KeyCode.DownArrow) ? FullTime / 10 : FullTime)) //
        {
            transform.position += new Vector3(0f, -1f, 0f);
            if (!MoveValid())
            {
                transform.position -= new Vector3(0f, -1f, 0f);
                addGrid();
                lineCheck();
                this.enabled = false;
                FindObjectOfType<Respawn>().createBlock();
                gameManag.ScoreLine(10);
            }
            
            previosTime = Time.time;
        }
    }

    bool MoveValid()
    {
        foreach (Transform child in transform)
        {
            int roundX = Mathf.RoundToInt(child.transform.position.x);
            int roundY = Mathf.RoundToInt(child.transform.position.y);

            if(roundX<0 || roundX>=Width || roundY<0 || roundY >= Height)
            {
                return false;
            }
            if (grid[roundX, roundY]!=null)
            {
                return false;
            }
        }
        return true;
    }

    void lineCheck()
    {
        for (int i = Height-1; i>=0; i--)
        {
            if (hasLine(i))
            {
                deleteLine(i);
                rowDown(i);
                gameManag.LinesCountP();
                gameManag.ScoreLine(100);
            }
        }
    }

    private bool hasLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        gameManag.LineCount += 1;
        return true;
    }

    private void deleteLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    private void rowDown(int i)
    {
        for (int k = i; k < Height; k++)
        {
            for (int j = 0; j < Width; j++) {
                if (grid[j, k] != null)
                {
                    grid[j, k - 1] = grid[j, k];
                    grid[j, k] = null;
                    grid[j, k - 1].transform.position -= new Vector3(0f, 1f, 0f);
                }
            }
        }
    }

    void addGrid()
    {
        foreach (Transform child in transform)
        {
            int roundX = Mathf.RoundToInt(child.transform.position.x);
            int roundY = Mathf.RoundToInt(child.transform.position.y);

            grid[roundX, roundY] = child;
        }
    }
}
