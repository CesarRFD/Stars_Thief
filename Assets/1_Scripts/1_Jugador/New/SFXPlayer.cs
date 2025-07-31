using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private SFX sfx;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool walking;
    private Movement _mov;
    private bool _muteSteps;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _mov = FindObjectOfType<Movement>();
    }

    void Update()
    {
        _muteSteps = _mov.StepsSFXState();
        if (Physics2D.Raycast(_rb.position - new Vector2(0.3f, 0.1f), Vector2.down, .25f, LayerMask.GetMask("Ground")) 
            || (Physics2D.Raycast(_rb.position + new Vector2(0.3f, -0.1f), Vector2.down, .25f, LayerMask.GetMask("Ground"))))
            _isGrounded = true; else _isGrounded = false;
        
        if (_isGrounded && _rb.velocity.x > 0.2f || _rb.velocity.x < -0.2f)
        {
            if(!walking && !_muteSteps)
            {
                walking = true;
                sfx.PlayWalk();
            }
        }
        if (!_isGrounded || _rb.velocity.x < 0.2f && _rb.velocity.x > -0.2f)
        {
            if (walking)
            {
                walking = false;
                sfx.StopWalk();
            }
        }
        if (!_isGrounded) sfx.StopWalk();//Este codigo detiene el sonido de caminar si no se esta moviendo o esta en el aire.
        if (_muteSteps) sfx.StopWalk();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            JumpSFX();
        }
    }

    public void JumpSFX()
    {
        sfx.PlayJump();
    }
}
