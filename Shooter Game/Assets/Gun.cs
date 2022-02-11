using UnityEngine;
using System.Collections;
using System;

public class Gun : MonoBehaviour
{

    public static Gun instance;

    
    public float damage = 10f;
    public float range = 100f;
    public float rateOfFire = 15f;
    public int magSize = 30;
    private int ammoLeft = 30;
    public float reloadTime = 1f;
    private float timeToFire = 0f;
    bool isReloading = false;
    public bool automatic = true;
    public Camera Cam;
    public ParticleSystem muzzleFlash;
    public GameObject bulletEffect;
    private AudioSource shotSound;

    void Start()
    {
        instance = this;
        ammoLeft = magSize;
        shotSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (isReloading == true)
        {
            transform.Rotate(Vector3.down, 30);
        }
        isReloading = false;

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && ammoLeft != magSize && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(reloadAnimation());
        }
        if (Input.GetButton("Fire1") && automatic && Time.time >= timeToFire && ammoLeft > 0 && isReloading == false)
        {
            timeToFire = Time.time + 1f / rateOfFire;
            Shoot();
        }else if(Input.GetButtonDown("Fire1") && Time.time >= timeToFire && ammoLeft > 0 && isReloading == false)
        {
            timeToFire = Time.time + 1f / rateOfFire;
            Shoot();
        }


    }

    IEnumerator reloadAnimation()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.Rotate(Vector3.up, 1);
            yield return new WaitForSeconds(i/100);
        }
        yield return new WaitForSeconds(reloadTime);
        for (int i = 0; i < 30; i++)
        {
            transform.Rotate(Vector3.down, 1);
            yield return new WaitForSeconds(i / 100);
        }
        isReloading = false;
        ammoLeft = magSize;
    }

    void Shoot()
    {
        ammoLeft--;
        //GunStatus gs = new GunStatus();
        //gs.updateText(ammoLeft, magSize);
        muzzleFlash.Play();
        shotSound.Play();
        RaycastHit hit;
        if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            GameObject impact = Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }

}