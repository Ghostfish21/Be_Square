using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DefaultNamespace {
    public class PlayerController : MonoBehaviour {
        private static PlayerController pc;
        public static PlayerController inst => pc;

        public Rigidbody rigidbody { get; private set; }

        private void Awake() {
            pc = this;
            rigidbody = GetComponent<Rigidbody>();
        }

        private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotationTweener;
        
        
    }
}