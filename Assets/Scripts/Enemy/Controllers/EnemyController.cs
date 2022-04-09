using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField]protected SimpleEnemyAI enemyAI;
    protected float lookDirectionX, lookDirectionY;
    protected Rigidbody2D rb;
    protected Animator anim;

    private void Awake()
    {
	rb = GetComponent<Rigidbody2D>();
	anim = GetComponent<Animator>();
    }

    protected abstract void Animate();
}
