using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed;
    float x;
    Vector2 move;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // get the player object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per fixed time step (default=0.02s)
    void FixedUpdate()
    {
        // get the x value
        x = Input.GetAxis("Horizontal");

        // use speed and x to calculate the new vector of movement
        move = new Vector2(x * speed, rb.velocity.y);

        // change player velocity to move right or left
        rb.velocity = move;
    }
}
