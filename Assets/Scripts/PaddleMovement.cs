using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 11f;
    public Transform floor;
    public Transform paddle;

    // Update is called once per frame
    void Update()
    {
        float maxX = floor.localScale.x * 10f * 0.5f - paddle.localScale.x * 0.5f;
        
        float dir = Input.GetAxis("Horizontal");
        Debug.Log(dir + " dir");
        float posX = transform.position.x + dir * speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(posX, -maxX, maxX);
        Debug.Log(posX + " posX");
        Debug.Log(clampedX + " clampedX");

        
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
