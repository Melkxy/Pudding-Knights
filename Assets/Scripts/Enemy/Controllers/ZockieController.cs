using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZockieController : EnemyController
{
    protected void Update()
    {
	Animate();
    }

    protected override void Animate()
    {
	anim.SetFloat("Speed", rb.velocity.magnitude);
	if (enemyAI.moveX > 0)
	{
	    lookDirectionX = 1;
	}
	else
	{
	    lookDirectionX = -1;
	}
	anim.SetFloat("LookDirectionX", lookDirectionX);
    }
}
