using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hw_PlayerManager : MonoBehaviour
{

    public void Attack()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Monster") {
                hit.collider.GetComponent<hw_Monster>().SetActiveAnim();
            }
        }
        
    }
}
