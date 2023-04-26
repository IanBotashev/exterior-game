using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatUnitScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private float randomSpread;
    [SerializeField] private float enemyCheckFrequency;
    [SerializeField] private float sightDistance;
    [SerializeField] private GovernmentOwnership owner;
    [SerializeField] private float reloadTime;
    /// <summary>
    /// Check if the reload function is running or not.
    /// </summary>
    [SerializeField] private bool reloading;
    /// <summary>
    /// How many times can we fire before we have to reload.
    /// </summary>
    [SerializeField] private float ammo;
    /// <summary>
    /// Max ammo we can hold in one "cartridge"
    /// </summary>
    [SerializeField] private float ammoCapacity;
    
    /// <summary>
    /// How fast can we fire.
    /// </summary>
    [SerializeField] private float firingSpeed;
    
    /// <summary>
    /// Can we fire again?
    /// </summary>
    [SerializeField] private bool readyToFire;

    private void Start()
    {
        // Check for enemies every enemyCheckFrequency.
        InvokeRepeating(nameof(EnemyCheck), enemyCheckFrequency, enemyCheckFrequency);
        owner = GetComponent<GovernmentOwnership>();
        readyToFire = true;
        ammo = ammoCapacity;
    }

    private void OnDisable()
    {
        CancelInvoke();
        reloading = false;
    }

    private void OnEnable()
    {
        readyToFire = true;
        ammo = ammoCapacity;
        InvokeRepeating(nameof(EnemyCheck), enemyCheckFrequency, enemyCheckFrequency);
    }

    private void Update()
    {
        // Check if we have a target, or if we're reloading.
        if (!target) return;

        if (ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        // Check if the target is out of distance, if so, disengage.
        if (Vector2.Distance(transform.position, target.transform.position) > sightDistance)
        {
            SetTarget(null);
            return;
        }
        
        Fire();
    }
    
    /// <summary>
    ///  Fires on the target set.
    /// </summary>
    private void Fire()
    {
        // If we're not ready to fire, or if we are reloading, return.
        if (!readyToFire || reloading) return;
        readyToFire = false;
        ammo--;
        ShootBullet();
        StartCoroutine(EnforceFiringRate());
    }

    /// <summary>
    /// Shoots bullet at target.
    /// </summary>
    private void ShootBullet()
    {
        var bulletObject = Instantiate(bulletPrefab, transform.position, quaternion.identity);
        var bullet = bulletObject.GetComponent<BulletScript>();
        var position = target.transform.position;
        // Basically, we get the distance to the target, divided by our maximum sightline.
        // We get a "percentage" of how far they are from our range.
        // Then we just multiply that by the random spread.
        var adjustedRandomSpread = Vector2.Distance(transform.position, target.transform.position) / sightDistance *
                                   randomSpread;
        
        var targetWithSpread = new Vector2(
            position.x + Random.Range(-adjustedRandomSpread, adjustedRandomSpread),
            position.y + Random.Range(-adjustedRandomSpread, adjustedRandomSpread));
        
        bullet.SetPointTarget(targetWithSpread);
        bullet.owner = gameObject;  // Set ourselves as the owner.
    }
    
    /// <summary>
    /// Sets the "readyToFire" variable. Waits a few seconds between each shot.
    /// </summary>
    private IEnumerator EnforceFiringRate()
    {
        yield return new WaitForSeconds(firingSpeed);
        readyToFire = true;
    }
    
    /// <summary>
    /// Checks for enemies nearby with a circle cast.
    /// </summary>
    /// <returns></returns>
    private void EnemyCheck()
    {
        // If we already have a target, don't check for new enemies.
        // If we're disabled, don't check for new enemies.
        Debug.Log("Enemy check..." + gameObject.name);
        Debug.Log((bool)target);
        if (target || !this.enabled) return;
        var results = Physics2D.OverlapCircleAll(transform.position, sightDistance, GameManager.Instance.selectableLayerMask);
        foreach (var collider in results)  // Iterate through all the caught objects
        {
            var owner = collider.gameObject.GetComponent<GovernmentOwnership>();
            if (!owner || CheckIfSameOwner(owner)) continue;
            SetTarget(owner.gameObject);
            return;
        }
    }

    /// <summary>
    /// Function to wait a bit to reload to our max capacity.
    /// </summary>
    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = ammoCapacity;
        reloading = false;
    }
    
    /// <summary>
    /// Set our current target
    /// </summary>
    /// <param name="newTarget"></param>
    private void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    
    /// <summary>
    /// Checks if we and a target have the same owner.
    /// </summary>
    /// <param name="ownerTarget"></param>
    private bool CheckIfSameOwner(GovernmentOwnership ownerTarget)
    {
        return owner.GetOwner() == ownerTarget.GetOwner();
    }
}
