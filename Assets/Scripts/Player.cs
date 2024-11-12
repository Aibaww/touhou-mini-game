using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    private float speed = 5f;
    private float shootDelay = 0.1f;
    private float shootTimer = 0f;

    public float orbVelocity = 10f;


    public GameObject orbPrefab;

    // win screen
    public Image youWinImage;
    private Sprite youWinSprite;


    public Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        youWinSprite = Resources.Load<Sprite>("youwin");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        MaybeShoot();

        if (FindObjectOfType<Enemy>() == null)
        {
            youWinImage.sprite = youWinSprite;
            youWinImage.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    private void UpdatePosition() {
        float x = Input.GetAxisRaw("Horizontal");
        float dash = Input.GetAxisRaw("Dash");

        rb.velocity = new Vector3(x * speed, 0, 0);

        float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float clampedX = Mathf.Clamp(transform.position.x, -screenWidth, screenWidth);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void MaybeShoot() {
        if (Input.GetButton("Fire1") && Time.time >= shootTimer) {
            GameObject orb = Instantiate(orbPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            Rigidbody2D orbRb = orb.GetComponent<Rigidbody2D>();

            orbRb.velocity = new Vector3(0, orbVelocity, 0);
            shootTimer = Time.time + shootDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Enemy Orb(Clone)") {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.OnPlayerHit();
        }
    }

}
