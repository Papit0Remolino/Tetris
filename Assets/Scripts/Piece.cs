using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]int width;
    float fallCooldown = 0.7f;
    public bool isPlaced;
    public bool isFalling;
    public bool isPrevisualizating;
    bool canMoveToRight;
    bool canMoveToLeft;
    bool isOnCooldown;
    bool justPressedS;

    void Update()
    {
        if (!isFalling && !isPlaced && !isPrevisualizating)
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (justPressedS == true)
                {
                    StopCoroutine(Fall(fallCooldown));
                    justPressedS = false;
                }
                justPressedS = true;
                StartCoroutine(Fall(fallCooldown - 0.6f));
            }
            else
            {
                StartCoroutine(Fall(fallCooldown));
            }
        }
        if (isPlaced)
        {
            PieceSpawner.singleton.SpawnNextPiece();
            Destroy(this);
        }
        PieceMovement();
        SeeIfOnMapBounds();
    }
    IEnumerator Fall(float cooldown)
    {
        isFalling = true;
        yield return new WaitForSeconds(cooldown);
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        isFalling = false;
        if (transform.position.y < 1)
        {
            isPlaced = true;
        }
    }
    void PieceMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0.1f && canMoveToRight && !isOnCooldown)
        {
            StartCoroutine(Move());
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (inputX < -0.1f && canMoveToLeft && !isOnCooldown)
        {
            StartCoroutine(Move());
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
    }
    IEnumerator Move()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(0.15f);
        isOnCooldown = false;
    }
    void SeeIfOnMapBounds()
    {
        //12 es el punto mas alejado a la derecha del mapa donde las piezas se pueden mover
        //teniendo en cuentas de que no todas la piezas miden igual hay que saber su longitud
        if (transform.position.x > (12 - width))
        {
            canMoveToRight = false;
        }
        if (transform.position.x < 3)
        {
            canMoveToLeft = false;
        }
        if (transform.position.x > 3 && transform.position.x < (12 - width))
        {
            canMoveToRight = true;
            canMoveToLeft = true;
        }

    }
}
