using System;
using UnityEngine;

public class PlaceMentSystem : MonoBehaviour
{
   
    [SerializeField] // 1
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField] // 1
    private InputManager inputManager;

    [SerializeField] // 2
    private Grid Grid;

    [SerializeField]
    private ObjectsDatabaseOS objectsDatabase;

    private int selectedObjectId = -1;
    [SerializeField]
    private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
        
    }
    public void StartPlacement(int ID)
    {

        selectedObjectId = objectsDatabase.objectDatas.FindIndex(data => data.Id == ID);
        if (selectedObjectId < 0)
        {
            Debug.LogError("Object with ID " + ID + " not found in database.");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExited += StopPlacement;
    }

    private void PlaceStructure()
    {
      if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 selectedPos = inputManager.getSelectedMapPostion(); // 1
        Vector3Int cellPos = Grid.WorldToCell(selectedPos); // 2
        GameObject prefab = Instantiate(objectsDatabase.objectDatas[selectedObjectId].Prefab);
        prefab.transform.position = Grid.CellToWorld(cellPos);
    }

    private void StopPlacement()
    {
        selectedObjectId = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExited -= StopPlacement;
       

    }

    private void Update()
    {
        Vector3 selectedPos = inputManager.getSelectedMapPostion(); // 1
        Vector3Int cellPos = Grid.WorldToCell(selectedPos); // 2
        mouseIndicator.transform.position = selectedPos;// 1
        cellIndicator.transform.position = Grid.CellToWorld(cellPos);// 2
        //Vector3 selectedPos = inputManager.getSelectedMapPostion();
        //Vector3Int cellPos = Grid.WorldToCell(selectedPos);

        //mouseIndicator.transform.position = selectedPos;

        //Vector3 cellWorldPos = Grid.CellToWorld(cellPos);
        //Vector3 cellCenter = cellWorldPos + new Vector3(
        //    Grid.cellSize.x / 2f,
        //    0,
        //    Grid.cellSize.y / 2f
        //);

        //cellIndicator.transform.position = cellCenter;
        //cellIndicator.transform.localScale = new Vector3(
        //    Grid.cellSize.x,
        //    1,
        //    Grid.cellSize.y
        //);
    }
}
