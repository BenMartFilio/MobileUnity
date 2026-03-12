using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private Sprite[] _skin;
    [SerializeField] private Sprite[] _skinLeft;
    [SerializeField] private Sprite[] _skinRight;
    [SerializeField] private Sprite[] _skinUp;
    [SerializeField] private Sprite[] _skinDown;
    [SerializeField] private SpriteRenderer Complet;
    [SerializeField] private SpriteRenderer Left;
    [SerializeField] private SpriteRenderer Right;
    [SerializeField] private SpriteRenderer Up;
    [SerializeField] private SpriteRenderer Down;
    //mettre que en fonction index ça pioche dans base de skin et ça le met
    public int skinIndex;

    private void Start()
    {
        Complet.sprite = _skin[skinIndex];
        Left.sprite = _skinLeft[skinIndex];
        Right.sprite = _skinRight[skinIndex];
        Up.sprite = _skinUp[skinIndex];
        Down.sprite = _skinDown[skinIndex];
    }
}