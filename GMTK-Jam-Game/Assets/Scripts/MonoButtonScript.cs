using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeType { Red, Blue }

public class MonoButtonScript : MonoBehaviour
{
    public event EventHandler<OnButtonActivatedEventArgs> OnButtonActivated;
    public class OnButtonActivatedEventArgs : EventArgs
    {
        public SlimeType senderType;
    }

    [SerializeField] private bool openDoorRed;
    [SerializeField] private bool openDoorBlue;

    private SlimeType _buttonType;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("BlueSlime"))
        {
            _animator.SetTrigger("BlueStand");
            _buttonType = SlimeType.Blue;
            OnButtonActivated?.Invoke(this, new OnButtonActivatedEventArgs { senderType = _buttonType });
        }
        else if (collision.CompareTag("RedSlime"))
        {
            _animator.SetTrigger("RedStand");
            _buttonType = SlimeType.Red;
            OnButtonActivated?.Invoke(this, new OnButtonActivatedEventArgs { senderType = _buttonType });
        }
    }
}
