using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlime : Enemy
{
    public Vector2 direction;
    public float timer;
    
    private Rigidbody2D rb;
    private bool inJump=false;
    private bool preparing = false;
    Vector2 jumpPosition;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f;
        enemyName = "Slime";
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.maxValue = maxHealth;
        healthBar.value = healthBar.maxValue;
        direction = new Vector2(5, 0);
        healhBarOffset = new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healhBarOffset);

        if (Input.GetKeyDown(KeyCode.Space))
            GetStun();
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetDamage(10);
            GetStun();
        }
    }
    private void FixedUpdate()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (inJump)
        {
            playerPosition = jumpPosition;
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 15 && !preparing && !isStunned)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        }
        if (Vector2.Distance(playerPosition, transform.position) <= 5)
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
        animator.speed = 4;
        yield return new WaitForSeconds(2f);
        jumpPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        preparing = false;
        animator.speed = 1;
        moveSpeed = 30f;
        yield return new WaitForSeconds(2f);
        moveSpeed = 2f;
        inJump = false;

    }
}
