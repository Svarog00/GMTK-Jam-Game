using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //public WeaponScript weapon;

    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }

    //private Player_Attack player_Attack;
    [SerializeField] private bool playerOne = true;
    [SerializeField] private PlayerMovement playerOneMovement;
    [SerializeField] private PlayerMovement playerTwoMovement;
    // Start is called before the first frame update
    void Start()
    {
        //player_Healing = GetComponent<Player_Healing>();
        //player_Attack = GetComponent<Player_Attack>();
        CanAttack = true;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        SwitchSlimeInput();
        //HealingInput();
        //MeleeAttackInput();
        //GunAttackInput();
    }

    void SwitchSlimeInput()
    {
        if(Input.GetButtonDown("Switch"))
        {
            playerOne = !playerOne;
            playerOneMovement.HandleMove(0, 0);
            playerTwoMovement.HandleMove(0, 0);
        }
    }

    void MovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (playerOne)
        {
            playerOneMovement.HandleMove(x, y);
        }
        else
        {
            playerTwoMovement.HandleMove(x, y);
        }
    }

    /*
    void MeleeAttackInput()
    {
        if (Input.GetButtonDown("MeleeStrike") && CanAttack)
        {
            player_Attack.Melee();
        }
    }

    void GunAttackInput()
    {
        if (Input.GetButtonDown("GunFire") && CanAttack)
        {
            weapon.Attack();
        }
    }*/
}
