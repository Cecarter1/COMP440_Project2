using UnityEngine;

public class PlayerHitHandler : MonoBehaviour
{
    public HUDManager hud;

    void Update()
    {
        // TEST ONLY: press G to force Game Over
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G pressed – forcing Game Over");
            if (hud != null)
                hud.GameOver();
        }
    }

    // later you can call this from collisions
    public void Hit()
    {
        Debug.Log("PlayerHitHandler.Hit() called – Game Over");
        if (hud != null)
            hud.GameOver();
    }

    // if this script is on the player and obstacles use the "Obstacle" tag,
    // this will automatically trigger Game Over on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Hit();
        }
    }
}
