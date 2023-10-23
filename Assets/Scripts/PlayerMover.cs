using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private readonly int JumpTrigger = Animator.StringToHash("Jump");

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private int maxNumberOfJumps = 3;
    [SerializeField] private int maxSpeed = 5;

    private new BatAudio audio;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private int currentNumberOfJumps = 3;

    private bool jumped = false;

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
        isOnGround();
        if (jumped) {
            Jump();
            audio.PlayFlap();
            jumped = false;
        }
        stoppingFriction();
        lookInDirection();
        move();
    }

    void stoppingFriction() {
        //while no inputs, slow down player
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
            playerRigidbody.velocity *= .9f;
        }

        if (playerRigidbody.velocity.magnitude < .1f) {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    void lookInDirection() {
        //look in direction over time
        Vector3 direction = getDirection();
        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), .15f);
        }

    }

    void move() {
        Vector3 direction = getDirection();
        if (direction != Vector3.zero) {
            playerRigidbody.AddForce(direction * moveSpeed);
            //clamp movement speed
            if (playerRigidbody.velocity.magnitude > maxSpeed) {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
            }

        }
    }

    void Jump() {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            maxNumberOfJumps--;
            if (playerAnimator != null)
                playerAnimator.SetTrigger(JumpTrigger);
    }

    Vector3 getDirection() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        return direction;
    }

    void isOnGround() {
        //make 5 raycasts down from player to check if on ground
        //if any of them hit ground, isGrounded = true
        //else isGrounded = false
        if (Physics.Raycast(transform.position, Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(.5f, 0, 0), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(-.5f, 0, 0), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, .5f), Vector3.down, 1f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0, -.5f), Vector3.down, 1f)) {
            maxNumberOfJumps = currentNumberOfJumps;
        }
    }
}
