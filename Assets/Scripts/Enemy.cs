using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Level01Controller _levelController;

    public Transform _player;
    [SerializeField] float _detectionRange;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _weaponDamage;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Transform _bulletSpawn;
    [SerializeField] float _attackFrequency;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] ParticleSystem _chronoParticles;
    [SerializeField] GameObject _healthPickup;
    [SerializeField] Slider _healthSlider;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _deathSound;

    Vector3 _spawnPoint;
    public int _health = 100;
    Color _defaultColor;
    bool _inRange = false;
    private float _nextAttack;

    private void Awake()
    {
        _spawnPoint = this.transform.position;
        _defaultColor = _meshRenderer.material.color;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _nextAttack = Time.time + _attackFrequency;
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;
    }

    private void Update()
    {
        float step = _moveSpeed * Time.deltaTime;
        this.transform.LookAt(_player);
        this.transform.position = Vector3.MoveTowards(this.transform.position, _player.position, step);
        if(Vector3.Distance(_player.transform.position, this.transform.position) < _detectionRange)
        {
            _inRange = true;
        }
        else
        {
            _inRange = false;
        }
        if(Time.time >= _nextAttack)
        {
            _nextAttack += _attackFrequency;
            Shoot();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        FlashRed();
        if(_health <= 0)
        {
            _health = 0;
            _healthSlider.value = _health;
            Kill();
        }
        _healthSlider.value = _health;
    }

    public void Kill()
    {
        _levelController.IncreaseScore(25);
        ParticleSystem particle = Instantiate(_deathParticles, this.transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 1f);
        AudioManager.Instance.PlaySound(_deathSound);
        float random = Random.Range(0f, 1f);
        if(random > .9)
        {
            Instantiate(_healthPickup, this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void ResetPosition()
    {
        ParticleSystem particle = Instantiate(_chronoParticles, this.transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 1f);
        float random = Random.Range(0f, 1f);
        if(random > .5)
        {
            Instantiate(_healthPickup, this.transform.position, Quaternion.identity);
        }
        this.transform.position = _spawnPoint;
    }

    void FlashRed()
    {
        _meshRenderer.material.color = Color.red;
        Invoke("ResetColor", .15f);
    }

    void ResetColor()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    void Shoot()
    {
        if(_inRange)
        {
            GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, _bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            bullet.GetComponent<Bullet>()._damage = _weaponDamage;
            Destroy(bullet, 10f);
            _audioSource.Play();
        }
    }
}
