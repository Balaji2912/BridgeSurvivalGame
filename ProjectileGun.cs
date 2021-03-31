using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;
    public float bulletSpeed = 30, damage = 20f, BulletlifeTime, LightlifeTime, FireanimTime = 0.01f, interactablebuttonTim = .5f;
    //public GameObject MuzzleFlash;
    public float maxBullet, reload = 1.5f, upwardForce, spread;//MuzzleFlashTime = 0.3f;
    //public GameObject Light;
    //public bool lighton;
    public Animator animatorr;
    public Button Bu;
    //public AudioSource shootsound, ReloadSound;
    private float currentBullet;
    private bool isShoot = true, isReloading = false;
    public Text ammoDisplay;
    //public Button Dropbut;
    public Camera Maincamera;


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
        animatorr.SetBool("Fire", true);
        yield return new WaitForSeconds(FireanimTime);
        animatorr.SetBool("Fire", false);
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
            //ReloadSound.Play();
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
        Ray ray = Maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - BulletSpawnPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);


        //MuzzleFlash.SetActive(true);
        //StartCoroutine(Muzzlefunction());
        //Light.SetActive(true);
        StartCoroutine(FireAnimation());
        StartCoroutine(ButtonEnable());
        //shootsound.Play();
        Bu.interactable = true;
        GameObject Bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity);

        Bullet.transform.forward = directionWithSpread.normalized;
        Physics.IgnoreCollision(Bullet.GetComponent<Collider>(), BulletSpawnPoint.parent.GetComponent<Collider>());
        Bullet.transform.position = BulletSpawnPoint.position;
        Vector3 rotation = Bullet.transform.rotation.eulerAngles;
        Bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        Bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bulletSpeed, ForceMode.Impulse);
        Bullet.GetComponent<Rigidbody>().AddForce(Maincamera.transform.up * upwardForce, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(Bullet, BulletlifeTime));
        //StartCoroutine(DestroyLightAfterTime(Light, LightlifeTime));
        yield return new WaitForSeconds(FireanimTime);
        currentBullet--;
        ammoDisplay.text = currentBullet.ToString();


    }

    IEnumerator Reloadanim()
    {

        isShoot = false;
        isReloading = true;
        //Dropbut.interactable = false;
        StartCoroutine(reloadf2());
        currentBullet = maxBullet;
        isReloading = false;
        yield return new WaitForSeconds(reload);
        isShoot = true;
        //Dropbut.interactable = true;
        ammoDisplay.text = currentBullet.ToString();
    }
    IEnumerator reloadf2()
    {
        //ReloadSound.Play();
        animatorr.SetBool("Reload", true);
        yield return new WaitForSeconds(1f);
        animatorr.SetBool("Reload", false);
    }
    /*IEnumerator Muzzlefunction()
    {

        yield return new WaitForSeconds(MuzzleFlashTime);
        MuzzleFlash.SetActive(false);
    }*/


    private IEnumerator DestroyBulletAfterTime(GameObject Bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(Bullet);
    }
    /*private IEnumerator DestroyLightAfterTime(GameObject Light, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find("Light").SetActive(false);

    }*/
    void Shootandanim()
    {
        //StartCoroutine(FireAnimation());
        StartCoroutine(Shoot());
    }

}
