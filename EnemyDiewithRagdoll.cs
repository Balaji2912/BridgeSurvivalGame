using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    //public float bulletImpactForce = 1000f;
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;
    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
        GetComponent<Animator>().enabled = true;
        //ragdoll = GetComponent<Rigidbody>();
        //rigidbodies = GetComponentsInChildren<Rigidbody>();


    }
    
    public void die()
    {
        
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        //GetComponent<Rigidbody>().AddExplosionForce(50f, transform.position, 10f);
        ragdollForce();
        
    }

    void setRigidbodyState(bool state)
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }


    void setColliderState(bool state)
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;

    }

    void ragdollForce()
    {
        //Rigidbody[] rigidbodies = Physics.OverlapSphere(transform.position ,50f);
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();   
        foreach (Rigidbody rb in rigidbodies) 
        {
            Rigidbody rigidbody = rb.GetComponent<Rigidbody>();
            if(rigidbody !=null)
            {
                rigidbody.AddExplosionForce(700f, BulletSpawnPoint.transform.position, 300f);
            }
        }  
    }

    

}