using UnityEngine;

namespace DefaultNamespace {
    public enum GravityDirection {
        up, down, left, right
    }

    public static class GravityDirectionExt {
        public static Vector3 getDelta(this GravityDirection gravityDirection) {
            return gravityDirection switch {
                GravityDirection.up => new Vector3(0, 9.8f, 0),
                GravityDirection.down => new Vector3(0, -9.8f, 0),
                GravityDirection.left => new Vector3(-9.8f, 0, 0),
                GravityDirection.right => new Vector3(9.8f, 0, 0),
                _ => new Vector3(-1, -1, -1)
            };
        }

        public static Vector3 getFacing(this GravityDirection gravityDirection) {
            return gravityDirection switch {
                GravityDirection.down => new Vector3(0, 0, 0),
                GravityDirection.up => new Vector3(0, 180f, 0),
                GravityDirection.left => new Vector3(0, 0, -90),
                GravityDirection.right => new Vector3(0, 0, 90),
                _ => new Vector3(0,0,0)
            };
        }
    }
    
    public class Portal : MonoBehaviour {
        public Portal connectedPortal;
        public GravityDirection newGravity;

        public bool teleportSign = false;
    }
}