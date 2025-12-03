using UnityEngine;

public class TitleMover : MonoBehaviour
{
    public float speed = 2f;
    public float amplitude = 25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(x, 0, 0);
    }
}
