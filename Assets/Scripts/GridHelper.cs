using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridHelper : MonoBehaviour
{
    public static GridHelper gridhelper = null;
    public static int w = 11, h = 18 + 5;
    public static Transform[,] grid = new Transform[w, h];
    public bool piecePlaced;
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI pointsText;
    int points;
    [SerializeField] TextMeshProUGUI linesText;
    int lines;  

    private void Start()
    {
        if (gridhelper == null)
        {
            gridhelper = this;
        }
    }
    public static bool IsInsideBorders(Vector2 pos)
    {
        if (pos.x >= 2 && pos.y >= 0 && pos.x < w + 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateGrid(float x, float y, Transform block)
    {
        grid[(int)x , (int)y] = block;
    }
    public void RemovePosFromGrid(float x, float y)
    {
        grid[(int)x , (int)y] = null;
    }

    public void CheckIfRowComplete()
    {
        bool isRowComplete = true;
        for (int column = 0; column < h; column++)
        {
            isRowComplete = true;
            for (int row = 0; row < w; row++)
            {
                if (grid[row, column] == null)
                {
                    isRowComplete = false;
                    break;
                }
            }
            if (isRowComplete) 
            { 
                DeleteRow(column); 
                RearrangeRows(column + 1); 
                points += 1000; pointsText.text = points.ToString();
                lines += 1; linesText.text = lines.ToString(); 
            }
        }
    }

    void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void RearrangeRows(int startColumn)
    {

        for (int column = startColumn; column < h; column++)
        {
            for (int row = 0; row < w; row++)
            {
                if (grid[row, column] != null)
                {
                    grid[row, column - 1] = grid[row, column];
                    grid[row, column] = null;
                    grid[row, column - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public void CheckIfGameOver()
    {
        for (int row = 0; row < w; row++)
        {
            if (grid[row, 15] != null)
            {
                gameOver.SetActive(true);
                break;
            }
        }
    }
}
