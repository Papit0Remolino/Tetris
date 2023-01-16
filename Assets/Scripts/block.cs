using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public void SendPosToGrid()
    {
        GridHelper.gridhelper.UpdateGrid(transform.position.x,transform.position.y, transform);
    }

    //private void Update()
    //{
    //    Debug.Log(transform.position);
    //}
}
