using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChrono : MonoBehaviour
{
    [SerializeField] Level01Controller _levelController;

    [SerializeField] FlashImage _flashImage;
    [SerializeField] Slider _chronoSlider;
    [SerializeField] Image _fill;
    [SerializeField] LayerMask _hitLayer;
    [SerializeField] Transform _bulletSpawn;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] AudioClip _chronoSound;

    public int _charge = 0;
    public int _maxCharge = 100;
    public bool _charged = false;
    public float _shootDistance = 100f;

    Color _defaultColor;

    RaycastHit _hitinfo;

    bool _dead = false;

    void Start()
    {
        _defaultColor = _fill.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && _charged && !_dead)
        {
            ShootChrono();
        }
    }

    public void AddCharge(int chargeAmount)
    {
        _charge += chargeAmount;
        if(_charge >= _maxCharge)
        {
            _charge = _maxCharge;
            if(!_charged)
            {
                _flashImage.StartFlash(.25f, .5f, _defaultColor);
            }
            _charged = true;
            _fill.color = Color.white;
        }
        _chronoSlider.value = _charge;
    }

    void ShootChrono()
    {
        Transform center = Camera.main.transform;
        if(Physics.Raycast(center.position, center.forward, out _hitinfo, _shootDistance, _hitLayer))
        {
            if(_hitinfo.transform.tag == "Enemy")
            {
                AudioManager.Instance.PlaySound(_chronoSound);
                _charged = false;
                _charge = 0;
                _chronoSlider.value = _charge;
                _fill.color = _defaultColor;
                CreateChronoBlast(_hitinfo.point);
                Enemy enemy = _hitinfo.transform.gameObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    _levelController.IncreaseScore(10);
                    enemy.ResetPosition();
                }
            }
        }
    }

    public void SetDead(bool dead)
    {
        _dead = dead;
    }

    void CreateChronoBlast(Vector3 point)
    {
        LineRenderer lr = Instantiate(_lineRenderer, Vector3.zero, Quaternion.identity);
        lr.SetPosition(0, _bulletSpawn.position);
        lr.SetPosition(1, point);
        Destroy(lr.gameObject, .2f);
    }
}
