using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScripy : MonoBehaviour
{
    private int slimeCount;

    // Update is called once per frame
    void Update()
    {
        if(slimeCount == 2)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BlueSlime") || collision.CompareTag("RedSlime"))
        {
            slimeCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueSlime") || collision.CompareTag("RedSlime"))
        {
            slimeCount--;
        }
    }
}
