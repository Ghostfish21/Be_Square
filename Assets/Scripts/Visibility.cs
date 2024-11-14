using UnityEngine;

public class Visibility : MonoBehaviour
{
    public float visibleTime = 2f;  // time the object stays visible
    public float invisibleTime = 2f; // time the object stays invisible
    private float timer = 0f;
    private bool isVisible = true;
    private Renderer objectRenderer;
    private Collider objectCollider;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        SetObjectState(true); // start with object visible
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isVisible && timer >= visibleTime)
        {
            SetObjectState(false); // invisible
            timer = 0f;  // reset the timer
        }
        else if (!isVisible && timer >= invisibleTime)
        {
            SetObjectState(true); // visible
            timer = 0f;  // reset the timer
        }
    }

    void SetObjectState(bool state)
    {
        isVisible = state;
        objectRenderer.enabled = state;  //  visibility
        objectCollider.enabled = state;  //  collider
    }
}
