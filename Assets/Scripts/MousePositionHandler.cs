using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionHandler : MonoBehaviour {
    private Vector3 _mousePosition;
    private Ray ray;
    private Plane hPlane;

    public static MousePositionHandler Instance;

    private void Awake() {
        Instance = this;
    }

    void Update () {
        //ray to mouse position in world
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //create a plane using the up vector
        hPlane = new Plane(Vector3.up, Vector3.zero);
        float dist = 0f;
        if(hPlane.Raycast(ray, out dist)) {
            _mousePosition = ray.GetPoint(dist);
        }else {
            _mousePosition = Vector3.zero;
        }
    }

    public Vector3 GetMousePosition() {
        return _mousePosition;
    }
}
