using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

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
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
