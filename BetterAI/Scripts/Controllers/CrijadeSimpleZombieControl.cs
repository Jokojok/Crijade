using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrijadeSimpleZombieControl : MonoBehaviour
{
    [SerializeField]
    IPLCharacterController2D controller = null;

    bool movingRight = true;

    bool isJumping = false;
    float jumpSpeed = 0f;

    [SerializeField]
    Transform upperBodyCheckA = null;
    [SerializeField]
    Transform lowerBodyCheckA = null;
    [SerializeField]
    Transform groundCheckA = null;
    [SerializeField]
    Transform groundCheckB = null;

    [SerializeField]
    LayerMask groundLayers;
    [SerializeField]
    LayerMask enemyLayers;

    Transform myTransform;
    Rigidbody2D myRigidbody;

    private void Start()
    {
        controller.HorizontalInput = 4f;

        groundLayers = LayerMask.GetMask("Ground");
        enemyLayers = LayerMask.GetMask("Enemies");

        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isJumping)
        {
            if (CheckGround() && CheckJumpableWall() && !(jumpSpeed > 0f))
            {
                Jump();
            }
            else if (!CheckGround() || CheckWall() || CheckEnemy())
            {
                TurnAround();
            }
        }
    }

    bool CheckGround()
    {
        if (Physics2D.Raycast(groundCheckB.position, Vector2.down, 0.1f, groundLayers).collider)
        {
            isJumping = false;
            return true;
        }
        return false;
    }

    bool CheckWall()
    {
        if (Physics2D.Raycast(upperBodyCheckA.position, Vector2.right, 0.1f, groundLayers).collider || Physics2D.Raycast(lowerBodyCheckA.position, Vector2.right, 0.1f, groundLayers).collider)
        {
            return true;
        }
        return false;
    }

    bool CheckJumpableWall()
    {
        if (ReferenceEquals(Physics2D.Raycast(upperBodyCheckA.position, Vector2.right, 0.1f, groundLayers).collider, null) && Physics2D.Raycast(lowerBodyCheckA.position, Vector2.right, 1f, groundLayers).collider)
        {
            return true;
        }
        return false;
    }

    bool CheckEnemy()
    {
        if (Physics2D.Raycast(lowerBodyCheckA.position, Vector2.right, 0.1f, enemyLayers).collider)
        {
            return true;
        }
        return false;
    }

    void TurnAround()
    {
        if (movingRight == true)
        {
            myTransform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            myTransform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    void Jump()
    {
        isJumping = true;
        jumpSpeed = 6f;
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
    }
}