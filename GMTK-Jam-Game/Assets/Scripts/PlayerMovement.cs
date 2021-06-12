using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2;
    private Animator animator;

    public float movementSpeed = 5f;

    private Vector2 _movement;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb2.MovePosition(rb2.position + _movement * movementSpeed * Time.deltaTime);
    }

    public void HandleMove(float x, float y)
    {
        //Get input to move
        _movement.x = x;
        _movement.y = y;
        AnimateMove();
    }


    private void AnimateMove()
    {
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
}
