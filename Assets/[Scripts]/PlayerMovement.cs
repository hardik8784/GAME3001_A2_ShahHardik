using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    [SerializeField]
    float walkSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 10.0f;
    [SerializeField]
    float jumpForce = 5.0f;

    //Components
    PlayerController playerController;

    //Movement Refrences
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isJumping)
        {
            return;
        }

        if (!(inputVector.magnitude > 0))
        {
            moveDirection = Vector3.zero;
        }

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        print("inputVector : " + inputVector);
    }
    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
    }
}
