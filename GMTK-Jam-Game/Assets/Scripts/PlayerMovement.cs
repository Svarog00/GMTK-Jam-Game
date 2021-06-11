using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Animator animator;

    public LayerMask ObstacleLayer;

    public int maxDashCount;

    public float dashForce;
    public float dashTimer;
    public float dashDistance;

    public float movementSpeed = 5f;

    private int _curDashCounter;
    private float _curDashTimer = 0f;

    private Vector2 _movement;
    private Vector2 _direction;
    private Vector3 _dashTarget;

    private bool _isDashing;
    private bool _isPressedDash;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ObstacleLayer = LayerMask.GetMask("Obstacles");
        _curDashCounter = maxDashCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_curDashTimer > 0f)
        {
            _curDashTimer -= Time.deltaTime;
            if (_curDashTimer <= 0f)
                _curDashCounter = maxDashCount;
        }
    }

    private void FixedUpdate()
    {
        rb2.MovePosition(rb2.position + _movement * movementSpeed * Time.deltaTime);
        Dash();
    }

    public void HandleMove(float x, float y, bool dash)
    {
        //Get input to move
        /*_movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");*/
        _movement.x = x;
        _movement.y = y;

        AnimateMove();

        if (_movement != new Vector2(0, 0))
        {
            ChangeDir();
        }

        if (dash && _curDashCounter > 0) //if timer is null
        {
            //Update timer
            //minus one dash
            _curDashTimer = dashTimer;
            _curDashCounter--;
            _isPressedDash = true;
            FindObjectOfType<AudioManager>().Play("PlayerDashSound");
        }
    }

    private void Dash()
    {
        if (_isPressedDash)
        {
            Dash(dashDistance, _direction);

            if (_isDashing)
            {
                rb2.velocity = Vector2.zero;
                float distSqr = (_dashTarget - transform.position).sqrMagnitude; //��������� �� ����� �����������

                if (distSqr < 0.1) //���� ��������� ������� ����, �� �� ����������
                {
                    _isDashing = false;
                    _dashTarget = Vector3.zero;
                }
                else
                {
                    rb2.MovePosition(Vector3.Lerp(transform.position, _dashTarget, dashForce * Time.deltaTime)); //��� � �����
                }
            }
            CameraShake.Instance.ShakeCamera(1f, .1f);
            _isPressedDash = false;
        }
    }

    private void Dash(float dashDistance, Vector2 direction)
    {
        _isDashing = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dashDistance, ObstacleLayer); //������� ����� �� ��������� ���� �� ���� �����������
        if (hit)
        {
            _dashTarget = transform.position + (Vector3)((direction * dashDistance) * hit.fraction); //���� ����� ���, �� ����� ���������� - ����� ���������
        }
        else
        {
            _dashTarget = transform.position + (Vector3)(direction * dashDistance); //���� ��� ������ �� ������, �� ����������� �� ������ ���������
        }

        rb2.velocity = Vector2.zero;
    }

    private void AnimateMove()
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
    }


}
