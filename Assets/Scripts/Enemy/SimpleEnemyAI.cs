using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : EnemyAI
{
    private float _moveX, moveY;
    public float moveX { get { return _moveX; } private set { _moveX = value; } }

    protected override void SetDirection()
    {
	if (isInRange)
	{
	    SpeedAceleration(maxSpeed, aceleration);

	    moveX = Mathf.Clamp(target.transform.position.x - transform.parent.position.x, -1, 1);
	    moveY = Mathf.Clamp(target.transform.position.y - transform.parent.position.y, -1, 1);

	    moveDirection = new Vector2(moveX, moveY);
	}
	else
	{
	    SpeedAceleration(maxSpeed / 2, aceleration / 2);
	}
    }

    private void SpeedAceleration(float _maxSpeed, float _aceleration)
    {
	if (speed < _maxSpeed)
	{
	    if (speed + _aceleration > _maxSpeed)
	    {
		speed = _maxSpeed;
	    }
	    else
	    {
		speed += _aceleration;
	    }
	}
	else if (speed > _maxSpeed)
	{
	    speed = _maxSpeed;
	}
    }

    protected override void MoveToTarget()
    {
	rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    protected override void MovePatroling()
    {
	rb.velocity = new Vector2(0f, 0f);
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackForce, Transform dmgOrigin)
    {
	float counter = 0;

	while (counter < knockbackDuration)
	{
	    counter += Time.deltaTime;
	    Vector2 direction = (dmgOrigin.transform.position - transform.position).normalized;
	    rb.AddForce(-direction * knockbackForce);
	}

	ResetSpeed();

	yield return 0;
    }
}
