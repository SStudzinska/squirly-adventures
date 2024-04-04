using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player playerHealth;
    List<Hearts> hearts = new List<Hearts>();

    public int currentNuts = 0;
    public TMP_Text numberOfNuts;
    public static PlayerBar instance;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Player.OnPlayerDamaged += DrawHearts;
       
    }

    private void OnDisable()
    {
        Player.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
        numberOfNuts.text = currentNuts.ToString();

    }

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetheartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Hearts heartComponent = newHeart.GetComponent<Hearts>();
        heartComponent.SetheartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }


    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<Hearts>();
    }

    public void IncreaseNuts(int v)
    {
        currentNuts += v;
        numberOfNuts.text = currentNuts.ToString();
    }

    
}
