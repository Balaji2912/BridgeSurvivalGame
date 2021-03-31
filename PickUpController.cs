using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    public BulletGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, GunHolder, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    public Button DropGun;
    public Button pickGun;
    public Button ShootButton;
    public Animator animator;
    public Camera Maincamera;

    //public BulletGun Shoot;
    //public bool isShoot;
    public void DropGunfunc()
    {

        //Drop if equipped and "Q" is pressed
        if (equipped)
        {
            //isShoot = false;
            Drop();

            //Add random rotation

            ShootButton.interactable = false;
            animator.enabled = false;
            Debug.Log("in the dropfunction");


        }

    }
    public void pickGunfunc()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(Maincamera.transform.position, Maincamera.transform.forward, out hit, 500f))
        {
            Debug.Log(hit.transform.name);
        }
        if (!equipped && hit.transform.tag == "Weapon" && distanceToPlayer.magnitude <= pickUpRange)
        {
            PickUp();
            //isShoot = true;
            ShootButton.interactable = true;
            animator.enabled = true;
        }


    }

    void Start()
    {
        //Setup
        /*if (isShoot == true)
        {
            Shoot();
        }*/
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            //BulletGun.GetComponent<isShoot>() = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        /*if(transform.SetParent(null))
        {
            ShootButton.interactable = false;
        }
        else
        {
            ShootButton.interactable = true;
        }*/
    }

    private void PickUp()
    {
        
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(GunHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        //coll.isTrigger = true;

        //Enable script
        gunScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);
        ShootButton.interactable = true;

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 2f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        gunScript.enabled = false;
        //animator.enabled = true;
    }
}
