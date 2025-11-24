using UnityEngine;

public class cameraRotator : MonoBehaviour
{
    [SerializeField] private float minSpeed = 30f;   // minimum rotation speed
    [SerializeField] private float maxSpeed = 130f;  // maximum rotation speed
    [SerializeField] private float interval = 10f;   // seconds between direction/speed swaps

    private float timer = 0f;
    private float currentSpeed;
    private int direction = 1; // 1 = forward, -1 = opposite

    void Start()
    {
        timer = 0f;
        direction = 1;
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.Rotate(Vector3.forward, Time.deltaTime * currentSpeed * direction);

        if (timer >= interval)
        {
            // flip direction and pick a new speed from the range
            direction = -direction;
            currentSpeed = Random.Range(minSpeed, maxSpeed);
            timer = 0f;
        }
    }
}
