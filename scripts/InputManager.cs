using UnityEngine;

public class InputManager : MonoBehaviour
{
    // 1
    [SerializeField]
    private Camera SceneCamera;

    private Vector3 LastPosition;

    [SerializeField]
    private LayerMask PlacementLayerMask;
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
