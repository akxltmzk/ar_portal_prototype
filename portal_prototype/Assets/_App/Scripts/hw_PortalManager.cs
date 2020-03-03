using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

public class hw_PortalManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject sponza;
    public bool isMonsterDead = false;
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
            if (!isMonsterDead)
            {
                Debug.Log("몬스터를 이겨야 방에 들어갈 수 있습니다");
            }
            else
            {
                // active inside of room
                for (int i = 0; i < sponzaMaterials.Length; ++i)
                {
                    sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
                }
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
