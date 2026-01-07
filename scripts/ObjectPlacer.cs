using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> PlacedGameObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 positon,float rotationY)
    {
        //GameObject newObject = Instantiate(prefab);
        //newObject.transform.position = positon;
        //PlacedGameObjects.Add(newObject);

        //return PlacedGameObjects.Count - 1;
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = positon;
        newObject.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        PlacedGameObjects.Add(newObject);
        return PlacedGameObjects.Count - 1;
    }
    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (PlacedGameObjects.Count <= gameObjectIndex
            || PlacedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(PlacedGameObjects[gameObjectIndex]);
        PlacedGameObjects[gameObjectIndex] = null;
    }

}