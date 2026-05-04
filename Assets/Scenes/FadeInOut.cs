using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    //[SerializeField] PlayableDirector playableDirector;
    //PlayableDirector playableDirector;
    //[SerializeField] PlayableAsset playableAsset;
    [SerializeField] Image fadeImage;
    Animator animator;
    public static FadeInOut Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //fadeImage.gameObject.SetActive(false);
        //playableDirector = fadeImage.GetComponent<PlayableDirector>();
        //playableDirector.enabled = true;
        animator = fadeImage.GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    public void Play(string trigger)
    {
        animator.enabled = true;
        fadeImage.gameObject.SetActive(true);
        animator.SetTrigger(trigger);
        //playableDirector.Play(playableAsset);
    }

    public void Stop()
    {
        fadeImage.gameObject.SetActive(false);
        animator.enabled = false;
    }
        
    
}

