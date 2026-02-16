using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class OnLifeLose : MonoBehaviour
{
    [SerializeField] private Sprite destroyedLife;
    //Mettre son perte pv ? Il y a déjà destruction

    private void Start()
    {
        //Mettre sprite apparition
    }

    public void LifeLosed()
    {
        GetComponent<Image>().sprite = destroyedLife;
        Destroy(gameObject);
    }
}
