using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    previewSystem previewSystem;
    GridData data;
    ObjectPlacer objectPlacer;

    public RemovingState(Grid grid, previewSystem previewSystem, GridData data,
                            ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.data = data;

        this.objectPlacer = objectPlacer;
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (data.CanPlaceObejctAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = data;
        }


        if (selectedData != null)
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }

        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, true);
    }
    //private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    //{
    //    return !(data.CanPlaceObejctAt(gridPosition, Vector2Int.one) &&
    //        data.CanPlaceObejctAt(gridPosition, Vector2Int.one));
    //}
    public void UpdateState(Vector3Int gridPosition)
    {
        //bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), true);
    }
}