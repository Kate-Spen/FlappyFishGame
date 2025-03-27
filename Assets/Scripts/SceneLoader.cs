using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string MainGame;
    public void LoadGameScene()
    {
        SceneManager.LoadScene(MainGame);
    }
}
