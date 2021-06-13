using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	public bool CanAttack { get; set; }
	public bool CanMove { get; set; }

	[SerializeField] private bool playerOne = true;
	[SerializeField] private PlayerMovement playerOneMovement;
	[SerializeField] private PlayerMovement playerTwoMovement;

	[SerializeField] private GameObject[] lightSource = new GameObject[2];

	private bool playerTwoOn = false;
	// Start is called before the first frame update
	void Start()
	{
		CanAttack = true;
		CanMove = true;
		playerTwoOn = bool.Parse(PlayerPrefs.GetString("EnablePlayerTwo"));
		lightSource[0].SetActive(playerOne);
		lightSource[1].SetActive(playerTwoOn);
	}

	// Update is called once per frame
	void Update()
	{
		MovementInput();
		SwitchSlimeInput();
		EnablePlayerTwo();
		ExitInput();
	}

    private void ExitInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
			SceneManager.LoadSceneAsync(0);
        }
    }

    void EnablePlayerTwo()
	{
		if (Input.GetButtonDown("EnablePlayerTwo"))
		{
			playerTwoMovement.HandleMove(0, 0);
			playerTwoOn = !playerTwoOn;
			PlayerPrefs.SetString("EnablePlayerTwo", playerTwoOn.ToString());
			playerOne = true;
			ToggleRedLight();
		}
	}

	void SwitchSlimeInput()
	{
		if(Input.GetButtonDown("Switch") && !playerTwoOn)
		{
			playerOne = !playerOne;
			playerOneMovement.HandleMove(0, 0);
			playerTwoMovement.HandleMove(0, 0);
			CheckLight();
		}
	}

	void MovementInput()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		float x2 = Input.GetAxisRaw("HorizontalTwo");
		float y2 = Input.GetAxisRaw("VerticalTwo");

		if (playerOne)
		{
			playerOneMovement.HandleMove(x, y);
		}
		else
		{
			playerTwoMovement.HandleMove(x, y);
		}

		if(playerTwoOn)
		{
			playerTwoMovement.HandleMove(x2, y2);
		}
	}

	void CheckLight()
	{
		lightSource[0].SetActive(playerOne);
		lightSource[1].SetActive(!playerOne);
	}

	void ToggleRedLight()
	{
		lightSource[0].SetActive(playerOne);
		lightSource[1].SetActive(playerTwoOn);
	}
}
