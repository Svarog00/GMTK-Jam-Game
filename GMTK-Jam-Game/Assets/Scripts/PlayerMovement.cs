using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Animator animator;

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
        /*_movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");*/
        _movement.x = x;
        _movement.y = y;

       /* AnimateMove();

        if (_movement != new Vector2(0, 0))
        {
            ChangeDir();
        }*/
    }


   /* private void AnimateMove()
    {
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void ChangeDir()
    {
        _direction = _movement;
        animator.SetFloat("Dir_Horizontal", _direction.x);
        animator.SetFloat("Dir_Vertical", _direction.y);
    }*/


}
