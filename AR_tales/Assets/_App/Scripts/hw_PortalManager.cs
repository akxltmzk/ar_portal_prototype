using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class hw_PortalManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject sponza;
    private Material[] sponzaMaterials;

    void Start()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
    }

    private void OnTriggerStay(Collider collider)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position);
        if (camPositionInPortalSpace.y < 1.0f)
        {
            // disable stencil
            for (int i = 0; i < sponzaMaterials.Length; ++i)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }

        }
        else {
            // enable stencil
            for (int i = 0; i < sponzaMaterials.Length; ++i)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);

            }
        }
    }
}
