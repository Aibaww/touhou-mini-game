using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject orbPrefab;

    // attack variables
    public float orbVelocity = 3f;
    public float shootDelay = 1f;
    public float shootTimer = 0.1f;
    public int orbSpray = 2;

    // movement variables
    public float moveSpeed = 2f;  
    public float moveRange = 3f;

    // health variables
    public float health = 20.0f;
    public int points = 100;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }


    void Update()
    {
        Move();
        MaybeShoot();
        if (health <= 0) {
            Die();
        }
    }

    void Move() {
        float x = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = new Vector3(initialPosition.x + x, transform.position.y, transform.position.z);
    }

    // check if the enemy should shoot
    void MaybeShoot() {
        if (Time.time >= shootTimer) {
            Shoot();
        }
    }

    void Shoot() {
            float angleOffset = 10f;
            float baseAngle = 180f;
            for (int i = -orbSpray; i <= orbSpray; i++) {
                float angle = baseAngle + (i * angleOffset);

                GameObject orb = Instantiate(orbPrefab, transform.position + new Vector3(i * 0.2f, -1f, 0), Quaternion.identity);

                Rigidbody2D orbRb = orb.GetComponent<Rigidbody2D>();

                orbRb.velocity = new Vector3( -Mathf.Sin(angle * Mathf.Deg2Rad) * orbVelocity, Mathf.Cos(angle * Mathf.Deg2Rad) * orbVelocity, 0);
            }

        shootTimer = Time.time + shootDelay;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Orb(Clone)"){
            health -= 1f;
        }
    }

    void Die() {
        Scorekeeper scorekeeper = FindObjectOfType<Scorekeeper>();
        scorekeeper.ScorePoints(points);

        Destroy(gameObject);
    }
}
