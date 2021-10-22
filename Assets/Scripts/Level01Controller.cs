using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView = null;
    [SerializeField] GameObject _pausePanel = null;
    [SerializeField] MouseLook _mouseLook = null;

    int _currentScore;
    public static bool _paused;

    [SerializeField] Texture2D _crosshair;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentScore = 0;
        _paused = false;
        _mouseLook.enabled = true;
    }

    void Update()
    {
        // TODO replace and add real scoring system
        if(Input.GetKeyDown(KeyCode.Q) && !_paused)
        {
            IncreaseScore(5);
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(_paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            RestartLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
    }

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (_crosshair.width / 2);
        float yMin = (Screen.height / 2) - (_crosshair.height / 2);
        if(!_paused)
        {
            GUI.DrawTexture(new Rect(xMin, yMin, _crosshair.width, _crosshair.height), _crosshair);
        }
        else
        {
        }
    }

    public void RestartLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
        }
        
        SceneManager.LoadScene("Level01");
    }

    public void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
        }

        SceneManager.LoadScene("MainMenu");
    }

    void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
        _paused = true;
    }

    public void UnpauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }
}
