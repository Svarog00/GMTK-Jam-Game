using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
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
            Debug.Log($"Door opened {_activatorButtonType} & {this.ToString()}");
            Open();
        }
    }

    private void Open()
    {
        _boxCollider.isTrigger = true;
        _animator.SetTrigger("Opened");
        Invoke("SetActiveFalse", 0.5f);
    }

    // Update is called once per frame
    private void SetActiveFalse()
    {
        _boxCollider.isTrigger = true;
    }
}
