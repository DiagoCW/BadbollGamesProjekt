using UnityEngine;

public class LerpLight : MonoBehaviour
{
    [SerializeField]Light spotLight;
    Color startColor;
    [SerializeField] float intensity = 5f;
    void Start()
    {
        startColor = spotLight.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LerpColor(intensity));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LerpColor(0f));
        }
    }

    void LerpBackColor()
    {
        float lerp = 2f;
        spotLight.intensity = Mathf.Lerp(spotLight.intensity, 0f, Time.deltaTime * lerp);
    }

    void LerpInColor()
    {
        float lerp = 2f;
        spotLight.intensity = Mathf.Lerp(spotLight.intensity, intensity, Time.deltaTime * lerp);
    }


    System.Collections.IEnumerator LerpColor(float targetIntensity)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        //Color targetColor = Color.red;
        while (elapsedTime < duration)
        {
            spotLight.intensity = Mathf.Lerp(spotLight.intensity, targetIntensity, Time.deltaTime * duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spotLight.intensity = targetIntensity; 
        //spotLight.color = targetColor; 
    }

}
