using System.Collections.Generic;
using UnityEngine;

public class GameObjectPath
{
    static List<(string, string)> paths = new List<(string, string)> ();

    static public void AddPath(string name, GameObject gameObject)
    {
        paths.Add((name, CreatePathString(gameObject.transform)));
    }

    //trouver sur l'aide de unity: https://discussions.unity.com/t/how-can-i-get-the-full-path-to-a-gameobject/412/2
    static string CreatePathString(Transform transform)
    {
        if (transform.parent == null)
        {
            return "/" + transform.name;
        }
        return CreatePathString(transform.parent) + "/" + transform.name;
    }

    static public string GetPath(string name)
    {
        foreach ((string, string) path in paths)
        {
            if (path.Item1 == name)
            {
                return path.Item2;
            }
        }
        return null;
    }
}
