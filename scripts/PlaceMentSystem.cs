using UnityEngine;

public class PlaceMentSystem : MonoBehaviour
{
   
    [SerializeField] // 1
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField] // 1
    private InputManager inputManager;

    [SerializeField] // 2
    private Grid Grid;

    private void Update()
    {
        Vector3 selectedPos = inputManager.getSelectedMapPostion(); // 1
        Vector3Int cellPos = Grid.WorldToCell(selectedPos); // 2
        mouseIndicator.transform.position = selectedPos;// 1
        cellIndicator.transform.position = Grid.CellToWorld(cellPos);// 2

    }
}
