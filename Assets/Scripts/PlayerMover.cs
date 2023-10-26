using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private readonly int JumpTrigger = Animator.StringToHash("Jump");
    private readonly int landingTrigger = Animator.StringToHash("IsLanding");
    private readonly int takingOffTrigger = Animator.StringToHash("IsTakingOff");

    private int isGroundedTrigger = Animator.StringToHash("IsGrounded");

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private int maxNumberOfJumps = 3;
    //[SerializeField] private int maxSpeed = 5;

    private new BatAudio audio;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private int currentNumberOfJumps = 3;

    private bool jumped = false;

    bool wasOnGround = false;

    private void Awake() {
        audio = GetComponent<BatAudio>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        isOnGround();
        if (Input.GetKeyDown(KeyCode.Space) && maxNumberOfJumps > 0) {
            jumped = true;
        }
    }

    void FixedUpdate()
    {
        if (jumped) {
            Jump();
            audio.PlayFlap();
            jumped = false;
        }
        //stoppingFriction();
        lookInDirection();
        //move();
        MoveTwo();
    }

    void lookInDirection() {
        //look in direction over time
        Vector3 direction = getDirection();
        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), .15f);
        } else {
            //transform.rotation = Quaternion.LookRotation(transform.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), .15f);
        }

    }

    private void MoveTwo()
    {
        Vector3 direction = getDirection();
        //set player velocity based on input direction, but don't touch y velocity since that affects jumps.
        playerRigidbody.velocity = new Vector3(direction.x * moveSpeed, playerRigidbody.velocity.y, direction.z * moveSpeed);
    }

    void Jump() {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            maxNumberOfJumps--;
            if (playerAnimator != null)
                playerAnimator.SetTrigger(JumpTrigger);
    }

    Vector3 getDirection() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        return direction;
    }

    void isOnGround() {
        //make 5 raycasts down from player to check if on ground
        //if any of them hit ground, isGrounded = true
        //else isGrounded = false
        if (Physics.Raycast(transform.position, Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(.25f, 0, 0), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(-.25f, 0, 0), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, .25f), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -.25f), Vector3.down, 1f)) {
            maxNumberOfJumps = currentNumberOfJumps;
            playerAnimator.SetBool(isGroundedTrigger, true);
            if (!wasOnGround) {
                playerAnimator.SetTrigger(landingTrigger);
                wasOnGround = true;
            }
        }
        else {
            playerAnimator.SetBool(isGroundedTrigger, false);
            if (wasOnGround) {
                playerAnimator.SetTrigger(takingOffTrigger);
                wasOnGround = false;
            }
        }
    }
}
