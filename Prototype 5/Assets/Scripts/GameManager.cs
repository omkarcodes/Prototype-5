using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackGroundPref = "BackGroundPref";

    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI livesText;

    public Button RestartButton;
    public Button pause;
    public Button play;
    public GameObject mainMenu;
    public GameObject howToPlayPanel;

    private int score;
    private int firstPlayInt;
    public int lives = 3;


    private float spawnRate = 1.0f;

    public bool isGameActive;

    public AudioSource backgroundMusic;
    public Slider backgroundMusicSlider;
    private float backgroundMusicFloat;

    public Animator camAnim;
     
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if(firstPlayInt == 0)
        {
            backgroundMusicFloat = 1f;
            backgroundMusicSlider.value = backgroundMusicFloat;
            PlayerPrefs.SetFloat(BackGroundPref, backgroundMusicFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundMusicFloat =  PlayerPrefs.GetFloat(BackGroundPref);
            backgroundMusicSlider.value = backgroundMusicFloat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTargets()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        isGameActive = false;
        RestartButton.gameObject.SetActive(true);
        backgroundMusic.Stop();
        pause.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        score = 0;
        backgroundMusic.Play();
        spawnRate /= difficulty;

        StartCoroutine(SpawnTargets());
        UpdateScore(0);
        UpdateLives(-3);

        mainMenu.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void UpdateLives(int livesToSubtract)
    {
        lives -= livesToSubtract;
        livesText.text = "Lives: " + lives;
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackGroundPref, backgroundMusicSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        backgroundMusic.volume = backgroundMusicSlider.value;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
        backgroundMusic.Stop();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        play.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
        backgroundMusic.Play();
    }

    public void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }


}
