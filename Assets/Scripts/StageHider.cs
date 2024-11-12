using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SeagullDK.TaskPerformer;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StageHider : MonoBehaviour {
    public Vector3 animStartScale;
    public Vector3 animEndScale;

    [ColorUsage(false, true)]
    public Color showColor;
    [ColorUsage(false, true)]
    public Color hideColor;
    
    public Vector2 showTime;
    public Vector2 hideTime;

    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool isShowing = true;
    
    // Start is called before the first frame update
    private void Start() {
        collider = GetComponent<Collider>();
        
        waitForHide();
    }

    private void waitForHide() {
        TaskPerformer.inst().scheduleTask((Action)(() => {
            isShowing = false;
            transform.DOScale(animEndScale, 0.3f).OnComplete(() => { transform.DOScale(animStartScale, 0.3f); });
            spriteRenderer.DOColor(hideColor, 0.6f).OnComplete(() => { collider.enabled = false; });
            waitForShow();
        }), Random.Range(showTime.x, showTime.y));
    }

    private void waitForShow() {
        TaskPerformer.inst().scheduleTask((Action)(() => {
            isShowing = false;
            transform.DOScale(animEndScale, 0.3f).OnComplete(() => { transform.DOScale(animStartScale, 0.3f); });
            spriteRenderer.DOColor(showColor, 0.6f).OnComplete(() => { collider.enabled = false; });
            waitForHide();
        }), Random.Range(hideTime.x, hideTime.y));
    }
}
