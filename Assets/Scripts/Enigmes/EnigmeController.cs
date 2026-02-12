using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnigmeController : MonoBehaviour
{
    [SerializeField] private EnigmeDatas enigmeData;
    [SerializeField] private Light2D[] lights;
    [SerializeField] private Light2D winLight;

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = enigmeData.lampsActivated[i];
        }
        winLight.enabled = enigmeData.IsAllActivated();
    }
}
