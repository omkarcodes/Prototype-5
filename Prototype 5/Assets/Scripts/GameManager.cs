using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver;

    public  Button RestartButton;

    private int score;

    private float spawnRate = 1.0f;

    public bool isGameActive;
     
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        score = 0;

        StartCoroutine(SpawnTargets());
        UpdateScore(0);      
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
    }
}
