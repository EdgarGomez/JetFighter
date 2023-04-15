using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleLeft") ||
            collision.gameObject.CompareTag("ObstacleRight") ||
            collision.gameObject.CompareTag("ObstacleTop") ||
            collision.gameObject.CompareTag("ObstacleBottom") ||
            collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the bullet when it collides with a specific object
        }
    }
}