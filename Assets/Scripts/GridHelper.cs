using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public static GridHelper gridhelper = null;
    public static int w = 11, h = 18 + 5;
    public static Transform[,] grid = new Transform[w, h];

    private void Start()
    {
        if (gridhelper == null)
        {
            gridhelper = this;
        }
    }
    public static Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
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
    public void UpdateGrid(float x, float y)
    {
        grid[(int)x-2 , (int)y] = null;
    }
    public void RemovePosFromGrid(float x, float y)
    {
        grid[(int)x-2 , (int)y] = null;
    }
}
