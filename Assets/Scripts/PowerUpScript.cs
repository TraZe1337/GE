using System.Collections;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public enum PowerUpType { SlowTime, ExtraLife, IncreasePaddle }
    public PowerUpType powerUpType;
    public PlayerStats playerStats;
    public float dropSpeed = 2f;
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
        Debug.Log("hit");
        if (other.CompareTag("Paddle"))
        {
            Debug.Log("Power-up collected");
            switch (powerUpType)
            {
                case PowerUpType.SlowTime:
                    StartCoroutine(SlowTime());
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
        else
        {
            // Continue dropping if it hits anything else
            Debug.Log("Power-up hit something else, continuing to drop");
        }
    }
    
    public void IncreaseLife()
    {
        if (playerStats.Health < 3)
        {
            playerStats.Heal(1);
        }
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(30);
        Time.timeScale = 1f;
    }
    

    private void IncreasePaddleSize(Transform paddle)
    {
        paddle.localScale += new Vector3(0.5f, 0, 0);
    }
}