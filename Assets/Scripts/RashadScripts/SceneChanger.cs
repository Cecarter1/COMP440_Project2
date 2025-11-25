using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
