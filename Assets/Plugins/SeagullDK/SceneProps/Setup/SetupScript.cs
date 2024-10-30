#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace SeagullDK.SceneProps.Setup {
    public static class SetupScript {
        
        [MenuItem("Fries/Setup for Built-in Rendering Pipeline")]
        public static void setup() {
            // 创建 Fries Props Manager
            GameObject friesManager = GameObject.Find("Fries Props Manager");
            if (friesManager == null) {
                friesManager = new GameObject("Fries Props Manager");
                friesManager.AddComponent<FriesManagerBRP>();
            }
            
            if (!friesManager.GetComponent<FriesManagerBRP>()) 
                Debug.LogError("You have an invalid Fries Props Manager in the scene. Please delete it and try again.");
            
            // 清除当前的选择
            Selection.activeGameObject = null;
            // 设置当前对象为选中状态
            Selection.activeGameObject = friesManager;
            // 也可以将视图聚焦到该对象上
            EditorGUIUtility.PingObject(friesManager);
        }
        
        [MenuItem("Fries/Setup for Universal Rendering Pipeline")]
        public static void setup1() {
            // 创建 Fries Initializer
            GameObject friesManager = GameObject.Find("Fries Props Manager");
            if (friesManager == null) {
                friesManager = new GameObject("Fries Props Manager");
                friesManager.AddComponent<FriesManagerURP>();
            }
            
            if (!friesManager.GetComponent<FriesManagerURP>()) 
                Debug.LogError("You have an invalid Fries Props Manager in the scene. Please delete it and try again.");
            
            // 清除当前的选择
            Selection.activeGameObject = null;
            // 设置当前对象为选中状态
            Selection.activeGameObject = friesManager;
            // 也可以将视图聚焦到该对象上
            EditorGUIUtility.PingObject(friesManager);
        }
    }
}

#endif

namespace Yurei_Sakana.Interior_01.Setup {
    public static class Config {
        public static readonly int DefaultLayer = 18;
    }
}