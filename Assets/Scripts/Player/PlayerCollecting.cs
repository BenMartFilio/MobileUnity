using TMPro;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private TMP_Text scoreInputField;

    public int score = 0;
    public float bonus = 1f;

    private void Start()
    {
        scoreInputField.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectsWhichFall objectCollected = collision.gameObject.GetComponent<ObjectsWhichFall>();
        if (objectCollected != null)
        {
            CollectingElement(objectCollected);
        }
    } 

    public void CollectingElement(ObjectsWhichFall toDestroy)
    {
        toDestroy.WhenGetted();
        score += (int) Mathf.Round(1*bonus);
        scoreInputField.text = score.ToString();
    }
}
