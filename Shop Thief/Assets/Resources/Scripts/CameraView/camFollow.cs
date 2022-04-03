using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookPlayer = false;
    private Vector3 _camera;
    public Transform Player;
    public Camera cam;

    int uploadSet;

    void Start()
    {
        _camera = transform.position - Player.position;
    }

    void FixedUpdate()
    {
        Vector3 newPos = Player.position + _camera;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookPlayer) transform.LookAt(Player);
    }
}
