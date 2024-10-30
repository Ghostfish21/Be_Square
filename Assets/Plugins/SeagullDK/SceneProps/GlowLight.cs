using System;
using UnityEngine;

namespace SeagullDK.SceneProps {
    public class GlowLight : MonoBehaviour {
        private MeshRenderer lightBoxRenderer;
        public Color activeLightSourceSurfaceColor;
        public Color inactiveLightSourceSurfaceColor;
        [ColorUsage(false, true)]
        public Color lightEmissionColor;

        private void Awake() {
            lightBoxRenderer = GetComponent<MeshRenderer>();
            turnOff();
        }

        public virtual void turnOn() {
            lightBoxRenderer.material.color = activeLightSourceSurfaceColor;
            lightBoxRenderer.material.SetColor("_EmissionColor", lightEmissionColor);
            lightBoxRenderer.material.EnableKeyword("_EMISSION");
        }
        
        public virtual void turnOff() {
            lightBoxRenderer.material.color = inactiveLightSourceSurfaceColor;
            lightBoxRenderer.material.DisableKeyword("_EMISSION");
        }
        
        public virtual void turnOn(Color lightSourceSurfaceColor, Color lightEmissionColor, float lightEmissionIntensity) {
            lightBoxRenderer.material.color = lightSourceSurfaceColor;
            lightBoxRenderer.material.SetColor("_EmissionColor", lightEmissionColor * lightEmissionIntensity);
            lightBoxRenderer.material.EnableKeyword("_EMISSION");
        }

        public virtual void turnOff(Color inactiveLightSourceSurfaceColor) {
            lightBoxRenderer.material.color = inactiveLightSourceSurfaceColor;
            lightBoxRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}