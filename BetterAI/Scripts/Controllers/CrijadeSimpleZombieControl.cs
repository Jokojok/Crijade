using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrijadeSimpleZombieControl : MonoBehaviour
{
    [SerializeField]
    IPLCharacterController2D controller = null;

    bool movingRight = true;
    bool isGrounded = false;

    [SerializeField]
    Transform groundCheckA = null;
    [SerializeField]
    Transform groundCheckB = null;
    [SerializeField]
    LayerMask groundLayers = 8  ;

    private void Start()
    {
        controller.HorizontalInput = 4f;
    }

    private void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.Raycast(groundCheckB.position, Vector2.down, 0.5f).collider != null;
        if (!isGrounded)
        {
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
    }
}