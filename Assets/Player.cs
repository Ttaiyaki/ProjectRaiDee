using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float movement;
    private float side;
    private bool isGround = true;
    public Rigidbody2D rb;
    public float jumpHeight;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = 0f;
        side = 0f;
        jumpHeight = 7f;
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current is not null){
            if (Keyboard.current.dKey.isPressed){
                // left walk
                movement = 1f;
                side = 0f;
                Debug.Log("Dkey");
            } else if (Keyboard.current.aKey.isPressed){
                // right walk
                movement = -1f;
                side = -180f;
                Debug.Log("Akey");
            } else {
                movement = 0f;
            }

            if (Keyboard.current.spaceKey.isPressed && isGround){
                jump();
                isGround = false;
            }
        } else {
            Debug.Log("no keyboard");
        }
    }

    private void FixedUpdate(){
        // change position
        Vector3 newPos = transform.position;
        Vector3 newSide = transform.eulerAngles;
        newPos.x = transform.position.x + 0.1f * movement * speed * Time.deltaTime;
        newSide.y = side;
        transform.position = newPos;
        transform.eulerAngles = newSide;
    }

    void jump(){
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Ground"){
            isGround = true;
        }
    }
}
