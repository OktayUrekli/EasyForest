
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelController : MonoBehaviour
{
    public void ToTheForest()
    {
        SceneManager.LoadScene(1);
    }

    public void EscapeForest()
    {
        Application.Quit();
    }

    public void ReturnMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
