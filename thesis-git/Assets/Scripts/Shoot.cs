using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public Rigidbody bulletPrefab;
    public Transform firePosition;
    public float bulletSpeed;
    GameObject[] bullets;
     
    void Update ()
    {
        Shooting();

        // would be better to run every minute or two instead
        ClearShells();
    }

    void ClearShells()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (bullets.Length > 5 )
        {
            Destroy(bullets[0]);
        }
    }
    
    
    void Shooting ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletInstance = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
            bulletInstance.AddForce(firePosition.forward * bulletSpeed);
            bulletInstance.tag = "Bullet";
        }
    }
}
