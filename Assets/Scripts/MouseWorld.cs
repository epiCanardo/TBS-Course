using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField]
    private LayerMask mousePlaneLayerMask;

    private static MouseWorld instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position = MouseWorld.GetPosition(); // pour être appelé de partout sans être trop crado
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // rayon à la recherche de colliders
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, MouseWorld.instance.mousePlaneLayerMask);

        return hit.point;
    }
}
