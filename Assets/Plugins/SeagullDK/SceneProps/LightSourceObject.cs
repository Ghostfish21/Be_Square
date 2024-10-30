﻿using System;
using System.Collections.Generic;
using SeagullDK.Inspector;
using SeagullDK.SceneProps.Setup;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SeagullDK.SceneProps {
    [Serializable]
    public class String2GlowLight : KiiValuePair<string, GlowLight> {}

    public class LightSourceObject : MonoBehaviour {
        public List<String2GlowLight> lights = new();
        private Dictionary<string, GlowLight> lightMap = new();
        
        [AButton("Turn On")] public UnityEvent onTurnOn;
        [AButton("Turn Off")] public UnityEvent onTurnOff;
        
        private void Awake() {
            lights.ForEach(light => lightMap[light.key] = light.value);
            onTurnOn.AddListener(turnOnAll);
            onTurnOff.AddListener(turnOffAll);
        }

        public void turnOnAll() {
            foreach (var lightMapValue in lightMap.Values) lightMapValue.turnOn();
        }

        public void turnOffAll() {
            foreach (var light in lightMap.Values) light.turnOff();
        }

        public void turnOn(string key) {
            lightMap[key].turnOn();
        }

        public void turnOff(string key) {
            lightMap[key].turnOff();
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(LightSourceObject))]
    public class LightSourceObjectInspector : AnInspector { }
#endif
}