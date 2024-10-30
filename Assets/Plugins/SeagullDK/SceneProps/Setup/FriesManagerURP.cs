using System;
using System.Collections.Generic;
using SeagullDK.Inspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
#endif

namespace SeagullDK.SceneProps.Setup {
    public class FriesManagerURP : MonoBehaviour {
        [Tooltip("Post Process effect including glowing will only show to these cameras")]
        public List<Camera> gameCameras = new();
        
        [AButton("Initialize")] [IgnoreInInspector]
        public Action initialize;

        private void Reset() {
            initialize = init;
        }

        private void init() {
            if (gameCameras == null || gameCameras.Count == 0) {
                Debug.LogError("Please provide at least 1 valid camera to Game Cameras field.");
                return;
            }

            VolumeProfile vp = Resources.Load<VolumeProfile>("Fries Seagull/Scene Props/URP Post Process Profile");
            
            foreach (var camera in gameCameras) {
                Volume volume = camera.GetComponent<Volume>();
                if (volume) volume.sharedProfile = vp;
                else {
                    volume = camera.gameObject.AddComponent<Volume>();
                    volume.sharedProfile = vp;
                }
            }
            Debug.Log($"Init post-processor settings for Universal Rendering Pipeline successfully.");
        }

    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(FriesManagerURP))]
    public class FriesManagerURPInspector : AnInspector { }
    #endif
}