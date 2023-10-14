using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 300f;

    private Rigidbody playerRigidbody;
    private bool isGrounded;

    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Vector3 movementVector = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * Vector3.right   //horizontal movement
                                + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime *  Vector3.forward; //in and out movement

        //get the vector of the direction of the player is moving
        Vector3 directionVector = ((transform.position + movementVector) - transform.position).normalized;

        transform.position += movementVector;

        //if moving, slowly rotate towards the direction player is moving 
        float rotateSpeed = 10f;
        if(directionVector != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(directionVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        //jumping
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isGrounded){
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isGrounded = false;
            }
        }

    }

    private void OnCollisionEnter(Collision other) {
        isGrounded = true;
    }
}
