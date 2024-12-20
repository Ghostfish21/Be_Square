﻿using System;
using UnityEngine;

namespace SeagullDK.Pool {
    public static class PoolExt {
        public static CompPool<T> toPool<T>(this GameObject prefab, Transform root, int size = 5) where T : Component => new(root, prefab, size);
        public static CompPool<T> toPool<T>(this GameObject prefab, Action<T> resetter, Transform root, int size = 5) where T : Component => new(resetter, root, prefab, size);
    }
}