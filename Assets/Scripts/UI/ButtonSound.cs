using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioEventDispatcher _AudioEventDispatcher;
    [SerializeField] private AudioType _ButtonAudioType;


    public void PlayButtonSound()
    {
        _AudioEventDispatcher.PlayAudio(_ButtonAudioType);
    }
}
