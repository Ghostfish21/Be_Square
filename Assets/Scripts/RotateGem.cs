using UnityEngine;

public class PulseGem : MonoBehaviour
{
    public float pulseSpeed = 2f; // 控制缩放和透明度变换的速度
    public float scaleAmount = 0.1f; // 控制缩放的幅度
    private Vector3 initialScale;
    private Material gemMaterial;
    private Color initialColor;

    void Start()
    {
        // 记录初始缩放和颜色
        initialScale = transform.localScale;
        gemMaterial = GetComponent<Renderer>().material;
        initialColor = gemMaterial.color;
    }

    void Update()
    {
        // 缩放效果：在原始大小基础上微弱地缩放
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
        transform.localScale = initialScale * scale;

        // 透明度闪烁效果
        float alpha = 0.5f + Mathf.Sin(Time.time * pulseSpeed) * 0.5f;
        Color color = initialColor;
        color.a = alpha;
        gemMaterial.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否与玩家碰撞
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // 碰撞后销毁 gem
        }
    }
}
