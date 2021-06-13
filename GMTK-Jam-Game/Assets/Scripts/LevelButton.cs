using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _levelNum;

    public void StartLevel()
    {
        if (PlayerPrefs.GetInt(_levelNum.ToString()) == 1)
            SceneManager.LoadSceneAsync(_levelNum);
    }
}
