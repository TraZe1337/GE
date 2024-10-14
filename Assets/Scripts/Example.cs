using UnityEngine;

public class Example : MonoBehaviour
{
    public float speed = 3f;
    public GameObject child;
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float x = rotateX ? 1f : 0f;
        float y = rotateY ? 1f : 0f;
        float z = rotateZ ? 1f : 0f;

        Vector3 rot = new Vector3(x, y, z);
        transform.Rotate(rot * speed * Time.deltaTime);
    }
}
