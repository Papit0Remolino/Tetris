using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public static PieceSpawner singleton = null;
    [SerializeField] GameObject[] levelPieces;
    Transform previsualization;
    GameObject previsualizatingPiece;
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        previsualization = transform.GetChild(0);
        SpawnNextPiece();
        Invoke("TeleportToTop", 1f);
    }

    public void SpawnNextPiece()
    {
        int i = Random.Range(0, levelPieces.Length);
        previsualizatingPiece = Instantiate(levelPieces[i], previsualization.position, Quaternion.identity);
        previsualizatingPiece.GetComponent<Piece>().enabled = false;
    }

    public void TeleportToTop()
    {
        GameObject pieceToTeleport = previsualizatingPiece;
        previsualizatingPiece.transform.position = this.transform.position;
        previsualizatingPiece.GetComponent<Piece>().enabled = true;
        SpawnNextPiece();
    }
}
