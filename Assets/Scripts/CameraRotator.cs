using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Modules.Tween.Scripts;
using UnityEngine;

public class CameraRotator : MonoBehaviour {
    private static CameraRotator cr;
    public static CameraRotator inst => cr;

    private void Awake() {
        cr = this;
    }

    public Vector3 cameraMinRotation;
    public Vector3 cameraMaxRotation;
    public Vector3 playerMinPosition;
    public Vector3 playerMaxPosition;

    public Transform playerTransform;

    public bool stopUpdating = false;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTween;
    
    private void Update() {
        if (stopUpdating) return;
        
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
        float rotationX = Mathf.LerpAngle(cameraMinRotation.x, cameraMaxRotation.x, progressX);
        float rotationY = Mathf.LerpAngle(cameraMinRotation.y, cameraMaxRotation.y, progressY);
        float rotationZ = transform.eulerAngles.z; // 如果不需要调整 z 轴，可以保持不变

        // 设置摄像机的旋转
        transform.eulerAngles = new Vector3(rotationY, rotationX, rotationZ);
    }

    private Vector3 calculateEndRotate(Vector3 currentRotation, Vector3 playerPos, float progress) {
        Vector3 endRot = calculateEndRotate(playerPos);
        
        // 计算摄像机的旋转角度
        float rotationX = Mathf.LerpAngle(currentRotation.x, endRot.x, progress);
        float rotationY = Mathf.LerpAngle(currentRotation.y, endRot.y, progress);
        float rotationZ = transform.eulerAngles.z; // 如果不需要调整 z 轴，可以保持不变

        // 设置摄像机的旋转
        Vector3 endPos = new Vector3(rotationX, rotationY, rotationZ);
        return endPos;
    }

    private Vector3 calculateEndRotate(Vector3 playerPos) {
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
        float rotationX = Mathf.LerpAngle(cameraMinRotation.x, cameraMaxRotation.x, progressX);
        float rotationY = Mathf.LerpAngle(cameraMinRotation.y, cameraMaxRotation.y, progressY);
        float rotationZ = transform.eulerAngles.z; // 如果不需要调整 z 轴，可以保持不变

        // 设置摄像机的旋转
        Vector3 endPos = new Vector3(rotationY, rotationX, rotationZ);
        return endPos;
    }

    private float tweenValue;
    private TweenerCore<float, float, FloatOptions> tweener;
    
    public void playTween(Transform playerTrans) {
        stopUpdating = true;
        
        if (tweener != null && tweener.IsActive() && tweener.IsPlaying()) tweener.Kill();

        Vector3 startRotate = transform.eulerAngles;

        tweenValue = 0;
        tweener = DOTween.To(() => tweenValue, value => {
            tweenValue = value;
            transform.eulerAngles = calculateEndRotate(startRotate, playerTrans.position, tweenValue);
        }, 1f, 1f).OnComplete(() => {
            stopUpdating = false;
        });

    }
}
