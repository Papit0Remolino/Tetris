using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] levelPieces;
    Transform previsualization;
    GameObject previsualizatingPiece;
    void Start()
    {
        previsualization = transform.GetChild(0);
        SpawnNextPiece();
    }

    void Update()
    {
        
    }

    public void SpawnNextPiece()
    {
        int i = Random.Range(0, levelPieces.Length);
        previsualizatingPiece = Instantiate(levelPieces[i], previsualization.position, Quaternion.identity);
        StartCoroutine(TeleportToTop());
    }

    IEnumerator TeleportToTop()
    {
        GameObject pieceToTeleport = previsualizatingPiece;
        yield return new WaitForSeconds(1f);
        previsualizatingPiece.transform.position = this.transform.position;
        SpawnNextPiece();
    }
}
