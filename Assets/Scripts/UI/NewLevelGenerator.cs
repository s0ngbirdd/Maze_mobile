using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelGenerator : MonoBehaviour
{
    public void GenerateNewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
