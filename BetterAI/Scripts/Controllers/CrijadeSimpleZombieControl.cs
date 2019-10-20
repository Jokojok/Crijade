using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrijadeSimpleZombieControl : MonoBehaviour
{
    [SerializeField]
    IPLCharacterController2D controller = null;

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
        if (CheckGround())
        {
            if (CheckGround() && CheckJumpableWall())
            {
                Jump();
            }
            else if (CheckFall() || CheckWall() || CheckEnemy())
            {
                TurnAround();
            }
        }
    }

    bool CheckGround()
    {
        if (Physics2D.OverlapArea(groundCheckA.position, groundCheckB.position, groundLayers))
        {
            return true;
        }
        return false;
    }

    bool CheckFall()
    {
        if (Physics2D.Raycast(groundCheckB.position, Vector2.down, 0.1f, groundLayers).collider)
        {
            return false;
        }
        return true;
    }

    bool CheckWall()
    {
        if (Physics2D.Raycast(upperBodyCheckA.position, myTransform.right, 0.1f, groundLayers).collider 
            || Physics2D.Raycast(lowerBodyCheckA.position, myTransform.right, 0.1f, groundLayers).collider)
        {
            return true;
        }
        return false;
    }

    bool CheckJumpableWall()
    {
        if (ReferenceEquals(Physics2D.Raycast(upperBodyCheckA.position, myTransform.right, 1f, groundLayers).collider, null) 
            && ReferenceEquals(Physics2D.Raycast(upperBodyCheckA.position, myTransform.up, 1f, groundLayers).collider, null) 
            && Physics2D.Raycast(lowerBodyCheckA.position, myTransform.right, 1f, groundLayers).collider)
        {
            return true;
        }
        return false;
    }

    bool CheckEnemy()
    {
        if (Physics2D.Raycast(upperBodyCheckA.position, myTransform.right, 0.1f, enemyLayers).collider)
        {
            return true;
        }
        return false;
    }

    void TurnAround()
    {
        myTransform.eulerAngles = new Vector3(0, myTransform.eulerAngles.y == 0 ? 180 : 0, 0);
    }

    void Jump()
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 6f);
    }
}