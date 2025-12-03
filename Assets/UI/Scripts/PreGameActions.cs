using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameActions : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("AhmadSampleScene");
    }
}
