using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;
    public float bulletSpeed = 30;
    public GameObject MuzzleFlash;
    public float damage = 20f;
    public GameObject Light;
    public float BulletlifeTime;
    public float LightlifeTime;
    public bool lighton;
    public Animator animator;
    public Animation Recoil;
    public float FireanimTime = 0.01f;
    public Button Bu;
    public float interactablebuttonTim  = .5f;
    public float MuzzleFlashTime = 0.3f;
    public AudioSource shootsound;
    public AudioSource ReloadSound;
    public float maxBullet;
    private float currentBullet;
    private bool isShoot = true;
    private bool isReloading = false;
    public Text ammoDisplay;
    public float reload = 1.5f;
    public Button Dropbut;

    
    public void ButtonFire()
    {

        if (isShoot == true)
        {
            Shootandanim();
        }
        Bu.interactable = false;
        StartCoroutine(ButtonEnable());
    }
    IEnumerator ButtonEnable()
    {
        yield return new WaitForSeconds(interactablebuttonTim);
        Bu.interactable = true;
    }
    
    void Start()
    {
        
        isShoot = true;
        currentBullet = maxBullet;
        ammoDisplay.text = currentBullet.ToString();
    }
    
    
    IEnumerator FireAnimation()
    {
        animator.SetBool("Fire",true);
        yield return new WaitForSeconds(FireanimTime);
        animator.SetBool("Fire",false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBullet == 0)
        {
            
            isShoot = false;
            
            StartCoroutine(Reloadanim());
            
            return;
        }
        if (currentBullet == 0)
        {
            ReloadSound.Play();
        }
        if (isReloading == true)
        {
            return;
        }
           
          
    }

    IEnumerator Shoot()
    {
        ammoDisplay.text = currentBullet.ToString();
        isShoot = true;
        MuzzleFlash.SetActive(true);
        StartCoroutine(Muzzlefunction());
        Light.SetActive(true);
        StartCoroutine(FireAnimation());
        StartCoroutine(ButtonEnable());
        shootsound.Play();
        Bu.interactable = true;
        GameObject Bullet = Instantiate(BulletPrefab);
        Physics.IgnoreCollision(Bullet.GetComponent<Collider>(), BulletSpawnPoint.parent.GetComponent<Collider>());
        Bullet.transform.position = BulletSpawnPoint.position;
        Vector3 rotation = Bullet.transform.rotation.eulerAngles;
        Bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        Bullet.GetComponent<Rigidbody>().AddForce(BulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(Bullet, BulletlifeTime));
        StartCoroutine(DestroyLightAfterTime(Light, LightlifeTime));
        yield return new WaitForSeconds(FireanimTime);
        currentBullet --;
        ammoDisplay.text = currentBullet.ToString();

        
    }

    IEnumerator Reloadanim()
    {
        
        isShoot = false;
        isReloading = true;
        Dropbut.interactable = false;
        StartCoroutine(reloadf2());
        currentBullet = maxBullet;
        isReloading = false;
        yield return new WaitForSeconds(reload);
        isShoot = true;
        Dropbut.interactable = true;
        ammoDisplay.text = currentBullet.ToString();
    }
    IEnumerator reloadf2()
    {
        ReloadSound.Play();
        animator.SetBool("Recoil",true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Recoil",false);
    }
    IEnumerator Muzzlefunction()
    {
        
        yield return new WaitForSeconds(MuzzleFlashTime);
        MuzzleFlash.SetActive(false);
    }


    private IEnumerator DestroyBulletAfterTime(GameObject Bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(Bullet);
    }
    private IEnumerator DestroyLightAfterTime(GameObject Light, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find ("Light") .SetActive (false);
        
    }
    void Shootandanim()
    {
        StartCoroutine(FireAnimation());
        StartCoroutine(Shoot());
    }
   
}
