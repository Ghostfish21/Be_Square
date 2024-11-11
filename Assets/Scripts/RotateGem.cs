using UnityEngine;

public class RotateGem : MonoBehaviour
{
    public float pulseSpeed = 2f; // Controls the pulsing speed
    public float scaleAmount = 0.1f; // Controls the scale amount
    public ParticleSystem gemDestroyEffect; // Particle effect prefab to play upon destruction
    private Vector3 initialScale;
    private Material gemMaterial;
    private Color initialColor;

    void Start()
    {
        // Record initial scale and color
        initialScale = transform.localScale;
        gemMaterial = GetComponent<Renderer>().material;
        initialColor = gemMaterial.color;
    }

    void Update()
    {
        // Scaling effect: slightly pulse based on the initial size
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
        transform.localScale = initialScale * scale;

        // Alpha pulsing effect
        float alpha = 0.5f + Mathf.Sin(Time.time * pulseSpeed) * 0.5f;
        Color color = initialColor;
        color.a = alpha;
        gemMaterial.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with player
        if (other.CompareTag("Player"))
        {
            // Play particle effect at the gem's position and rotation
            if (gemDestroyEffect != null)
            {
                Instantiate(gemDestroyEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Destroy gem after collision
        }
    }
}
