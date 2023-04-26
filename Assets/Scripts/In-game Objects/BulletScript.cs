using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /// <summary>
    /// The physical point in the world at which we will point at.
    /// And move in the direction of.
    /// </summary>
    public Vector2 pointTarget;

    public GameObject owner;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private void Update()
    {
        PointTowardsTarget();
        Move();
        if ((Vector2)transform.position == pointTarget)
        {
            Debug.Log("Hello");
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Set the target which this bullet will point towards.
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetPointTarget(Vector2 newTarget)
    {
        pointTarget = newTarget;
    }
    
    /// <summary>
    /// Just moves the bullet forward indiscriminately. 
    /// </summary>
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointTarget, speed * Time.deltaTime);
    }
    
    /// <summary>
    /// Points the bullet in the direction of pointTarget
    /// </summary>
    private void PointTowardsTarget()
    {
        transform.up = (Vector3)pointTarget - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Don't hit our own shooter.
        if (col.gameObject == owner) return;
        
        // If we hit something that's destructible
        var destructible = col.gameObject.GetComponent<Destructible>();
        if (destructible)  // Make it take damage.
        {
            destructible.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
