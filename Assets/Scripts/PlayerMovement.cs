using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed = 12f;
    public float _sprintMultiplier = 2f;
    public float _jumpHeight = 3f;
    public float _gravity = -9.81f;

    [SerializeField] CharacterController _controller;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] LayerMask _groundMask;

    public bool _dead = false;

    Vector3 _velocity;
    bool _grounded;
    bool _sprinting;

    void Update()
    {
        if(_dead)
        {
            return;
        }

        _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        _sprinting = Input.GetKey(KeyCode.LeftShift);

        if(_grounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if(_sprinting)
        {
            move *= _sprintMultiplier;
        }

        _controller.Move(move * _speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && _grounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void SetDead(bool dead)
    {
        _dead = dead;
    }
}
