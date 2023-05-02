using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 1f;
    public float bulletSpeed = 2f;

    public float turnSmoothness = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public bool isPlayer1 = false;

    public int playerLives = 4;
    public SpriteRenderer[] lifeBarSprites;

    public AudioClip collisionSound;
    public AudioClip shootSound;
    public AudioClip impactSound;
    public GameObject collisionEffectPrefab;
    private AudioSource audioSource;

    public float bulletCooldown = 0.5f;
    private float lastBulletTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayer1)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float targetRotation = transform.eulerAngles.z - horizontalInput * turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), turnSmoothness * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.W))
            {
                ShootBullet();
            }
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal2");
            float targetRotation = transform.eulerAngles.z - horizontalInput * turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), turnSmoothness * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShootBullet();
            }
        }

    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void ShootBullet()
    {
        if (Time.time - lastBulletTime > bulletCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            audioSource.PlayOneShot(shootSound);

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = transform.up * bulletSpeed;

            lastBulletTime = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleLeft"))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = collision.gameObject.transform.position.x + 25 * collision.collider.bounds.extents.x;
            transform.position = newPosition;
        }
        else if (collision.gameObject.CompareTag("ObstacleRight"))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = collision.gameObject.transform.position.x - 25 * collision.collider.bounds.extents.x;
            transform.position = newPosition;
        }
        else if (collision.gameObject.CompareTag("ObstacleTop"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = collision.gameObject.transform.position.y - 25 * collision.collider.bounds.extents.y;
            transform.position = newPosition;
        }
        else if (collision.gameObject.CompareTag("ObstacleBottom"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = collision.gameObject.transform.position.y + 25 * collision.collider.bounds.extents.y;
            transform.position = newPosition;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            UpdateLifeBar(collision);
        }
        else if (collision.gameObject.CompareTag("SpaceTrash"))
        {
            Destroy(collision.gameObject);
            UpdateLifeBar(collision);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            UpdateLifeBar(collision);
        }
    }

    public void UpdateLifeBar(Collision2D collision)
    {

        if (playerLives > 0 && playerLives <= 4)
        {
            audioSource.PlayOneShot(impactSound);
            lifeBarSprites[playerLives - 1].GetComponent<SpriteRenderer>().enabled = false;
            playerLives--;
        }
        else
        {
            audioSource.PlayOneShot(collisionSound);
            SpawnCollisionEffect(collision.contacts[0].point);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
            GameManager.Instance.GameOver(isPlayer1);
        }
    }

    public void SpawnCollisionEffect(Vector2 position)
    {
        GameObject effectInstance = Instantiate(collisionEffectPrefab, position, Quaternion.identity);
        Destroy(effectInstance, 1f);
    }
}
