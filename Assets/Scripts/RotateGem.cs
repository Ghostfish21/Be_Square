using UnityEngine;

public class RotateGem : MonoBehaviour
{
    public float rotationSpeed = 100f; // ������ת�ٶ�

    void Update()
    {
        // ���� XY ƽ����ת gem
        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0);
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
