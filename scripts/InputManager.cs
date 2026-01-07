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

    public GameObject clickedObject;


    public event Action OnClicked, OnExited , OnRotate;
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    OnClicked?.Invoke();
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExited?.Invoke();
        }
    }
    public void Onclicked()
    {
        print("++++[onclicked================");
        OnClicked?.Invoke();
    }
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    public Vector3 getSelectedMapPostion()
    {
        Vector3 mausePos = Input.mousePosition;
        mausePos.z = SceneCamera.nearClipPlane;
        Ray ray = SceneCamera.ScreenPointToRay(mausePos);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider.transform.parent.CompareTag("object"))
                {
                    clickedObject = hit.collider.transform.parent.gameObject;
                }
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if (clickedObject != null && Physics.Raycast(ray,out hit , 100,PlacementLayerMask))
            {
                LastPosition = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            clickedObject = null;
        }
        return LastPosition;
    }
}