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
    public float friction;
    public bool grounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (jumpTimer > 0) {
            jumpTimer = jumpTimer - Time.deltaTime;
        }
        else if (dir.y != 0) {
            dir.y = 0;
        }
        if (dir.x == 0 && Mathf.Abs(rb.linearVelocityX) > 0.1f && grounded) {
             rb.linearVelocityX = rb.linearVelocityX / (1 + (friction/100));
             lastVelocity = rb.linearVelocityX;
        }
        else if (dir.x == 0 && !grounded) {
            rb.linearVelocityX = lastVelocity;
        }
        else if (grounded) {
            rb.linearVelocityX = dir.x * velocity;
        }
        if (dir.y == 1)
        rb.linearVelocityY = dir.y * jumpVelocity;
    }

    public void LeftRightMovement(InputAction.CallbackContext ctx) {
        dir.x = ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (!jumped){
            dir.y = 1f;
            jumpTimer = jumpDuration;
        }
        jumped = true;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            jumped = false;
            grounded = true;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            jumped = true;
            grounded = false;
        }
    }

    
}
