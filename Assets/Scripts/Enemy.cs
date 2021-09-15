using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy :MonoBehaviour
{
    public string enemyName;
    public int maxHealth=20;
    public int currentHealth=20;
    public bool isStunned = false;
    public float stunTime=2f;
    public Dictionary<string, float> resists;
    public SpriteRenderer sprite;
    public Animator animator;
    public Slider healthBar;
    public Vector3 healhBarOffset;
    public float moveSpeed = 1;
    public Vector2 attackRadius;

    public void GetStun()
    {
        isStunned = true;
        StartCoroutine(WaitForStunToEnd());
    }
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            animator.speed = 0;
            this.enabled = false;
            StartCoroutine(WaitForDeath());
        }
    }
    IEnumerator WaitForStunToEnd()
    {
        sprite.color = Color.black;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        sprite.color = Color.white;

    }
    IEnumerator WaitForDeath()
    {
        sprite.color = Color.white;
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(256, 256, 256, 0);
        yield return new WaitForSeconds(2f);
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);

    }
}
