using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    protected GameManager gameManager;
    public Slider musicSlider, soundsSlider;
    public GameObject pauseMenu;
    public GameObject winnerPanel;
    public GameObject restart;

    AudioSource audioSource;
    public AudioClip clip;

    public AudioMixer audioMixer;
    public bool isGamePaused;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        audioMixer.SetFloat("music", SaveManager.instance.musicVolumeValue);
        audioMixer.SetFloat("sounds", SaveManager.instance.soundsVolumeValue);

        musicSlider.value = SaveManager.instance.musicVolumeValue;
        soundsSlider.value = SaveManager.instance.soundsVolumeValue;

        isGamePaused = false;
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        isGamePaused = !isGamePaused;
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

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        restart.SetActive(false);
    }

    public void SaveName()
    {
        gameManager.isGameFinished = false;
        winnerPanel.SetActive(false);
        restart.SetActive(true);
    }

}
