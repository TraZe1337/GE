using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovementNeu : MonoBehaviour
{
    public float speed = 11f;
    public Transform floor;
    public Transform paddle;

    private Vector2 moveDir;


    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        float maxX = floor.localScale.x * 10f * 0.5f - paddle.localScale.x * 0.5f;
        float posX = transform.position.x + moveDir.x * speed * Time.deltaTime;
        float clampedX = Mathf.Clamp(posX, -maxX, maxX);
        
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
