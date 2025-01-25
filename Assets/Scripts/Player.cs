using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 dir;
    public bool jumped = false;

    public float velocity;
    public float lastVelocity;
    public float jumpVelocity;
    public float jumpDuration;

    public float jumpTimer = 0f;
    public float fallingTimer = 0f;
    public float bounceThreshold;
    public float friction;
    public float bounce;
    public bool grounded = true;
    public Vector2 hitNormal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (jumpTimer > bounceThreshold) {
            Debug.Log("Jumped or bounced");
            jumpTimer = jumpTimer - Time.fixedDeltaTime;
            rb.linearVelocity = new Vector2(rb.linearVelocityX + hitNormal.x * jumpVelocity, hitNormal.y * jumpVelocity);
        }
        else if (dir.y != 0) {
            dir.y = 0;
        }
        if (rb.linearVelocityY < 0){
            fallingTimer = fallingTimer + Time.fixedDeltaTime;
        }
        //If you are on the ground, and not inputting anything, and going fast enough, just slow down.
        if (dir.x == 0 && Mathf.Abs(rb.linearVelocityX) > 0.1f && grounded) {
             rb.linearVelocityX = rb.linearVelocityX / (1 + (friction/100));
             lastVelocity = rb.linearVelocityX;
        }
        //If you are n
        else if (!grounded) {
            rb.linearVelocityX = lastVelocity;
        }
        else if (grounded) {
            rb.linearVelocityX = dir.x * velocity;
            lastVelocity = rb.linearVelocityX;
        }
    }

    public void LeftRightMovement(InputAction.CallbackContext ctx) {
        dir.x = ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (!jumped){
            jumpTimer = jumpDuration;
        }
        jumped = true;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        hitNormal = collision.contacts[0].normal;
        if (collision.gameObject.tag == "Ground") {
            jumped = false;
            grounded = true;
            Debug.Log("FallingTimer was: " + fallingTimer);
            jumpTimer = fallingTimer / bounce;
            Debug.Log("JumpTimer is: " + jumpTimer);
            Debug.Log("Compared to threshold: " + bounceThreshold);
            fallingTimer = 0f;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            jumped = true;
            grounded = false;
        }
    }

    
}
