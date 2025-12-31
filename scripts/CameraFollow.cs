using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    Vector3 lastPos;

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    void HandleDrag()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;
            Vector3 move = new Vector3(-delta.x * moveSpeed * Time.deltaTime, 0,-delta.y * moveSpeed * Time.deltaTime);
            transform.Translate(move, Space.World);
            lastPos = Input.mousePosition;
        }
    }
    void HandleZoom()
    {
        //// Mobile pinch
        //if (Input.touchCount == 2)
        //{
        //    Touch t0 = Input.GetTouch(0);
        //    Touch t1 = Input.GetTouch(1);

        //    float prevDist = (t0.position - t0.deltaPosition -
        //                      (t1.position - t1.deltaPosition)).magnitude;
        //    float currDist = (t0.position - t1.position).magnitude;

        //    float diff = prevDist - currDist;

        //    transform.position += transform.forward * diff * zoomSpeed * Time.deltaTime;
        //}

        // PC scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * zoomSpeed;

        ClampZoom();
    }

    void ClampZoom()
    {
        float y = transform.position.y;
        y = Mathf.Clamp(y, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
