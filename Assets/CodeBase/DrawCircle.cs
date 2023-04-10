using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle: MonoBehaviour {
    [SerializeField] private float _thetaScale = 0.01f;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private int _size;
    [SerializeField] private LineRenderer _lineDrawer;
    [SerializeField] private float _theta = 0f;

    private void Start() {
        _lineDrawer = GetComponent<LineRenderer>();
    }

    [System.Obsolete]
    void Update() {
        _theta = 0f;
        _size = (int)((1f / _thetaScale) + 1f);
        _lineDrawer.SetVertexCount(_size);
        for (int i = 0; i < _size; i++) {
            _theta += (2.0f * Mathf.PI * _thetaScale);
            float x = transform.position.x + _radius * Mathf.Cos(_theta);
            float y =  transform.position.y + _radius * Mathf.Sin(_theta);
            _lineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}