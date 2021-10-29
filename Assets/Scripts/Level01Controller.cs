using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView = null;
    [SerializeField] Text _currentTimeTextView = null;
    [SerializeField] Text _displayText = null;
    [SerializeField] GameObject _pausePanel = null;
    [SerializeField] MouseLook _mouseLook = null;
    [SerializeField] FlashImage _flashImage = null;
    [SerializeField] AudioClip _winSound = null;

    int _currentScore;
    float _timeRemaining;
    public static bool _paused;
    PlayerHealth _playerHealth;

    public bool _victory = false;

    [SerializeField] Texture2D _crosshair;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentScore = 0;
        _timeRemaining = 60f;
        _paused = false;
        _mouseLook.enabled = true;
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
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
        if(_victory)
        {
            return;
        }
        if(_timeRemaining > 0)
        {
            if(_playerHealth._health > 0)
            {
                _timeRemaining -= Time.deltaTime;
                _currentTimeTextView.text = "Time: " + ((int)_timeRemaining).ToString();
            }
        }
        else
        {
            _currentTimeTextView.text = "Time: 0";
            WinGame();
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

    public void IncreaseScore(int scoreIncrease)
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

    void WinGame()
    {
        _victory = true;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.Kill();
        }
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach(Bullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        HealthPickup[] healths = FindObjectsOfType<HealthPickup>();
        foreach(HealthPickup health in healths)
        {
            Destroy(health.gameObject);
        }
        _displayText.text = "You win!";
        AudioManager.Instance.PlaySound(_winSound);
        _flashImage.StartFlash(3f, .8f, Color.green);
    }
}
