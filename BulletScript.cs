using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    //public float health;
    public float damage = 20f;
    public Transform BulletSpawnPoint;
    //public float bulletSpeed;
    public GameObject BulletPrefab;
    public float bulletImpactForce = 10f;
    //public Animator run;
    public ParticleSystem ParticleImpactEF;
    public Transform ForceDirection;
    //public Vector3 spawnHere;


    void update()
    {
        //BulletSpawn.GetComponent<Transform>().position;

        //Destroy(ParticleImpactEF, 1f);
    }
    void start()
    {
        ParticleImpactEF.gameObject.SetActive(false);
        //run = GetComponent<Animator> ();
        //GetComponent<Animator>().enabled = false;
        //run.enabled = true;
    }
    void ImpactEffect()
    {
        ParticleImpactEF.transform.parent = null;
        ParticleImpactEF.Play();
        Destroy(ParticleImpactEF.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider target)
    {
        ParticleImpactEF.gameObject.SetActive(true);
        //Destroy(ParticleImpactEF.gameObject, 1f);
        Debug.Log("s");
        ImpactEffect();
        //ParticleSystem SpawnwedEF = Instantiate(ParticleImpactEF, transform.position, transform.rotation);
        //SpawnwedEF.Play();
        //Destroy(SpawnwedEF, 2f);
        //Destroy(ImpactEffect, 2f);
        if (target.transform.tag == "Enemyy")
        {
            Debug.Log("Enemy");
            //Instantiate(ParticleImpactEF, target.transform.position, Quaternion.identity);
            //target.GetComponent<Rigidbody>().AddForce(BulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
            target.GetComponent<Rigidbody>().AddForce(ForceDirection.transform.forward * bulletImpactForce, ForceMode.Impulse);
            //target.GetComponent<Enemy>().health -= damage;
            Debug.Log("hit" + target.name + "!");
            Destroy(BulletPrefab);
            //GetComponent<Enemy>();
            EnemyDie enemy = target.transform.GetComponent<EnemyDie>();



            if (enemy != null)
            {
                //target.GetComponent<Rigidbody>().AddForce(BulletSpawnPoint.forward * bulletImpactForce, ForceMode.Impulse);
                //impact();
                Collider[] colliders = Physics.OverlapSphere(target.transform.position, 50f);
                /*foreach (Collider rb in colliders)
                {
                    Rigidbody rigidbody = rb.GetComponent<Rigidbody>();
                    /*if (rigidbody != null)
                    {
                        //EnemyDie enemy = target.transform.GetComponent<EnemyDie>();
                        target.GetComponent<Rigidbody>().GetComponent<Rigidbody>().AddExplosionForce(500f, target.transform.position, 50f);
                        //enemy.die();
                    }*/
                //}
   
                enemy.die();
            }
        }
        if (target.transform.tag == "Ground")
        {
            Debug.Log("HitGround");
            //spawnHere = new Vector3 (Collider.transform.position)
            //Instantiate(ParticleImpactEF, spawnHere, target.transform.rotation);
            //Instantiate(ParticleImpactEF, target.transform.position, target.transform.rotation);
            //Destroy(BulletPrefab);
            //Instantiate(ParticleImpactEF, target.transform.position, Quaternion.identity);

        }


        else
        {

            Debug.Log("NOTHit");
        }

    }
    void impact()
    {
        //Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = Physics.OverlapSphere(BulletSpawnPoint.position ,50f);
        foreach (Collider rb in colliders) 
        {
            Rigidbody rigidbody = rb.GetComponent<Rigidbody>();
            if(rigidbody !=null)
            {
                //EnemyDie enemy = target.transform.GetComponent<EnemyDie>();
                //rigidbody.AddExplosionForce(500f, BulletSpawnPoint.position, 50f);
                rigidbody.AddForce(BulletSpawnPoint.forward * 50f, ForceMode.Impulse);
                //enemy.die();
            }
        }  
    }
}
