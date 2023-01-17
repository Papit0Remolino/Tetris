using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class GridHelperTetris : MonoBehaviour
{
    public static GridHelperTetris Singleton = null;
    public static int w = 11, h = 18 + 5;
    public static Transform[,] grid = new Transform[w, h];
    public bool piecePlaced;
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI pointsText;
    int points;
    [SerializeField] TextMeshProUGUI linesText;
    int lines; 
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource rowCompleteSound;

    private void Start()
    {
        if (Singleton == null)
        {
            Singleton = this;
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
        grid[(int)Mathf.Round(x - .1f), (int)Mathf.Round(y - .1f)] = block;
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
                rowCompleteSound.Play();
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
                gameOverSound.Play();
                break;
            }
        }
    }

    //public void PrevisualizeGrid()
    //{
    //    for (int column = 0; column < h; column++)
    //    {
    //        for (int row = 0; row < w; row++)
    //        {
    //            if (grid[row, column] != null)
    //            {
    //                Instantiate(pruebas, new Vector3(row + 30, column + 30, 0),Quaternion.identity);
    //            }
    //        }
    //    }
    //}

    public bool CheckIfPositionValid(float x, float y, Vector2 offset)
    {
        bool isValid = false;
        if (x < 11 && x > Mathf.Abs(offset.x) && y > 0)
        {
            if (grid[(int)Mathf.Round(x - .1f + offset.x) , (int)Mathf.Round(y - .1f + offset.y) ] == null)
            {
                isValid = true;
            }
        }
        return isValid;
    }
}
