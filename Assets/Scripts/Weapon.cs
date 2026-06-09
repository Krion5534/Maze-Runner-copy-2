using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // --- CAMERA & INPUT CHECKS ---
    public Camera playerCamera;            
    private bool isShooting;               
    private bool readyToShoot;             
    private bool allowReset = true;        

    // --- BULLET SETTINGS ---
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 100f;    
    public float bulletLifetime = 3f;
    public float shootingDelay = 0.1f;     

    // --- SPREAD ACCURACY ---
    public float spreadIntensity = 0.02f;  

    // --- BURST FIRE SYSTEM ---
    public int bulletsPerBurst = 3;        
    private int burstBulletsLeft;          

    public int weaponDamage;
    // --- SHOOTING MODES ENUM ---
    public enum ShootingMode
    {
        Single,
        Burst,
        Automatic
    }

    public ShootingMode currentShootingMode; 

    private void Awake()
    {
        readyToShoot = true;                        
        burstBulletsLeft = bulletsPerBurst;         
    }

    void Update()
    {
        
        if (currentShootingMode == ShootingMode.Automatic)
        {
            
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst; 
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false; // Lock out further updates during firing [00:04:42]

        // Calculate the vector path including crosshair tracing and structural accuracy spread [00:04:54]
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Spawn bullet clone [00:05:10]
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.bulletDamage = weaponDamage; 
        }

        
        bullet.transform.forward = shootingDirection;

        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        }

        Destroy(bullet, bulletLifetime);

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false; 
        }

        
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--; 
            Invoke("FireWeapon", shootingDelay); 
        }
    }

    private void ResetShot()
    {
        readyToShoot = true; 
        allowReset = true;   
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; 
        }
        else
        {
            targetPoint = ray.GetPoint(100); 
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float xSpread = Random.Range(-spreadIntensity, spreadIntensity);
        float ySpread = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + playerCamera.transform.right * xSpread + playerCamera.transform.up * ySpread;
    }
}