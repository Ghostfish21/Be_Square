using UnityEngine;

public class PulseGem : MonoBehaviour
{
    public float pulseSpeed = 2f; // �������ź�͸���ȱ任���ٶ�
    public float scaleAmount = 0.1f; // �������ŵķ���
    private Vector3 initialScale;
    private Material gemMaterial;
    private Color initialColor;

    void Start()
    {
        // ��¼��ʼ���ź���ɫ
        initialScale = transform.localScale;
        gemMaterial = GetComponent<Renderer>().material;
        initialColor = gemMaterial.color;
    }

    void Update()
    {
        // ����Ч������ԭʼ��С������΢��������
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
        transform.localScale = initialScale * scale;

        // ͸������˸Ч��
        float alpha = 0.5f + Mathf.Sin(Time.time * pulseSpeed) * 0.5f;
        Color color = initialColor;
        color.a = alpha;
        gemMaterial.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ��������ײ
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // ��ײ������ gem
        }
    }
}
