using System.Collections.Concurrent;
using UnityEngine;

namespace SeagullDK {
    public class PrefabParameters {

        private static PrefabParameters prefabParameters;
        public static PrefabParameters inst() {
            if (prefabParameters == null) prefabParameters = new PrefabParameters();
            return prefabParameters;
        }
    
        private static int idCounter = 1;

        private readonly ConcurrentDictionary<int, object[]> parameters;
    
        public PrefabParameters() {
            parameters = new ConcurrentDictionary<int, object[]>();
        }

        public static GameObject initPrefab(GameObject prefab, Transform parent, params object[] param) {
            GameObject go = GameObject.Instantiate(prefab, parent);
            PrefabParameters pp = inst();
            pp.parameters.TryAdd(go.GetInstanceID(), param);
            return go;
        }
    
        public static object[] getParameters(GameObject go) {
            PrefabParameters pp = inst();
            pp.parameters.TryGetValue(go.GetInstanceID(), out object[] param);
            return param;
        }
    
    }
}
