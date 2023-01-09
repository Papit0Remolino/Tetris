using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]int width;
    float fallCooldown = 0.7f;
    [SerializeField]bool isOnCooldown;
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
        if (fallCooldown > 0) { fallCooldown -= Time.deltaTime; }
        if (Input.GetKey(KeyCode.S))
        {
            fallCooldown -= 8 * Time.deltaTime;
        }
        if (fallCooldown < 0)
        {
            Fall();
        }


        PieceMovement();
        CheckIfEmpty();
    }
    void Fall()
    {
        fallCooldown = 1;
        foreach (block b in blocks)
        {
            b.RemovePositionFromGrid();
        }
        if (CheckIfCanFall())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            foreach (block b in blocks)
            {
                b.SendPosToGrid();
            }
        }
        else
        {
            foreach (block b in blocks)
            {
                b.SendPosToGrid();
            }
            PieceSpawner.singleton.TeleportToTop();
            Destroy(this);
        }
    }
    void PieceMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0.1f && transform.position.x < (11 - width) && !isOnCooldown)
        {
            StartCoroutine(Move());
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (inputX < -0.1f && transform.position.x > 0 && !isOnCooldown)
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
        bool canFall = false;
        if (transform.position.y > 0)
        {
            foreach (block b in blocks)
            {
                if (GridHelper.grid[(int)b.transform.position.x, (int)b.transform.position.y - 1] == null)
                {
                    Debug.Log(GridHelper.grid[(int)b.transform.position.x, (int)b.transform.position.y - 1]);
                    canFall = true;
                    Debug.Log("1");
                }
                //else if (GridHelper.grid[(int)b.transform.position.x, (int)b.transform.position.y - 1].parent == this)
                //{
                //    canFall = true;
                //    Debug.Log("2");
                //}
                else
                {
                    Debug.Log("3");
                    canFall = false;
                    break;
                }
            }
        }
        return canFall;
    }

}
