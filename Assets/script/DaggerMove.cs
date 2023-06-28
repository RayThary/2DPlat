using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DaggerMove : MonoBehaviour
{

    [SerializeField] private float daggerSpeed = 4.0f;
  

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * daggerSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right*-1 * daggerSpeed * Time.deltaTime);
        }
    }
}
