using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 _lastInput;
    public Vector2 lastInput { get { return _lastInput; } private set { _lastInput = value; } }
    private bool freezed;

    private Animator _anim;
    public Animator anim { get { return _anim; } private set { _anim = value; } }

    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashCoolDown;
    private float dashCoolDownTimer;

    [SerializeField]
    private float dashDuration;
    private float dashTimer;

    [SerializeField]
    private int dashManaCost;

    private void Awake()
    {
	rb = GetComponent<Rigidbody2D>();
	anim = GetComponent<Animator>();
	lastInput = new Vector2(0, -1);
	stats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
	CalcAxis();
	Dash();
	if (dashTimer <= 0)
	{
	    Move();
	}
    }

    private void CalcAxis()
    {
	if (GameState.IsBusy())
	{
	    moveInput = Vector2.zero;
	    anim.SetFloat("Speed", moveInput.sqrMagnitude);
            anim.SetFloat("Vertical", -1);
	}
	else
	{
	    moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
	    if (moveInput == Vector2.zero)
	    {
	        anim.SetFloat("Speed", moveInput.sqrMagnitude);
	    }
	    else
	    {
	        lastInput = moveInput.normalized;
	        anim.SetFloat("Speed", moveInput.sqrMagnitude);
                anim.SetFloat("Horizontal", lastInput.x);
                anim.SetFloat("Vertical", lastInput.y);
	    }
	}
    }

    private void Move()
    {
	if (freezed)
	{
	    Freeze();
	}
	else
	{
	    rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
	}
    }

    private void Freeze()
    {
	rb.velocity = Vector2.zero;
    }

    public void PausePlayer(float waitTime)
    {
	StartCoroutine(PausePlayerMovement(waitTime));
    }

    private IEnumerator PausePlayerMovement(float waitTime)
    {
	freezed = true;
	yield return new WaitForSeconds(waitTime);
	freezed = false;
    }

    private void Dash()
    {
	if (Input.GetAxisRaw("Fire3") > 0 && dashCoolDownTimer <= 0 && stats.currentMP > 0)
	{
	    dashTimer = dashDuration;
	    dashCoolDownTimer = dashCoolDown;
	    rb.AddForce(new Vector2(dashSpeed * lastInput.x, dashSpeed * lastInput.y), ForceMode2D.Impulse);
	    stats.DamageMP(dashManaCost);
	    stats.ResetManaRegen();
	}
	if (dashTimer > 0)
	{
	    dashTimer -= Time.deltaTime;
	}
	if (dashCoolDownTimer > 0)
	{
	    dashCoolDownTimer -= Time.deltaTime;
	}
    }
}
