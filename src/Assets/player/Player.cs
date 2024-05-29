using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float laneSpeed;
    public float JumpLength;
    public float JumpHeight;

    private Animator animator;
    private Rigidbody rb;
    private BoxCollider BoxCollider;
    private Vector3 verticalTargetPosition;
    private Vector3 BoxCollidersize;
    private int currentLane = 1;
    private float JumpStart;
    private bool Jumping = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        BoxCollider = GetComponent<BoxCollider>();
        BoxCollidersize = BoxCollider.size;
        animator.Play("runStart");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-6);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(6);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if(Jumping)
        {
            float ratio = (transform.position.z - JumpStart) / JumpLength;
            if (ratio >= 1f)
            {
                Jumping = false;
                animator.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y =Mathf.Sin(ratio * Mathf.PI) * JumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        Vector3 targetPisition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPisition, laneSpeed = Time.maximumDeltaTime);
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < -6 || targetLane > 12)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3(currentLane, 0, 0);
    }

    void Jump() 
    {
        if(!Jumping)
        {
            JumpStart = transform.position.z;
            animator.SetFloat("JumpSpeed", speed / JumpLength);
            animator.SetBool("Jumping", true);
            Jumping = true;
        }
    }

     private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }
}


