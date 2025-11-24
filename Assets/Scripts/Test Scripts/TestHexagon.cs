using UnityEngine;

public class TestHexagon : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shrinkSpeed = 3f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.localScale = Vector3.one * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        if(transform.localScale.x <= 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
