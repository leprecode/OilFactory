using UnityEngine;

public class TapHandler : MonoBehaviour
{
    public delegate void OnTapHandler();
    public static event OnTapHandler OnFactoryTapped;

    [SerializeField] private LayerMask _touchMask;
    [SerializeField] private Camera _camera;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _touchMask))
            {
                OnFactoryTapped?.Invoke();
                Debug.Log("!");
            }
        }
    }
}