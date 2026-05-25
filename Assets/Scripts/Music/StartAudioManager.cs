using UnityEngine;

public class StartAudioManager : MonoBehaviour
{
    public static StartAudioManager Instance;

    [Header("Sources")]
    public AudioSource ambienceSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip carInteriorLoop;
    public AudioClip carCrash;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAmbience(string id)
    {
        switch (id)
        {
            case "highway-travel-interior":
                ambienceSource.clip = carInteriorLoop;
                ambienceSource.loop = true;
                ambienceSource.Play();
            break;
        }
    }

    public void StopAmbience()
    {
        ambienceSource.Stop();
    }

    public void PlaySFX(string id)
    {
        switch (id)
        {
            case "car-crash":
                sfxSource.PlayOneShot(carCrash);
            break;
        }
    }
}