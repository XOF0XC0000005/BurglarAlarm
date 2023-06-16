using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private int _forward = Animator.StringToHash("forward");
    private int _strafe = Animator.StringToHash("strafe");
    private CharacterController _controller;
    private Animator _animator;

    [Header("Movement")]
    [SerializeField] float _speed = 1f;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis(Horizontal);
        float z = Input.GetAxis(Vertical);

        _animator.SetFloat(_forward, z);
        _animator.SetFloat(_strafe, x);

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);
    }
}