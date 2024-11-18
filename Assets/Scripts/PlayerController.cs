using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 4; // distance between two lanes
    public float jumpForce;
    public float Gravity = -20;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        
        if(controller.isGrounded){
            // direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }else{
            direction.y += Gravity * Time.deltaTime;
        }
        

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Debug.Log("Right arrow key pressed");
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Debug.Log("Left arrow key pressed");
            desiredLane--;
            if(desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position;
        targetPosition.z = transform.position.z;
        targetPosition.y = transform.position.y;

        if (desiredLane == 0)
        {
            targetPosition.x = -laneDistance;
        }
        else if (desiredLane == 1)
        {
            targetPosition.x = 0;
        }
        else if (desiredLane == 2)
        {
            targetPosition.x = laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 800 * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump(){
        direction.y = jumpForce;
    }
}
