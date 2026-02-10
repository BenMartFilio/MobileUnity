using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNamePanel : MonoBehaviour
{
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private TMP_InputField playerInputField;

    public void LoadDatadInPanel()
    {
        playerInputField.text = playerDatas.Name;
    }

    public void SaveDatasInSO()
    {
        playerDatas.Name = playerInputField.text;
    }
}
