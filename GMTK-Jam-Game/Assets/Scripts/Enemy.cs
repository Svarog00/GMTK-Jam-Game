﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Patroling, Chasing, Attacking };

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour, IDamagable
{
    [Header("Enemy base")]
    public Animator animator;
    [SerializeField] private float stunTime;
    private float _curStunTime = 0f;
    private bool _canMove = true;
    [SerializeField] private EnemyStates currentState;
    [SerializeField] private SlimeType _slimeType;

    [Header("Player relation")]
    public LayerMask layerMask;
    private bool playerDetected;
    private Transform center;
    protected GameObject Player;

    public float attackDistance; //Дистанция на которой может совершить атаку
    public float agressionDistance; //Дистанция на которой происходит агр
    public float chill = 0; //pause between attacks

    protected Rigidbody2D playerPosition;
    protected Rigidbody2D enemyPosition;

    private bool _faceRight;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _chillTime = 1f;

    protected Vector2 direction;
    protected bool _canAttack;
    protected Vector2 _movement;
    protected Vector2 _headingToPlayer;
    protected float _distanceToPlayer;

    [Header("Stats")]
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected int _hpMax;
    [SerializeField] protected int _hp;
    protected float _currentSpeed;

    [Header("Patrol")]
    public Transform[] patrolSpots;
    private int _randomSpot;
    [SerializeField] private float _waitTime = 0f;
    private float _curWaitTime;
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyStates.Patroling;
        enemyPosition = GetComponent<Rigidbody2D>();
        center = GetComponent<Transform>();
        _canAttack = false;
        _randomSpot = Random.Range(0, patrolSpots.Length);
    }

    void OnEnable()
    {
        _hp = _hpMax;
    }

    // Update is called once per frame
    protected void Update()
    {
        //Если игрока рядом нет, то патрулирует территорию по рандомным точкам
        //Дойдя до точки запускается таймер отдиха. Когда таймер кончается - выбирается новая рандомная точка
        if (currentState == EnemyStates.Patroling)
        {
            Patrol();
            ScanArea();
        }

        if (playerDetected)
        {
            ReactOnPlayer();
        }

        if (_curStunTime > 0f)
            _curStunTime -= Time.deltaTime;
        else
            _canMove = true;

        animator.SetFloat("Speed", _currentSpeed); //В зависимости от скорости активировать анимации
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    private void EstimateDistance()
    {
        _headingToPlayer = enemyPosition.position - playerPosition.position; //направленный вектор к игроку / a vector to the player
        _distanceToPlayer = _headingToPlayer.magnitude; //длина вектора / lenght of the vector
        direction = _headingToPlayer / _distanceToPlayer; //direction to player
    }

    private void ScanArea()
    {
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center.position, agressionDistance, layerMask); //find the player in circle
        foreach (Collider2D enemy in detectedEnemies)
        {
            if (((enemy.tag == "BlueSlime" && _slimeType == SlimeType.Blue) || (enemy.tag == "RedSlime" && _slimeType == SlimeType.Red))
                && !playerDetected) //если произошел агр, то заполняем ссылки на игрока
            {
                if (Player == null)
                {
                    Player = enemy.gameObject;
                    playerPosition = Player.GetComponent<Rigidbody2D>();
                }
                currentState = EnemyStates.Chasing;
                playerDetected = true;
                break;
            }
        }
    }

    private void ReactOnPlayer()
    {
        EstimateDistance();//Рассчет дистанции для дальнейших действий
        if (currentState == EnemyStates.Chasing)
        {
            //Поворот спрайта в зависимости от положения игрока
            if (playerPosition.position.x < gameObject.transform.position.x && _faceRight == true)
            {
                Flip();
            }
            if (playerPosition.transform.position.x > gameObject.transform.position.x && _faceRight == false)
            {
                Flip();
            }
            Chase(); //Go to the enemy if he is too close
                     //конец поведение врага по отношению к игроку

            if (_distanceToPlayer > agressionDistance)
            {
                currentState = EnemyStates.Patroling;
                playerDetected = false;
            }
        }
    }

    private void Patrol()
    {
        if (patrolSpots.Length > 0)
        {
            _currentSpeed = _maxSpeed;
            transform.position = Vector2.MoveTowards(transform.position, patrolSpots[_randomSpot].position, _currentSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolSpots[_randomSpot].position) <= 0.2f)
            {
                _currentSpeed = 0;
                if (_curWaitTime <= 0f)
                {
                    _curWaitTime = _waitTime;
                    _randomSpot = Random.Range(0, patrolSpots.Length);
                    _currentSpeed = _maxSpeed;
                }
                else _curWaitTime -= Time.deltaTime;
            }
        }
        else
            _currentSpeed = 0f;
    }

    void Move()
    {
        if(_canMove)
        {
            enemyPosition.MovePosition(enemyPosition.position - direction * _currentSpeed * Time.deltaTime); //movement
        }
    }

    protected void Chase()
    {
        _currentSpeed = _maxSpeed; //Увеличиваем скорость до максимальной
        if (_distanceToPlayer <= attackDistance) //Если игрок слишком близко, то остановиться для атаки
        {
            _currentSpeed = 0f;
            if (chill <= 0 && _canAttack) //Если является атакующим и паузка кончилась, то атака
            {
                Attack();
            }
        }
        else
        {
            Leave();
        }
    }

    protected virtual void Attack() { }

    protected void Leave()
    {
        if (_canAttack)
        {
            _canAttack = false;
        }
    }

    public virtual void Hurt(int damage) //get damage from player or another entity
    {
        FindObjectOfType<AudioManager>().Play("EnemyHurt");
        _hp -= damage;

        if (_canMove)
        {
            _curStunTime = stunTime;
            _canMove = false;
        }

        if (_hp <= 0)
        {
            FindObjectOfType<AudioManager>().Play(this.ToString());
            Leave(); //Don't touch the player
            gameObject.SetActive(false);
        }
    }

    protected void Flip() //turn left or right depends on player position
    {
        _faceRight = !_faceRight;
        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    protected IEnumerator Cooldown()
    {
        while (true)
        {
            chill -= Time.deltaTime;
            if (chill <= 0)
            {
                yield break;
            }
            yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, agressionDistance);
    }
}
