using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isPlaced;
    public bool isFalling;

    void Update()
    {
        if (!isFalling && !isPlaced)
        {
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPlaced = false;
    }

    IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(0.7f);
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        isFalling = false;
    }
}
