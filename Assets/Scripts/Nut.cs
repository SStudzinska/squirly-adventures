using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nut : MonoBehaviour
{
    public int value = 1;
    public static event Action OnNutDestroyed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            OnNutDestroyed?.Invoke();
            PlayerBar.instance.IncreaseNuts(value);
        }
    }
}
