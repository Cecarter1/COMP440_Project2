using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("SPACE PRESSED!");

        if (Input.GetKeyDown(KeyCode.K))
            Debug.Log("K PRESSED!");
    }
}
