using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Vector3 velocity;
    public GameObject newBall;
    public GameObject powerUp;
    public AudioSource beep;
    public TMP_Text text;
    public TMP_Text score;
    public TMP_Text slowTimeText;
    public float scoreNumber;

    public HealthBarHUDTester healthBarHUDTester;
    public PlayerStats playerStats;

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
        normal.y = 0;
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

            if (GameObject.FindGameObjectsWithTag("Obstacle").Length == 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                if (Random.value <= 0.5f)
                {
                    Vector3 powerUpPosition =
                        new Vector3(obstacle.transform.position.x, 0.5f, obstacle.transform.position.z);
                    GameObject spawnedPowerUp = Instantiate(powerUp, powerUpPosition, Quaternion.identity);
                    PowerUpScript powerUpScript = spawnedPowerUp.GetComponent<PowerUpScript>();
                    powerUpScript.powerUpType = (PowerUpScript.PowerUpType)Random.Range(0, 3);
                    powerUpScript.playerStats = playerStats;
                    powerUpScript.ball = this;
                }
            }
        }
    }

    public void SlowTime()
    {
        StartCoroutine(ActivateSlowTime());
    }

    public IEnumerator ActivateSlowTime() {
        Time.timeScale = 0.5f;
        float slowTimeDuration = 10f;
        float elapsedTime = 0f;

        while (elapsedTime < slowTimeDuration)
        {
            slowTimeText.text = $"Slow Time: {slowTimeDuration - elapsedTime:F1}s";
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        slowTimeText.text = "";
        Time.timeScale = 1f;
    }
}