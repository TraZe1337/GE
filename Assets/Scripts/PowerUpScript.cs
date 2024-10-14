using System.Collections;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public enum PowerUpType { SlowTime, ExtraLife, IncreasePaddle }
    public PowerUpType powerUpType;
    public PlayerStats playerStats;
    public float dropSpeed = 2f;
    public Ball ball;
    private new Renderer renderer;
    
    
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        SetColor();
    }
    
    private void SetColor()
    {
        switch (powerUpType)
        {
            case PowerUpType.SlowTime:
                renderer.material.color = Color.black;
                break;
            case PowerUpType.ExtraLife:
                renderer.material.color = Color.red;
                break;
            case PowerUpType.IncreasePaddle:
                renderer.material.color = Color.green;
                break;
        }
    }

    
    private void Update()
    {
        // Set the y position to 0.5
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    
        // Move the power-up downwards along the z-axis
        transform.position += Vector3.back * dropSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            switch (powerUpType)
            {
                case PowerUpType.SlowTime:
                    ball.SlowTime();
                    break;
                case PowerUpType.ExtraLife:
                    IncreaseLife();
                    break;
                case PowerUpType.IncreasePaddle:
                    IncreasePaddleSize(other.transform);
                    break;
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("BottomWall"))
        {
            Destroy(gameObject);
        }
    }
    
    public void IncreaseLife()
    {
        if (playerStats.Health < 3)
        {
            playerStats.Heal(1);
        }
    }
    

    private void IncreasePaddleSize(Transform paddle)
    {
        paddle.localScale += new Vector3(0.5f, 0, 0);
    }
}