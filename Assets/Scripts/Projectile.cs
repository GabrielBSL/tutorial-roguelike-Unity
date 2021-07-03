using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private IEnumerator projectileCoroutine;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DeactivateProjectile(float lifeTime)
    {
        if(projectileCoroutine != null)
        {
            StopCoroutine(projectileCoroutine);
        }

        projectileCoroutine = DeactivateInTime(lifeTime);
        StartCoroutine(projectileCoroutine);
    }

    IEnumerator DeactivateInTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.Enemy.ToString()))
        {
            collision.GetComponent<Entity>().ReceiveHit(damage);
            gameObject.SetActive(false);
        }
    }
}
