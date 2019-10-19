using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrijadeSimpleZombieControl : MonoBehaviour
{
    [SerializeField]
    IPLCharacterController2D controller = null;

    bool movingRight = true;

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

    private void Start()
    {
        controller.HorizontalInput = 4f;
        groundLayers = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        CheckGround();
        CheckWall();
    }

    void CheckGround()
    {
        if (!Physics2D.Raycast(groundCheckB.position, Vector2.down, 0.5f, groundLayers).collider)
        {
            TurnAround();
        }
    }

    void CheckWall()
    {
        if (Physics2D.Raycast(upperBodyCheckA.position, Vector2.right, 0.1f, groundLayers).collider || Physics2D.Raycast(lowerBodyCheckA.position, Vector2.right, 0.1f, groundLayers).collider)
        {
            TurnAround();
        }
    }

    private void TurnAround()
    {
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }
}