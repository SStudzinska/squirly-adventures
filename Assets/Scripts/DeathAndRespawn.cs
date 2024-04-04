using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<Player>().health > 1)
            {
                player.transform.position = respawnPoint.position;
            }
            player.GetComponent<Player>().TakeDamage(2);
        }

    }
}
