using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // 1
    [SerializeField]
    private Camera SceneCamera;

    private Vector3 LastPosition;

    [SerializeField]
    private LayerMask PlacementLayerMask;


    public event Action OnClicked, OnExited;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnExited?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        =>EventSystem.current.IsPointerOverGameObject();
    public Vector3 getSelectedMapPostion()
    {
        Vector3 mausePos = Input.mousePosition;
        mausePos.z = SceneCamera.nearClipPlane;
        Ray ray = SceneCamera.ScreenPointToRay(mausePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, PlacementLayerMask))
        {
            LastPosition = hit.point;
        }

        return LastPosition;
    }           
}
