using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 4; // distance between two lanes
    public float jumpForce;
    public float Gravity = -20;
    private bool isSliding = false;
    public float slideDuration = 1f;
    private float slideTimer;

    private Vector3 initialPosition;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 1f * Time.deltaTime;
        }
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right arrow key pressed");
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left arrow key pressed");
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                StopSlide();
            }
        }

        Vector3 targetPosition = initialPosition;
        targetPosition.z = transform.position.z;
        targetPosition.y = transform.position.y;

        if (desiredLane == 0)
        {
            targetPosition.x = initialPosition.x - laneDistance;
        }
        else if (desiredLane == 1)
        {
            targetPosition.x = initialPosition.x;
        }
        else if (desiredLane == 2)
        {
            targetPosition.x = initialPosition.x + laneDistance;
        }

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 800 * Time.fixedDeltaTime);
        if (transform.position == targetPosition)
        {
            return;
        }

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
        animator.SetTrigger("Jump");
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        // controller.height = controller.height / 3;
        // controller.center = new Vector3(controller.center.x, controller.center.y / 3, controller.center.z);
        animator.SetTrigger("Slide");
        Debug.Log("Slide triggered");
    }

    private void StopSlide()
    {
        isSliding = false;
        // controller.height = controller.height * 3;
        // controller.center = new Vector3(controller.center.x, controller.center.y * 3, controller.center.z);
        animator.ResetTrigger("Slide");
        Debug.Log("Slide stopped");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
