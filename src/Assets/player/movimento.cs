using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class movimento : MonoBehaviour
{

    private CharacterController controller;

    private float jumpedvel;
    public float speed;
    public float jumpHeight;
    public float gravity;


    public float horizontalSpeed;
    private bool isMovingLeft;
    private bool isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * speed;

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpedvel = jumpHeight;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x <3 && !isMovingRight)
            {
                isMovingRight = true;
                StartCoroutine(RightMove());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) &&  transform.position.x >-3 && !isMovingLeft)
            {
                isMovingLeft = true;
                StartCoroutine(LeftMove());
            }

        }

        else
        {
            jumpedvel -= gravity;
        }

        direction.y = jumpedvel;

        controller.Move(direction * Time.deltaTime);
    }

    IEnumerator LeftMove()
    {
            for (float i = 0; i < 10; i += 1)
        {
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }
    IEnumerator RightMove()
    {
            for (float i = 0; i < 10; i += 1)
        {
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingRight = false;
    }
}
