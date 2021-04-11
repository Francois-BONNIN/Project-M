using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public Animator animator;

    private bool isShooting = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && isShooting==true)
        {
            animator.SetTrigger("Shoot");
            Shoot();
            isShooting = false;
            
            //Bruit de tir
            
            StartCoroutine(shoot_delayed(0.5f));
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator shoot_delayed(float temps)
    {
        yield return new WaitForSeconds(temps);
        isShooting = true;
    }
}
