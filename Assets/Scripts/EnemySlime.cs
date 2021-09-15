using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlime : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        enemyName = "Slime";
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthBar.maxValue = maxHealth;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetStun();
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDamage(10);
            GetStun();
        }


    }
}
