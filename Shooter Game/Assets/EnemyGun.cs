using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float rateOfFire = 15f;
    public int magSize = 30;
    private int ammoLeft = -1;
    public float reloadTime = 1f;
    private float timeToFire = 0f;
    bool isReloading = false;
    public bool automatic = true;
    public ParticleSystem muzzleFlash;
    public GameObject bulletEffect;
    private AudioSource shotSound;
    private Vector3 direction;
    private float angle;
    public Camera Cam;
    private bool playerAlive = true;
    private bool seePlayer = false;
    public Transform capsuleStart;
    public Transform capsuleEnd;
    public LayerMask playerMask;

    void Start()
    {
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
        if (GameObject.Find("Body") == null)
        {
            playerAlive = false;
        }
        seePlayer = (Physics.CheckCapsule(capsuleStart.position, capsuleEnd.position, 5, playerMask));
        if (ammoLeft == 0 && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(reloadAnimation());
        }
        if (Time.time >= timeToFire && ammoLeft > 0 && isReloading == false && playerAlive && seePlayer)
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
            yield return new WaitForSeconds(i / 100);
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
        muzzleFlash.Play();
        shotSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Player player = hit.transform.GetComponentInChildren<Player>();
            Debug.Log(player);
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            GameObject impact = Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }
}
