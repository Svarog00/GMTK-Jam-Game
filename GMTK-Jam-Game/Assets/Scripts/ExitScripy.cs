using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScripy : MonoBehaviour
{
    private int _slimeCount;
    private Animator _animator;
    [SerializeField] private int _nextLevel;
    [SerializeField] private Animator _doorAnimator;

    [SerializeField] private List<GameObject> _slimes;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_slimeCount == 2)
        {
            _doorAnimator.SetTrigger("Opened");
            Invoke("Merge", 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BlueSlime") || collision.CompareTag("RedSlime"))
        {
            _slimeCount++;
            if(!_slimes.Find(x => x == collision.gameObject))
            {
                _slimes.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueSlime") || collision.CompareTag("RedSlime"))
        {
            _slimeCount--;
        }
    }

    void Merge()
    {
        foreach (GameObject gameObject in _slimes)
            gameObject.SetActive(false);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 1);
        _animator.SetTrigger("Merge");
        FindObjectOfType<AudioManager>().Play("SlimesMerge");
        Invoke("Transit", 3f);
    }

    void Transit()
    {
        SceneManager.LoadSceneAsync(_nextLevel);
    }
}
