using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int _health;
    public int _maxHealth = 100;
    bool _dead = false;

    [SerializeField] FlashImage _flashImage;
    [SerializeField] Text _displayText;
    [SerializeField] Slider _healthSlider;

    PlayerMovement _playerMovement;
    [SerializeField] MouseLook _mouseLook;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Heal(int healAmt)
    {
        if(!_dead)
        {
            _health += healAmt;
            if(_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
            _healthSlider.value = _health;
        }
    }

    public void DamagePlayer(int damageAmt)
    {
        if(!_dead)
        {
            if(_health > 0)
            {
                _health -= damageAmt;
            }
            if(_health <= 0)
            {
                _health = 0;
                _healthSlider.value = 0;
                Kill();
            }
            else
            {
                _healthSlider.value = _health;
                _flashImage.StartFlash(.25f, .5f, Color.red);
            }
        }
    }

    public void Kill()
    {
        _playerMovement.SetDead(true);
        _mouseLook.SetDead(true);
        _displayText.text = "You lost!\nPress backspace to restart.";
        _flashImage.StartFlash(3f, .8f, Color.red);
    }
}
