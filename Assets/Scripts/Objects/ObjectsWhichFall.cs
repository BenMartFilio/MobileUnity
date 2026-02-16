using System.Linq;
using UnityEngine;

public class ObjectsWhichFall : MonoBehaviour
{
    private SpriteRenderer spriteToChange;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite spriteDestruction;
    private int randomNumber;
    private void Start()
    {
        ChangeSkin();
    }
    public void WhenGetted()
    {
        Destroy(gameObject);
        // faire spawn un game object avec un sprite de récupérer
        // faire spawn son récupéré.
    }

    public void ChangeSkin()
    {
        SpriteRenderer spriteToChange = GetComponent<SpriteRenderer>();
        randomNumber = Random.Range(0, sprites.Length);
        spriteToChange.sprite = sprites[randomNumber];
    }

    public void WhenDestroyed()
    {
        SpriteRenderer spriteToChange = GetComponent<SpriteRenderer>();
        spriteToChange.sprite = spriteDestruction;
    }
}
