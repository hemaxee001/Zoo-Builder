using System;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    previewSystem previewSystem;
    ObjectsDatabaseOS objectDB;
    GridData data;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD, Grid grid, previewSystem previewSystem,
        ObjectsDatabaseOS objectDB, GridData data,
        ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.objectDB = objectDB;
        this.data = data;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = objectDB.objectDatas.FindIndex(data => data.Id == ID);
        if (selectedObjectIndex > -1)
        {
            //gridVisualization.SetActive(true);
            previewSystem.StartShowingPlacementPreview(objectDB.objectDatas[selectedObjectIndex].Prefab,
                objectDB.objectDatas[selectedObjectIndex].Size);
        }
        else
        {
            throw new Exception($"No Object Withe This ID {iD}");
        }

    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (placementValidity == false)
        {
            return;
        }
        float rotationY = previewSystem.GetPreviewRotationY(); //-----------------------------------------------------
        int index = objectPlacer.PlaceObject(objectDB.objectDatas[selectedObjectIndex].Prefab,grid.CellToWorld(gridPosition),rotationY);
        //int index = objectPlacer.PlaceObject(objectDB.objectDatas[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));
        GridData selectedData = data;
        //GridData selectedData = objectDB.objectsData[selectedObjectIndex].ID == 0 || ID == 4 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, objectDB.objectDatas[selectedObjectIndex].Size,
            objectDB.objectDatas[selectedObjectIndex].Id, index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = data;
        //GridData selectedData = objectDB.objectsData[selectedObjectIndex].ID == 0||ID == 4 ? floorData : furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, objectDB.objectDatas[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        //mouseIndicator.transform.position = gridPosition;
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }


}