using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{

    private Slider _slider;

    private void Start()
    {
        AudioListener.volume = 0.15f;
        _slider = GetComponent<Slider>();
    }

    public void OnValueChanged()
    {
        AudioListener.volume = _slider.value;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
