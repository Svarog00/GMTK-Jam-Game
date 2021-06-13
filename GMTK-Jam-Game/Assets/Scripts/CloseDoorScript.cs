using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorScript : MonoBehaviour
{
    [SerializeField] private MonoButtonScript _activatorButton;
    [SerializeField] private SlimeType _activatorButtonType;

    private BoxCollider2D _boxCollider;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _activatorButton.OnButtonActivated += ActivatorButton_OnButtonActivated;
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void ActivatorButton_OnButtonActivated(object sender, MonoButtonScript.OnButtonActivatedEventArgs e)
    {
        if(e.senderType == _activatorButtonType)
        {
            Debug.Log($"Door closed {_activatorButtonType} & {this.ToString()}");
            Close();
        }
    }

    private void Close()
    {
        FindObjectOfType<AudioManager>().Play("Door");
        _animator.SetTrigger("Closed");
        Invoke("SetActiveFalse", 0.5f);
    }

    // Update is called once per frame
    private void SetActiveFalse()
    {
        _boxCollider.isTrigger = false;
    }
}
