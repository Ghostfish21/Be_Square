using System;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentCache {
    private static Dictionary<GameObject, Dictionary<Type, Component>> components = new();

    public static T getComponent<T>(this GameObject gobj) where T : Component {
        if (!components.ContainsKey(gobj)) components[gobj] = new Dictionary<Type, Component>();
        if (!components[gobj].ContainsKey(typeof(T))) components[gobj][typeof(T)] = gobj.GetComponent<T>();
        else if (components[gobj][typeof(T)] == null) components[gobj][typeof(T)] = gobj.GetComponent<T>();
        return (T)components[gobj][typeof(T)];
    }
}