using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //public WeaponScript weapon;

    public bool playerOne;
    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }

    //private Player_Attack player_Attack;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //player_Healing = GetComponent<Player_Healing>();
        //player_Attack = GetComponent<Player_Attack>();
        CanAttack = true;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        //HealingInput();
        //MeleeAttackInput();
        //GunAttackInput();
    }

    void MovementInput()
    {
        if (playerOne)
        {
            playerMovement.HandleMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            playerMovement.HandleMove(Input.GetAxisRaw("HorizontalTwo"), Input.GetAxisRaw("VerticalTwo"));
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
