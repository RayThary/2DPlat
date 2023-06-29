using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTrs;
    [SerializeField] private float CameraMove = 8.0f;

    public Vector2 center;
    public Vector2 size;
    private float m_fheight;
    private float m_fwidth;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, size);
    }
    void Start()
    {
        m_fheight= Camera.main.orthographicSize;
        m_fwidth= m_fheight*Screen.width/Screen.height;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTrs.position, Time.deltaTime * CameraMove);

        float Ix = size.x * 0.5f - m_fwidth;
        float clampX = Mathf.Clamp(transform.position.x,-Ix+ center.x, Ix + center.x);

        float Iy = size.y * 0.5f - m_fheight;
        float clampY = Mathf.Clamp(transform.position.y, -Iy+ center.y, Iy + center.y);
        transform.position =new Vector3(clampX,clampY,-10f);
    }
}
