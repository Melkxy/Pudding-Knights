using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected float maxSpeed;
    protected float speed;
    [SerializeField]
    protected float aceleration;
    
    protected GameObject _target;
    public GameObject target { get { return _target; } protected set { _target = value; } }

    protected CircleCollider2D viewRange;
    [SerializeField]
    protected float viewRadius, followRadius;

    protected Rigidbody2D rb;
    protected Vector2 moveDirection;
    
    protected bool isInRange;

    protected bool freezed;
    
    protected void Awake()
    {
	rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
	viewRange = GetComponent<CircleCollider2D>();
	target = GameObject.FindWithTag("Player");
    }

    protected void Update()
    {
	if (!freezed)
	{
	    SetDirection();
	}
    }

    protected void FixedUpdate()
    {
	if (GameState.IsBusy())
	{
	    freeze();
	}
	else if (freezed)
	{
	    freeze();
	}
	else if (isInRange)
 	{
	    MoveToTarget();
	    viewRange.radius = followRadius;
	}
	else
	{
	    MovePatroling();
	    viewRange.radius = viewRadius;
	}
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject == target)
	{
	    isInRange = true;
	}
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
	if(other.gameObject == target)
	{
	    isInRange = true;
	}
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
	if(other.gameObject == target)
	{
	    isInRange = false;
	}
    }

    protected void ResetSpeed()
    {
	speed = 0;
    }

    protected abstract void SetDirection();

    protected abstract void MoveToTarget();
    protected abstract void MovePatroling();

    protected void freeze()
    {
	rb.velocity = Vector2.zero;
	moveDirection = Vector2.zero;
    }

    public void PauseEnemy(float waitTime)
    {
	StartCoroutine(PauseEnemyMovement(waitTime));
    }

    protected IEnumerator PauseEnemyMovement(float waitTime)
    {
	freezed = true;
	yield return new WaitForSeconds(waitTime);
	freezed = false;
    }
}
