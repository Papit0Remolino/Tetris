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
        Time.timeScale = 1;
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
        float offsetX = 0;
        float offsetY = 0;
        if (previsualizatingPiece.GetComponent<Piece>().offsetX)
        {
            offsetX = -0.5f;
        }
        if (previsualizatingPiece.GetComponent<Piece>().offsetY)
        {
            offsetY = -0.5f;
        }
        previsualizatingPiece.transform.position = new Vector3(this.transform.position.x - offsetX, this.transform.position.y - offsetY, this.transform.position.z);

        previsualizatingPiece.GetComponent<Piece>().enabled = true;
        SpawnNextPiece();
    }
}
