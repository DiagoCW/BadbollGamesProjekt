using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RainCollision : MonoBehaviour
{
    //New Splashsystem is in scene to prevent too many clones
    public ParticleSystem[] splashes;
    private int currentSplash = 0;

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
            //exact point where the raindrop hit
            Vector3 hitPos = rainCollision[i].intersection;
            Vector3 hitNormal = rainCollision[i].normal;

            //Reuse splashes instead of creating new ones - prevent clones
            ParticleSystem splash = splashes[currentSplash];

            //Move splash to where the raindrops hit
            splash.transform.position = hitPos;
            splash.transform.rotation = Quaternion.LookRotation(hitNormal);
            splash.Play();

            currentSplash = (currentSplash + 1) % splashes.Length;
        }
    }
}
