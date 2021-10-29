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
    [SerializeField] Level01Controller _levelController;
    [SerializeField] AudioClip _healSound;
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] AudioClip _lossSound;

    PlayerMovement _playerMovement;
    [SerializeField] MouseLook _mouseLook;
    FireWeapon _fireWeapon;
    PlayerChrono _playerChrono;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _fireWeapon = GetComponent<FireWeapon>();
        _playerChrono = GetComponent<PlayerChrono>();
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Heal(int healAmt)
    {
        if(!_dead)
        {
            AudioManager.Instance.PlaySound(_healSound);
            _health += healAmt;
            if(_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
            _healthSlider.value = _health;
            _flashImage.StartFlash(.25f, .5f, Color.green);
            _levelController.IncreaseScore(20);
        }
    }

    public void DamagePlayer(int damageAmt)
    {
        if(!_dead)
        {
            AudioManager.Instance.PlaySound(_hurtSound);
            if(_health > 0)
            {
                _health -= damageAmt;
            }
            if(_health <= 0)
            {
                _health = 0;
                _healthSlider.value = 0;
                if(!_dead)
                {
                    Kill();
                }
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
        _dead = true;
        _playerMovement.SetDead(true);
        _mouseLook.SetDead(true);
        _fireWeapon.SetDead(true);
        _playerChrono.SetDead(true);
        _displayText.text = "You lost!\nPress backspace to restart.";
        AudioManager.Instance.PlaySound(_lossSound);
        _flashImage.StartFlash(3f, .8f, Color.red);
    }
}
