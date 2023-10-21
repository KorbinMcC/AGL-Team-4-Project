using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private readonly int JumpTrigger = Animator.StringToHash("Jump");

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private int maxNumberOfJumps = 3;

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private bool isGrounded;
    private int currentNumberOfJumps = 0;

    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 movementVector = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * Vector3.right   //horizontal movement
                                + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime *  Vector3.forward; //in and out movement

        //get the vector of the direction of the player is moving
        Vector3 directionVector = ((transform.position + movementVector) - transform.position).normalized;

        Run();

        //transform.position += movementVector;

        //if moving, slowly rotate towards the direction player is moving 
        float rotateSpeed = 10f;
        if(directionVector != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(directionVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        } else {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        //jumping
        if(Input.GetKeyDown(KeyCode.Space)){
            if(currentNumberOfJumps > 0){
                playerAnimator.SetTrigger(JumpTrigger);
                currentNumberOfJumps--;
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isGrounded = false;
            }
        }

        if(isGrounded) { 
            currentNumberOfJumps = maxNumberOfJumps;
        }

    }

    private void Run()
    {
        Vector3 playerVelocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        playerRigidbody.velocity = playerVelocity;
    }


    private void OnCollisionEnter(Collision other) {
        isGrounded = true;
    }
}
