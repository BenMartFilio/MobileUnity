using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private SaveGameSystem save;

    [SerializeField] private List<Button> difficultyButtons;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Button selectedButton;

    private void Start()
    {
        int indexToSelect = (int)playerDatas.selectedDifficulty;

        if (indexToSelect < 0 || indexToSelect >= difficultyButtons.Count)
        {
            indexToSelect = 0;
        }

        SetSelectedButton(difficultyButtons[indexToSelect]);
    }

    public void SetSelectedButton(Button button)
    {
        
        if (selectedButton != null)
        {
            selectedButton.GetComponent<Image>().color = normalColor;
        }
        selectedButton = button;

        selectedButton.GetComponent<Image>().color = highlightColor;
    }


    public void SetDifficultyNormal()
    {
        playerDatas.selectedDifficulty = Difficulty.Normal;
        save.SaveGame();
    }

    public void SetDifficultyHard()
    {
        playerDatas.selectedDifficulty = Difficulty.Hard;
        save.SaveGame();
    }
}
