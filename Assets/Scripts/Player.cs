using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public Animator anim;
    public CinemachinePositionComposer cameraPositionComposer;
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
            Debug.Log("Falling");
            fallingTimer = fallingTimer + Time.fixedDeltaTime;
            if (!anim.GetBool("Falling") && fallingTimer > jumpDuration) anim.SetBool("Falling", true);
        }
        else{
            if (anim.GetBool("Falling")) anim.SetBool("Falling", false);
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
        if (dir.x == 0 && anim.GetBool("Walking")) {
            anim.SetBool("Walking", false);
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    public void LeftRightMovement(InputAction.CallbackContext ctx) {
        dir.x = ctx.ReadValue<float>();
        if (dir.x > 0) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.rotation = 0;
            anim.SetBool("Walking", true);
            anim.SetBool("Right", true);
        }
        else if (dir.x < 0) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.rotation = 0;
            anim.SetBool("Walking", true);
            anim.SetBool("Right", false);
        }
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (!jumped){
            jumpTimer = jumpDuration;
        }
        jumped = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitNormal = collision.contacts[0].normal;
        if (collision.gameObject.tag == "Ground") {
            Debug.Log("Collided with ground");
            jumped = false;
            grounded = true;
            jumpTimer = fallingTimer / bounce;
            fallingTimer = 0f;
        }
        if (collision.gameObject.tag == "Win") {
            SceneManager.LoadScene("Win");
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Debug.Log("Left Ground");
            jumped = true;
            grounded = false;
        }
    }

    
}
