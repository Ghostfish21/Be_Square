using System;
using UnityEngine;

namespace DefaultNamespace {
    public class PlayerTrigger : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Clickable")) return;
            other.gameObject.getComponent<Clickable>().canClick = true;
            other.gameObject.getComponent<Clickable>().showLetter();
        }

        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Clickable")) return;
            other.gameObject.getComponent<Clickable>().canClick = false;
            other.gameObject.getComponent<Clickable>().hideLetter();
        }
    }
}