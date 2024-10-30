using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DefaultNamespace {
    public class PlayerController : MonoBehaviour {
        private static PlayerController pc;
        public static PlayerController inst => pc;

        public Rigidbody rigidbody { get; private set; }

        public float speed;

        private void Awake() {
            pc = this;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            float xAxis = Input.GetAxis("Horizontal");
            Debug.Log(xAxis);
            rigidbody.AddForce(transform.right * xAxis * Time.deltaTime * 100 * speed);
        }

        private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotationTweener;
        
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Portal")) return;
            Portal p = other.gameObject.getComponent<Portal>();
            if (p.teleportSign) {
                p.teleportSign = false;
                return;
            }

            p.connectedPortal.teleportSign = true;
            rigidbody.MovePosition(p.connectedPortal.transform.position);
            Physics.gravity = p.newGravity.getDelta();
            if (rotationTweener != null && rotationTweener.IsPlaying()) rotationTweener.Kill();
            rotationTweener = transform.DORotate(p.newGravity.getFacing(), 0.65f);
        }
    }
}