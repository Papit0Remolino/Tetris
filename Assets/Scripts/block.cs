using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public void RemovePositionFromGrid()
    {
        GridHelper.gridhelper.RemovePosFromGrid(transform.position.x, transform.position.y);
    }
    public void SendPosToGrid()
    {
        GridHelper.gridhelper.UpdateGrid(transform.position.x,transform.position.y);
    }
}
