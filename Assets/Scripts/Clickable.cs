using System;
using DG.Tweening;
using SeagullDK;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace {
    public class Clickable : MonoBehaviour {
        public bool canClick = false;
        public bool clicked = false;
        
        public UnityEvent ue;
        public GameObject outSign;
        public GameObject exitSign;

        private Rigidbody rigidBody;
        private Collider collider;

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        private void Update() {
            if (clicked) return;
            if (!canClick) return;
            if (Input.GetKeyDown(KeyCode.F)) ue.Invoke();
        }

        public void openGravity() {
            clicked = true;
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            collider.isTrigger = false;
            transform.position = transform.position.xy_(Movement.inst.rb.position.z + 2);
        }

        public void startToFall() {
            clicked = true;
            float z = Movement.inst.rb.position.z;
            Movement.inst.rb.useGravity = false;
            Movement.inst.canMove = false;
            exitSign.transform.DOScale(0, 0.3f);
            Movement.inst.rb.transform.DORotate(Movement.inst.rb.transform.eulerAngles.xy_(-90), 0.7f);
            Movement.inst.rb.transform.DOMove(this.transform.position.xy_(-3.5f), 1.5f);
            Movement.inst.rb.transform.DOScale(0.07f, 4f).OnComplete(() => {
                Movement.inst.rb.transform.DOMove(outSign.transform.position, 1f).OnComplete(
                    () => {
                        Movement.inst.rb.transform.DORotate(Movement.inst.rb.transform.eulerAngles.xy_(0f), 0.1f);
                        Movement.inst.rb.transform.DOMove(Movement.inst.rb.position.xy_(z), 0.1f);
                        Movement.inst.rb.transform.DOScale(0.7f, 0.1f);
                        Movement.inst.rb.useGravity = true;
                        Movement.inst.canMove = true;
                    });
            });
        }
    }
}