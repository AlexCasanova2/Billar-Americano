using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance { get; private set; }

    public GameObject generalPanel;
    public GameObject optionsPanel;
    public GameObject rankingPanel;
    public GameObject areYouSurePanel;

    public Slider musicSlider, soundsSlider;

    AudioSource audioSource;
    public AudioClip clip;

    public AudioMixer audioMixer;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioMixer.SetFloat("music", SaveManager.instance.musicVolumeValue);
        audioMixer.SetFloat("sounds", SaveManager.instance.soundsVolumeValue);

        musicSlider.value = SaveManager.instance.musicVolumeValue;
        soundsSlider.value = SaveManager.instance.soundsVolumeValue;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ShowMenuOptions()
    {
        generalPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void HideMenuOptions()
    {
        generalPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowRanking() 
    {
        generalPanel.SetActive(false);
        rankingPanel.SetActive(true);
    }

    public void HideRanking()
    {
        generalPanel.SetActive(true);
        rankingPanel.SetActive(false);
    }

    public void AreYouSure()
    {
        areYouSurePanel.SetActive(true);
        generalPanel.SetActive(false);
    }

    public void HideAreYouSure()
    {
        areYouSurePanel.SetActive(false);
        generalPanel.SetActive(true);
    }

    public void DeleteRanking()
    {
        Debug.Log("Borrar Ranking");
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
        SaveManager.instance.musicVolumeValue = volume;
        SaveManager.instance.Save();
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("sounds", volume);
        SaveManager.instance.soundsVolumeValue = volume;
        SaveManager.instance.Save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
