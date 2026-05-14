using UnityEngine;

public class ChoppedAnimation : MonoBehaviour
{
    [SerializeField] float animationFPS = 12f;
    Animator animator;
    float accumulator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 1f / animationFPS;
        accumulator += Time.deltaTime;
        if (accumulator >= step)
        {
            animator.Update(accumulator);
            accumulator = 0f;
        }
    }
}
