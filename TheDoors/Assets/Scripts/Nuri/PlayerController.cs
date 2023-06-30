using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Transform rayStartPoint;

    private CharacterController controller;
    private Vector3 playerVelocity;
    //[SerializeField] private bool groundedPlayer;
    private InputManager inputManager;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
    }

    void Update()
    {


        //groundedPlayer = controller.isGrounded;
        if (OnGroundCheck() && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        controller.Move(move * Time.deltaTime * runSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && OnGroundCheck())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //ground control
    private bool OnGroundCheck()
    {
        bool hit = Physics.Raycast(rayStartPoint.position, -rayStartPoint.transform.up, 0.25f);
        Debug.DrawRay(rayStartPoint.position, -rayStartPoint.transform.up * 0.25f, Color.red);

        if (hit) { return true; }
        else { return false; }
    }

}
