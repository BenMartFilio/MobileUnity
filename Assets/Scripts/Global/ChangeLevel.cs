using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void SwitchToLevel(int NameID)
    {
        SceneManager.LoadScene(NameID);
    }
}
