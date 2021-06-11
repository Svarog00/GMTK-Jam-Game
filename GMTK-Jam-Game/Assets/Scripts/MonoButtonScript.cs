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
    [SerializeField] private SlimeType _buttonType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueSlime") || collision.CompareTag("RedSlime"))
        {
            OnButtonActivated?.Invoke(this, new OnButtonActivatedEventArgs { senderType = _buttonType });
        }
    }
}
