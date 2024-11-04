using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace {
    public class StageMover : MonoBehaviour {
        private GameObject toMove;
        
        public Vector2 stageMinPosition;
        public Vector2 stageMaxPosition;
        public Vector2 playerMinPosition;
        public Vector2 playerMaxPosition;

        public Transform playerTransform;

        public bool stopUpdating = false;
        private TweenerCore<float, float, FloatOptions> tweener;
        
        private void Update() {
            if (stopUpdating) return;
            
            transform.localPosition = calculateEndPos();
        }

        private Vector3 calculateEndPos() {
            // 计算 x 方向上的进度
            float totalX = playerMaxPosition.x - playerMinPosition.x;
            float currentX = playerTransform.position.x - playerMinPosition.x;
            float progressX = totalX != 0 ? currentX / totalX : 0f;
            progressX = Mathf.Clamp01(progressX);

            // 计算 y 方向上的进度
            float totalY = playerMaxPosition.y - playerMinPosition.y;
            float currentY = playerTransform.position.y - playerMinPosition.y;
            float progressY = totalY != 0 ? currentY / totalY : 0f;
            progressY = Mathf.Clamp01(progressY);

            // 计算摄像机的旋转角度
            float posX = Mathf.LerpAngle(stageMinPosition.x, stageMaxPosition.x, progressX);
            float posY = Mathf.LerpAngle(stageMinPosition.y, stageMaxPosition.y, progressY);
            float posZ = transform.localPosition.z; // 如果不需要调整 z 轴，可以保持不变
            //uwu 0.o

            // 设置摄像机的旋转
            return new Vector3(posX, posY, posZ);
        }
        
        private Vector3 calculateEndPos(Vector3 playerPos) {
            // 计算 x 方向上的进度
            float totalX = playerMaxPosition.x - playerMinPosition.x;
            float currentX = playerPos.x - playerMinPosition.x;
            float progressX = totalX != 0 ? currentX / totalX : 0f;
            progressX = Mathf.Clamp01(progressX);

            // 计算 y 方向上的进度
            float totalY = playerMaxPosition.y - playerMinPosition.y;
            float currentY = playerPos.y - playerMinPosition.y;
            float progressY = totalY != 0 ? currentY / totalY : 0f;
            progressY = Mathf.Clamp01(progressY);

            // 计算摄像机的旋转角度
            float posX = Mathf.LerpAngle(stageMinPosition.x, stageMaxPosition.x, progressX);
            float posY = Mathf.LerpAngle(stageMinPosition.y, stageMaxPosition.y, progressY);
            float posZ = transform.localPosition.z; // 如果不需要调整 z 轴，可以保持不变
            //uwu 0.o

            // 设置摄像机的旋转
            return new Vector3(posX, posY, posZ);
        }

        private float tweenValue;
        public void playTween() {
            stopUpdating = true;
        
            if (tweener != null && tweener.IsActive() && tweener.IsPlaying()) tweener.Kill();

            Vector3 startLocalPos = transform.localPosition;

            tweenValue = 0;
            tweener = DOTween.To(() => tweenValue, value => {
                tweenValue = value;
                Vector3 endPos = calculateEndPos(playerTransform.position);
                transform.localPosition = new Vector3(Mathf.Lerp(startLocalPos.x, endPos.x, tweenValue),
                    Mathf.Lerp(startLocalPos.y, endPos.y, tweenValue),
                    startLocalPos.z);
            }, 1f, 1f).OnComplete(() => {
                stopUpdating = false;
            });
        }
    }
}