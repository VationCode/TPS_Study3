using UnityEngine;

public class LookUI : MonoBehaviour
{
    private Camera _cam;
    void Start()
    {
        if(_cam == null )
        {
            _cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_cam != null)
        {
            transform.LookAt(transform.position + _cam.transform.rotation * Vector3.forward, 
                _cam.transform.rotation * Vector3.up);
        }
    }
}
