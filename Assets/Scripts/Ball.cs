using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 velocity;
    public GameObject newBall;
    public GameObject powerUp;
    public AudioSource beep;
    public TMP_Text text;
    public TMP_Text score;
    public float scoreNumber = 0;

    public HealthBarHUDTester healthBarHUDTester;
    public PlayerStats playerStats;


    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            bool isSide = other.gameObject.name == "Left" || other.gameObject.name == "Right";
            velocity = isSide
                ? new Vector3(-velocity.x, velocity.y, velocity.z)
                : new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("Paddle"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("BottomWall"))
        {
            Vector3 position = Vector3.zero;
            healthBarHUDTester.Hurt(1);
            if (playerStats.Health <= 0f)
            {
                text.text = "Game over";
            }
            else
            {
                Instantiate(newBall, position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(other);
        }

        beep.Play();
    }

    private void HandleObstacleCollision(Collider obstacle)
    {
        Vector3 collisionPoint = GetComponent<Collider>().ClosestPoint(obstacle.transform.position);
        Vector3 normal = (collisionPoint - obstacle.transform.position).normalized;
        normal.y = 0; // Ignore the y-component to prevent changes in the y-axis
        velocity = Vector3.Reflect(velocity, normal);
        Obstacle obstacleScript = obstacle.gameObject.GetComponent<Obstacle>();
        if (obstacleScript.hardness > 0)
        {
            obstacleScript.DecreaseHardness();
        }
        else
        {
            Destroy(obstacle.gameObject);
            scoreNumber += 10;
            score.text = "Score: " + scoreNumber;

            if (Random.value <= 1f )
            {
                Debug.Log("41241");
                Vector3 powerUpPosition = new Vector3(obstacle.transform.position.x, 0.5f, obstacle.transform.position.z);
                GameObject spawnedPowerUp = Instantiate(powerUp, powerUpPosition, Quaternion.identity);
                PowerUpScript powerUpScript = spawnedPowerUp.GetComponent<PowerUpScript>();
                powerUpScript.powerUpType = (PowerUpScript.PowerUpType)Random.Range(0, 3);
                powerUpScript.playerStats = playerStats; // Set the ball reference
            }
        }
    }
}