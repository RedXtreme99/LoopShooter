using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Level01Controller _levelController;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] ParticleSystem _spawnParticles;

    AudioSource _audioSource;

    float _spawnTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _spawnTime = Time.time + Random.Range(2f, 10f);
    }

    void Update()
    {
        if(Time.time >= _spawnTime && !_levelController._victory)
        {
            _spawnTime += Random.Range(10f, 18f);
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        _audioSource.Play();
        ParticleSystem particle = Instantiate(_spawnParticles, this.transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 1f);
        GameObject obj = Instantiate(_enemyPrefab, this.transform.position, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy._levelController = FindObjectOfType<Level01Controller>();
        enemy._player = FindObjectOfType<PlayerMovement>().transform;
    }
}
