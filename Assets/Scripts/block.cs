using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    Piece piece;
    private void Start()
    {
        piece = GetComponentInParent<Piece>();
    }
    public void SendPosToGrid()
    {
        GridHelperTetris.Singleton.UpdateGrid(transform.position.x + piece.rotationOffset.x, transform.position.y + piece.rotationOffset.y, transform);
    }
}
