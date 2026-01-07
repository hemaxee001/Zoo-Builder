using System;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlaceMentSystem : MonoBehaviour
{

    //[SerializeField] // 1
    //private GameObject mouseIndicator, cellIndicator;

    [SerializeField] // 1
    private InputManager inputManager;

    [SerializeField] // 2
    private Grid Grid;

    [SerializeField]
    private ObjectsDatabaseOS objectsDatabase;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData data;

    [SerializeField]
    private previewSystem preview;

    private Vector3Int lastDeletedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    public GameObject storePanel;
    public GameObject ObjectControlPanel;

    [SerializeField]
    private LayerMask unlockLayer;

    [SerializeField]
    private GridManager GridManager;


    private void Start()
    {
        setGrid();
        StopPlacement();
        data = new();
    }
   

    public void showStorePanel()
    {
        if (storePanel.activeInHierarchy)
        {
            storePanel.SetActive(false);
            return;
        }
        storePanel.SetActive(true);
    }
    

    public void StartPlacement(int ID)
    {
        storePanel.SetActive(false);
        ObjectControlPanel.SetActive(true);
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, Grid, preview, objectsDatabase, data, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExited += StopPlacement;
        inputManager.OnRotate += RotatePreview;

        Vector3 centerPos = GridManager.GetCenterWorldPosition();
        Vector3Int centerGridPos = Grid.WorldToCell(centerPos);
        buildingState.UpdateState(centerGridPos);
    }
    private void RotatePreview()
    {
        preview.RotateObject();
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(Grid, preview, data, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExited += StopPlacement;
    }

    private void PlaceStructure()
    {
        print("-----------PlaceStructure----------");
        //if (inputManager.IsPointerOverUI())
        //{
        //    return;
        //}
        Vector3 mousePosition = inputManager.getSelectedMapPostion();
        Vector3Int gridPosition = Grid.WorldToCell(mousePosition);
        buildingState.OnAction(gridPosition);
    }

    private void StopPlacement()
    {
        if (buildingState == null)
        {
            return;
        }
        ObjectControlPanel.SetActive(false);
        gridVisualization.SetActive(false);

        buildingState.EndState();

        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExited -= StopPlacement;
        lastDeletedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null)
        {
            return;
        }

        Vector3 mousePosition = inputManager.getSelectedMapPostion();
        Vector3Int gridPosition = Grid.WorldToCell(mousePosition);

        if (lastDeletedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDeletedPosition = gridPosition;
        }
    }

    public void setGrid()
    {
        var grid = gridVisualization.transform;
        var totalSize = GridManager.width * 10;
        var offset = totalSize / 2 ;
        gridVisualization.transform.position = new Vector3(offset - 5, 0.01f, offset - 5);

    }
}