using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light lightSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightSource.intensity = Mathf.PerlinNoise(Time.time, 2);
    }
}
