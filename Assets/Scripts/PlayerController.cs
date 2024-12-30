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

    private bool isInvincible = false;
    private float powerUpTimer = 0f;
    private float originalJumpForce;
    public float originalForwardSpeed;
    private bool doubleCoinsActive = false;

    // Reference to the point light
    private Light powerUpLight;
    private float powerUpActivationTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        originalJumpForce = jumpForce;
        originalForwardSpeed = forwardSpeed;

        // Find the point light
        powerUpLight = GetComponentInChildren<Light>();
        if (powerUpLight != null)
        {
            powerUpLight.enabled = false; // Disable the light initially
        }
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.2f * Time.deltaTime;
            originalForwardSpeed += 0.2f * Time.deltaTime;
        }
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
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

        if (SwipeManager.swipeDown)
        {
            StartSlide();
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

        // // Handle power-up timer
        // if (powerUpTimer > 0)
        // {
        //     powerUpTimer -= 5*Time.deltaTime;
        //     Debug.Log($"Power-up timer: {powerUpTimer}");
        //     if (powerUpTimer <= 0)
        //     {
        //         DeactivatePowerUp();
        //     }
        // }
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }

        // Move the player
        controller.Move(direction * Time.deltaTime);

        // Handle power-up timer
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            Debug.Log($"Power-up timer: {powerUpTimer}");
            if (powerUpTimer <= 0)
            {
                DeactivatePowerUp();
            }
            else
            {
                UpdatePowerUpLight();
            }
        }
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
        controller.height = controller.height / 3;
        controller.center = new Vector3(controller.center.x, controller.center.y / 3, controller.center.z);
        animator.SetTrigger("Slide");
        Debug.Log("Slide triggered");
    }

    private void StopSlide()
    {
        isSliding = false;
        controller.height = controller.height * 3;
        controller.center = new Vector3(controller.center.x, controller.center.y * 3, controller.center.z);
        animator.ResetTrigger("Slide");
        Debug.Log("Slide stopped");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log($"Collision with: {hit.gameObject.name}, Layer: {LayerMask.LayerToName(hit.gameObject.layer)}, Tag: {hit.gameObject.tag}");

        if (hit.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            // Debug.Log($"Obstacle detected: {hit.gameObject.name}");
            PlayerManager.gameOver = true;
        }
        else
        {
            // Debug.Log($"Non-Obstacle detected: {hit.gameObject.name}");
        }
    }

    public void ActivatePowerUp(PowerUp.PowerUpType powerUpType, float duration)
    {
        // Deactivate any existing power-up
        DeactivatePowerUp();

        Debug.Log($"Activating power-up: {powerUpType} for {duration} seconds");
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.SpeedBoost:
                forwardSpeed *= 2;
                break;
            case PowerUp.PowerUpType.Invincibility:
                isInvincible = true;
                break;
            case PowerUp.PowerUpType.HighJump:
                jumpForce *= 2;
                break;
            case PowerUp.PowerUpType.DoubleCoins:
                doubleCoinsActive = true;
                break;
        }
        powerUpTimer = duration;
        powerUpActivationTime = Time.time; // Set the activation time
        Debug.Log($"Power-up timer set to: {powerUpTimer}");

        // Enable the power-up light
        if (powerUpLight != null)
        {
            powerUpLight.enabled = true;
        }
    }

    private void DeactivatePowerUp()
    {
        forwardSpeed = originalForwardSpeed;
        isInvincible = false;
        jumpForce = originalJumpForce;
        doubleCoinsActive = false;
        // Reset other power-up effects

        // Disable the power-up light
        if (powerUpLight != null)
        {
            powerUpLight.enabled = false;
        }
    }

    public bool IsDoubleCoinsActive()
    {
        return doubleCoinsActive;
    }

    private void UpdatePowerUpLight()
    {
        if (powerUpLight != null)
        {
            powerUpLight.color = new Color(1.0f, 0.84f, 0.0f);

            // Pulse faster as the timer gets lower
            float pulseSpeed = Mathf.Lerp(0.5f, 5f, 1 - (powerUpTimer / 5f)); // Adjust 5f to match the typical duration of your power-ups
            float elapsedTime = Time.time - powerUpActivationTime; // Calculate elapsed time since activation
            powerUpLight.intensity = Mathf.PingPong(elapsedTime * pulseSpeed, 3f) + 1f;
        }
    }
}