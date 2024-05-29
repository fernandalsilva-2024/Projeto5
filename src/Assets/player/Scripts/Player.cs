using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    public float laneSpeed = 10f;
    public float JumpLength;
    public float JumpHeight;
    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    public int maxLife = 3;
    public float invincibleTime;
    public GameObject mesh;

    private Animator animator;
    private Rigidbody rb;
    private BoxCollider BoxCollider;
    private Vector3 verticalTargetPosition;
    private Vector3 BoxCollidersize;
    private int currentLane = 0;
    private float JumpStart;
    private bool Jumping = false;
    private int currentLife;
    private bool invincible = false;
    static int blinkingValue;
    private uiManager uiMan;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        BoxCollider = GetComponent<BoxCollider>();
        BoxCollidersize = BoxCollider.size;
        animator.Play("runStart");
        currentLife = maxLife;
        speed = minSpeed;
        uiMan = FindObjectOfType<uiManager>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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

        if (Jumping)
        {
            float ratio = (transform.position.z - JumpStart) / JumpLength;
            if (ratio >= 1f)
            {
                Jumping = false;
                animator.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * JumpHeight;
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
        if (!Jumping)
        {
            JumpStart = transform.position.z;
            animator.SetBool("Jumping", true);
            Jumping = true;
        }
    }

    public void IncreaseSpeed()
    {
        speed *= 1.15f;
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            other.gameObject.SetActive(false);
        }

        if (invincible)
            return;

        if (other.CompareTag("Obstaculo"))
        {
            currentLife--;
            uiMan.UpdateLives(currentLife);
            animator.SetTrigger("Hit");
            speed = 0;
            if(currentLife <= 0)
            {
                speed = 0;
                Invoke("CallMenu", 1f);
            }
            else
            {
                StartCoroutine(Blinking(invincibleTime));
            }
        }
    }

    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;
        bool enabled = false;
        yield return new WaitForSeconds(1f);
        speed = minSpeed;
        while (timer < time && invincible)
        {
            mesh.SetActive(enabled);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                enabled = !enabled;
            }
        }
        mesh.SetActive(true);
        invincible = false;
    }

    void CallMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}


