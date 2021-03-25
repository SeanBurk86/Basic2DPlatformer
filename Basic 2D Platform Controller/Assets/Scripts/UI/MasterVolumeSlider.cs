using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    private AudioManager AM;
    public Slider slider;
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        slider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = AM.masterVolume;
    }
}
