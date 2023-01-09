using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]int width;
    float fallCooldown = 0.7f;
    public bool isFalling;
    [SerializeField]bool isOnCooldown;
    bool justPressedS;
    public List<block> blocks;
    private void Start()
    {
        for (int i=0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != null)
            {
                blocks.Add(transform.GetChild(i).GetComponent<block>());
            }
        }
    }
    void Update()
    {
        if (!isFalling)
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
        PieceMovement();
        CheckIfEmpty();
    }
    IEnumerator Fall(float cooldown)
    {
        isFalling = true;
        yield return new WaitForSeconds(cooldown);
        foreach (block b in blocks)
        {
            b.RemovePositionFromGrid();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        foreach (block b in blocks)
        {
            b.SendPosToGrid();
        }
        isFalling = false;
        if (transform.position.y < 1)
        {
            PieceSpawner.singleton.TeleportToTop();
            Destroy(this);
        }
    }
    void PieceMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0.1f && transform.position.x < (13 - width) && !isOnCooldown)
        {
            StartCoroutine(Move());
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (inputX < -0.1f && transform.position.x > 2 && !isOnCooldown)
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
    void CheckIfEmpty()
    {
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
    bool CheckIfCanFall()
    {
        bool canFall;
        foreach (block b in blocks)
        {
            if (GridHelper.grid[(int)b.transform.position.x, (int)b.transform.position.y - 1] == null)
            {
                canFall = true;
            }
            else if (GridHelper.grid[(int)b.transform.position.x, (int)b.transform.position.y - 1].parent == this)
            {
                canFall = true;
            }
            else
            {
                return false;
            }
            return canFall;
        }
    }

}
