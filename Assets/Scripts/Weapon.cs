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

        // Align the forward velocity direction of the physics object to the tracked screen point path [00:05:17]
        bullet.transform.forward = shootingDirection;

        // Apply impulse velocity vector to shoot the bullet [00:05:25]
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        }

        Destroy(bullet, bulletLifetime);

        // Reset Shot Routine - Invoked using string tracking to support dynamic timing loops [00:05:31]
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false; // Clamp trigger lock [00:05:31]
        }

        // --- BURST LOGIC ---
        // If in burst mode and there are more bullets left to shoot in this burst [00:05:44]
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--; // Deduct bullet from calculation loop count [00:05:57]
            Invoke("FireWeapon", shootingDelay); // Loop weapon execution directly via recursion delay [00:05:57]
        }
    }

    private void ResetShot()
    {
        readyToShoot = true; // Unlock weapon logic processing loop [00:06:38]
        allowReset = true;   // Reset internal verification gate [00:06:38]
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        // Cast a target-finding ray right through the dead center of the screen viewport [00:07:26]
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        // Check if the raycast collides physically with an obstacle or layout geometry [00:07:34]
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Target found in space [00:07:34]
        }
        else
        {
            targetPoint = ray.GetPoint(100); // Ray missed; fallback target path drawn 100 units out [00:07:40]
        }

        // Base calculation heading: Target point vector minus gun tip origin vector [00:07:52]
        Vector3 direction = targetPoint - bulletSpawn.position;

        // Generate accuracy offset vectors dynamically across horizontal and vertical thresholds [00:08:13]
        float xSpread = Random.Range(-spreadIntensity, spreadIntensity);
        float ySpread = Random.Range(-spreadIntensity, spreadIntensity);

        // Inject the localized accuracy spread deviations into the normalized forward facing direction [00:08:28]
        return direction + playerCamera.transform.right * xSpread + playerCamera.transform.up * ySpread;
    }
}