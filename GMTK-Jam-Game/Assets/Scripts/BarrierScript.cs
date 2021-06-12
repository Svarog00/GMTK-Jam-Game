using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    [SerializeField] private SlimeType _slimeType;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlueSlime") && _slimeType == SlimeType.Blue)
        {
            _boxCollider.isTrigger = true;
        }
        else if (collision.gameObject.CompareTag("RedSlime") && _slimeType == SlimeType.Red)
        {
            _boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueSlime") && _slimeType == SlimeType.Blue)
        {
            _boxCollider.isTrigger = false;
        }
        else if (collision.CompareTag("RedSlime") && _slimeType == SlimeType.Red)
        {
            _boxCollider.isTrigger = false;
        }
    }
}
