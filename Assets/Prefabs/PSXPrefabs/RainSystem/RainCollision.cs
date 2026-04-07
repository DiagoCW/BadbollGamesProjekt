using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RainCollision : MonoBehaviour
{
    public GameObject SplashSystem;
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> rainCollision;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        rainCollision = new List<ParticleCollisionEvent>();
    }

    // As soon as the rain hits somehting - function called
    void OnParticleCollision(GameObject other)
    {
        int numberofDrops = ps.GetCollisionEvents(other, rainCollision);


        //Looping through each Raindrop hit
        for (int i = 0; i < numberofDrops; i++)
        {
            //Save for later - if it lags make it randomly
            //if (Random.value > 0.3f) continue;

            //exact point where the raindrop hit
            Vector3 hitPos = rainCollision[i].intersection;
            Vector3 hitNormal = rainCollision[i].normal;

          

            //spawn splash - Quaternion = Rotates splash 
            GameObject splash = Instantiate(SplashSystem, hitPos, Quaternion.LookRotation(hitNormal));

            //Delete raindrop
            Destroy(splash, 2f);
        }
    }
}
