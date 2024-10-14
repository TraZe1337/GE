using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int hardness;
    private new Renderer renderer;

    void Start()
    {
        hardness = Random.Range(0, 3);
        renderer = GetComponent<Renderer>();
        UpdateColor();
        Debug.Log(hardness);
    }

    public void DecreaseHardness()
    {
        if (hardness > 0)
        {
            hardness -= 1;
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        switch (hardness)
        {
            case 2:
                renderer.material.color = Color.red;
                break;
            case 1:
                renderer.material.color = Color.yellow;
                break;
            case 0:
                renderer.material.color = Color.green;
                break;
            default:
                renderer.material.color = Color.white;
                break;
        }
    }
}