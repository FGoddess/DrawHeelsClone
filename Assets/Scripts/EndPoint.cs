using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
