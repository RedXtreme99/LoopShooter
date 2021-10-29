using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Level01Controller _levelController;

    [SerializeField] ParticleSystem _hitParticle;
    [SerializeField] float _shootDistance = 100f;
    [SerializeField] int _weaponDamage = 10;
    [SerializeField] int _chargeAmount = 10;
    [SerializeField] LayerMask _hitLayer;
    [SerializeField] Transform _bulletSpawn;
    [SerializeField] ParticleSystem _smokeParticles;
    [SerializeField] AudioClip _shootSound;

    RaycastHit _hitinfo;

    PlayerChrono _chrono;

    bool _dead = false;

    private void Awake()
    {
        _chrono = GetComponent<PlayerChrono>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !_dead)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        ParticleSystem smokeParticle = Instantiate(_smokeParticles, _bulletSpawn.position + new Vector3(0f, -.3f, 0f), Quaternion.identity);
        Destroy(smokeParticle.gameObject, 1f);
        AudioManager.Instance.PlaySound(_shootSound);
        Transform center = Camera.main.transform;
        if(Physics.Raycast(center.position, center.forward, out _hitinfo, _shootDistance, _hitLayer))
        {
            if(_hitinfo.transform.tag == "Enemy")
            {
                ParticleSystem hitParticle = Instantiate(_hitParticle,_hitinfo.point + _hitinfo.normal * 2, Quaternion.identity);
                Destroy(hitParticle, 1f);
                Enemy enemy = _hitinfo.transform.gameObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(_weaponDamage);
                    _chrono.AddCharge(_chargeAmount);
                    _levelController.IncreaseScore(5);
                }
            }
        }
    }

    public void SetDead(bool dead)
    {
        _dead = dead;
    }
}
