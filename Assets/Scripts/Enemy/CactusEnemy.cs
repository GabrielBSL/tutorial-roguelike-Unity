using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusEnemy : Enemy
{
    [SerializeField] private float minimalDistanceToPlayer;
    [SerializeField] private float attackSpeed;

    private bool m_IsAttacking;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (m_IsAttacking || Player == null)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        if((transform.position - Player.position).sqrMagnitude >= Mathf.Pow(minimalDistanceToPlayer, 2))
        {
            Vector2 direction = (Player.position - transform.position).normalized * speed;
            rb2d.velocity = direction;
        }
        else
        {
            if(Time.time > attackDelayTimer)
            {
                attackDelayTimer = Time.time + attackDelay;
                StartCoroutine(ChargeAttack());
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    }

    IEnumerator ChargeAttack()
    {
        m_IsAttacking = true;

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = Player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            rb2d.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }

        m_IsAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            collision.gameObject.GetComponent<Entity>().ReceiveHit(damage);
        }
    }
}
