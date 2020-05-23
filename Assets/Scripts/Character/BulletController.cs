using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;
    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + GetComponent<Rigidbody2D>().velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }

            if (shooter.CompareTag("Player") && other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<CharacterController>();
                enemy.TakeDamage(shooter.GetComponent<CharacterController>().damage);
                Destroy(gameObject);
            }

            if (shooter.CompareTag("Enemy") && other.CompareTag("Player"))
            {
                var player = other.GetComponent<CharacterController>();
                player.TakeDamage(shooter.GetComponent<CharacterController>().damage);
                Destroy(gameObject);
            }
        }
    }
}
