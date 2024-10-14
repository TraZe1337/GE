using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovementNeu : MonoBehaviour
{
    public float speed = 11f;
    public Transform floor;
    public Transform paddle;

    private Vector2 moveDir;
    private bool isGamePaused = true;

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0f;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();

        // Unpause the game when the paddle is moved for the first time
        if (isGamePaused && moveDir != Vector2.zero)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

    void Update()
    {
        if (!isGamePaused)
        {
            float maxX = floor.localScale.x * 10f * 0.5f - paddle.localScale.x * 0.5f;
            float posX = transform.position.x + moveDir.x * speed * Time.deltaTime;
            float clampedX = Mathf.Clamp(posX, -maxX, maxX);

            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}