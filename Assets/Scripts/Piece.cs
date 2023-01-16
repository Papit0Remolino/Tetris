using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]float width;
    [SerializeField]float height;
    [SerializeField]bool canRotate;
    public bool offsetX;
    public bool offsetY;
    float fallCooldown = 0.7f;
    bool isOnCooldown;
    bool isFalling;
    public List<block> blocks;
    public int currentRotation;
    public Vector2 rotationOffset = new Vector2 (0, 0);
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
        if (fallCooldown < 0 && !isFalling)
        {
            Fall();
        }

        Rotate();


        PieceMovement();
        CheckIfEmpty(); // cuando vacias una fila que el gameobject parent vacio no se quede por ahi 
    }
    void Fall()
    {
        isFalling = true;
        fallCooldown = 1;
        if (CheckIfCanFall())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            isFalling = false;
        }
        else
        {
            foreach (block b in blocks)
            {
                b.SendPosToGrid();
            }
            PieceSpawner.singleton.TeleportToTop();
            GridHelper.Singleton.CheckIfRowComplete();
            GridHelper.Singleton.CheckIfGameOver();
            //GridHelper.Singleton.PrevisualizeGrid();
            Destroy(this);
        }
    }
    void PieceMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0.1f && transform.position.x < (11 - width) && !isOnCooldown)
        {
            if (CheckIfCanMove(1))
            {
                StartCoroutine(Move());
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }

        }
        if (inputX < -0.1f && transform.position.x > 0 + width && !isOnCooldown)
        {
            if (CheckIfCanMove(-1))
            {
                StartCoroutine(Move());
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }
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
        if (transform.position.y > height)
        {
            foreach (block b in blocks)
            {
                if (GridHelper.grid[(int)Mathf.Round(b.transform.position.x + rotationOffset.x), (int)Mathf.Round(b.transform.position.y - 1 + rotationOffset.y)] == null)
                {
                    canFall = true;
                }
                else
                {
                    canFall = false;
                    break;
                }
            }
        }
        return canFall;
    }

    bool CheckIfCanMove(int dir)
    {
        bool canMove = false;
        if (transform.position.y > 0)
        {
            foreach (block b in blocks)
            {
                //Debug.Log("x = " + (b.transform.position.x ).ToString("F2") + "y = " + (b.transform.position.y).ToString("F2"));
                if (GridHelper.grid[(int)Mathf.Round(b.transform.position.x + dir + rotationOffset.x), (int)Mathf.Round(b.transform.position.y + rotationOffset.y) ] == null)
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                    break;
                }
            }
        }
        return canMove;
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.R) && canRotate)
        {
            bool isValid = false;

            transform.Rotate(0, 0, 90);

            foreach (block b in blocks)
            {
                if (GridHelper.Singleton.CheckIfPositionValid(b.transform.position.x, b.transform.position.y, rotationOffset))
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                float storeWidth = width;
                width = height;
                height = storeWidth;

                if (currentRotation == 360)
                {
                    currentRotation = 0;
                }
                currentRotation += 90;

                switch (currentRotation)
                {
                    case 90:
                        transform.position += new Vector3(.5f, .5f, 0);
                        rotationOffset = new Vector2(-1, 0);
                        break;
                    case 180:
                        transform.position += new Vector3(-.5f, -.5f, 0);
                        rotationOffset = new Vector2(-1, -1);
                        break;
                    case 270:
                        transform.position += new Vector3(.5f, .5f, 0);
                        rotationOffset = new Vector2(0, -1);
                        break;
                    case 360:
                        rotationOffset = new Vector2(0, 0);
                        transform.position += new Vector3(-.5f, -.5f, 0);
                        break;
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);
            }



        }

    }

}
