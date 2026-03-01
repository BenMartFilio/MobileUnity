using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private ChangeDifficulty difficultyManager;

    public void OnClickDifficulty()
    {
        difficultyManager.SetSelectedButton(this.gameObject.GetComponent<Button>());
    }
}
