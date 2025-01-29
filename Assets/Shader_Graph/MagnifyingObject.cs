using UnityEngine;

public class MagnifyingObject : MonoBehaviour
{
    Renderer _renderer;
    Camera _cam;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _cam = Camera.main;
    }

    void Update()
    {
        Vector3 ScreenPoint = _cam.WorldToScreenPoint(transform.position);
        ScreenPoint.x = ScreenPoint.x / Screen.width;
        ScreenPoint.y = ScreenPoint.y / Screen.height;
        _renderer.material.SetVector("_ObjScreenPos", ScreenPoint);
    }
}
