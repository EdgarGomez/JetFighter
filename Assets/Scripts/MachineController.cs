using UnityEngine;

public class MachineController : MonoBehaviour
{
    public float speed = 3f; // Set the machine's movement speed
    public float turnSpeed = 3f; // Set the machine's turn speed
    public float shootingInterval = 2f; // Set the interval at which the machine shoots

    public GameObject bulletPrefab; // Drag the bullet prefab in the Inspector
    public Transform bulletSpawnPoint; // Drag the bullet spawn point in the Inspector

    private float nextShootTime; // Time at which the machine can shoot again

    private Vector2 initialPosition; // Machine's initial position
    private float nextTurnTime; // Time at which the machine will turn again
    private Vector2 turnDirection; // Direction in which the machine will turn
    private Vector2 currentDirection; // Machine's current direction of movement

    void Start()
    {
        initialPosition = transform.position;
        currentDirection = Vector2.up;
        nextTurnTime = Time.time + Random.Range(1f, 3f);
    }

    void Update()
    {
        /*if (GameManager.Instance.isGameOver)
        {
            return; // don't allow any movement input while game is over
        }*/

        // Move the machine forward
        transform.Translate(currentDirection * speed * Time.deltaTime);

        // Check if the machine has hit the screen edges
        if (transform.position.x < -300 || transform.position.x > 300 || transform.position.y < -300 || transform.position.y > 300)
        {
            // If so, reset its position and direction
            transform.position = initialPosition;
            currentDirection = Vector2.up;
            nextTurnTime = Time.time + Random.Range(1f, 3f);
        }

        // Check if it's time to turn
        if (Time.time > nextTurnTime)
        {
            // If so, pick a random direction to turn
            turnDirection = Quaternion.Euler(0, 0, Random.Range(-90f, 90f)) * currentDirection;
            nextTurnTime = Time.time + Random.Range(1f, 3f);
        }

        // Rotate the machine towards its current direction
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Check if it's time to shoot
        if (Time.time > nextShootTime)
        {
            // If so, spawn a bullet and set the next shooting time
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            nextShootTime = Time.time + shootingInterval;
        }

        // Update the machine's current direction of movement towards its turn direction
        currentDirection = Vector2.Lerp(currentDirection, turnDirection, turnSpeed * Time.deltaTime);
    }
}
