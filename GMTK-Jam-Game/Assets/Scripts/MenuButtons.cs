using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] menuButtons = new GameObject[3];
    [SerializeField] private GameObject[] levelButtons = new GameObject[6];
    public Sprite lockedBtn;
    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Levels()
    {
        foreach(GameObject btn in menuButtons)
        {
            btn.SetActive(false);
        }

        for(int i = 0; i < levelButtons.Length; i++)
        {
            if(i == levelButtons.Length - 1)
            {
                levelButtons[i].SetActive(true);
                continue;
            }

            if (PlayerPrefs.GetInt((i + 1).ToString()) == 0)
                levelButtons[i].GetComponent<Image>().sprite = lockedBtn;
            levelButtons[i].SetActive(true);
        }
    }

    public void Back()
    {
        foreach (GameObject btn in menuButtons)
        {
            btn.SetActive(true);
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
