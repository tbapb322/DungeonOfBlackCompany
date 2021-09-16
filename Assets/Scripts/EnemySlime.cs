using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlime : Enemy
{   
    private bool inJump=false;
    private bool preparing = false;
    Vector2 jumpPosition;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyName = "Slime";
        MaxHealth = 20;
        MoveSpeed = 2f;
        HealhBarOffset = new Vector3(0, 0.5f, 0);
        AttackRadius = 5;
    }
    private void FixedUpdate()
    {
        if (!active)
            return;
            
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (inJump)
        {
            playerPosition = jumpPosition;
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15 && !preparing && !IsStunned)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, MoveSpeed * Time.deltaTime);
        }
        if (Vector2.Distance(playerPosition, transform.position) <= AttackRadius)
        {
            if (!inJump)
            {
                StartCoroutine(WaitForJump());
            }
        }
    }
    IEnumerator WaitForJump()
    {
        inJump = true;
        preparing = true;
        SetAnimatorSpeed(4);
        yield return new WaitForSeconds(2f);
        jumpPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        preparing = false;
        SetAnimatorSpeed(1);
        MoveSpeed = 30f;
        yield return new WaitForSeconds(2f);
        MoveSpeed = 2f;
        inJump = false;

    }
}
