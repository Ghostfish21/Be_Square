using UnityEngine;

public class RotateGem : MonoBehaviour
{
    public float rotationSpeed = 100f; // 控制旋转速度

    void Update()
    {
        // 沿着 XY 平面旋转 gem
        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0);
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
