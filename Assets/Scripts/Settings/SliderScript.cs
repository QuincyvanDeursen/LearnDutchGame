
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI volumeText;

    private void Start()
    {
        // Laad de opgeslagen MasterVolume en pas deze toe op de slider
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // Default volume is 1
        slider.value = savedVolume;
        volumeText.text = (savedVolume * 100f).ToString("F0") + "%";

        // Pas de MasterVolume toe op de AudioListener
        AudioListener.volume = savedVolume;
    }

    // Wordt aangeroepen wanneer de waarde van de slider verandert
    public void OnValChange(float value)
    {
        // Beperk de waarde tussen 0 en 1
        value = Mathf.Clamp01(value);

        // Update de opgeslagen MasterVolume
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save(); // Sla de wijzigingen op

        // Pas de MasterVolume toe op de AudioListener
        AudioListener.volume = value;

        // Update de tekst op het scherm
        volumeText.text = (value * 100f).ToString("F0") + "%";
    }
}

