using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2 (Mathf.Sign(transform.position.x), 2);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
