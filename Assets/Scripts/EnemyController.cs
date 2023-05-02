using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float minScale = 0.1f;
    public float maxScale = 1f;
    public float scaleSpeed = 0.5f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float shootInterval = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float scale = 0f;
    private bool isMoving = false;
    private Vector2 moveDirection;
    private float shootTimer = 0f;
    public float bulletSpeed = 5f;

    Quaternion randomRotation = Quaternion.Euler(0f, 0f, 0f);

    void Awake()
    {
        randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }
    void Start()
    {
        transform.localScale = new Vector3(minScale, minScale, 1f);

        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        if (scale < maxScale)
        {
            scale += Time.deltaTime * scaleSpeed;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
        else if (!isMoving)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;




            transform.rotation = Quaternion.Slerp(transform.rotation, randomRotation, rotationSpeed * Time.deltaTime);

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootBullet(new Vector2(1, 1));
                shootTimer = 0f;
            }
        }
    }

    public void ShootBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleRight") ||
        collision.gameObject.CompareTag("ObstacleLeft") ||
        collision.gameObject.CompareTag("ObstacleTop") ||
        collision.gameObject.CompareTag("ObstacleBottom") ||
        collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
