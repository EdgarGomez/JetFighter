using UnityEngine;

public class ObjectBoundary : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleRight") || collision.gameObject.CompareTag("ObstacleLeft"))
        {
            Destroy(gameObject);
        }
    }
}
