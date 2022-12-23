using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("funciono(Colisiones)");
        Piece parentPiece = GetComponentInParent<Piece>();
        parentPiece.isPlaced = true;
    }
}
