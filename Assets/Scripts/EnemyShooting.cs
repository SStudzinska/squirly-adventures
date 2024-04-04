using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int timeBetweenBullets;
    public int maxDistance;

    private GameObject player;
    private float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (player.transform.position.x > bulletPos.position.x)
        {
            // If player is on the right side of the bullet, flip the sprite
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            // If player is on the left side, revert sprite back to normal
            transform.localScale = new Vector3(1f, 1f, 1f);
        }



        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < maxDistance)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenBullets)
            {
                timer = 0;
                shoot();
            }
        }
        
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
