using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hw_Monster : MonoBehaviour
{

    Animator monsterAnim;
    
    private void Start()
    {
        monsterAnim = GetComponent<Animator>();   
    }


    public void SetActiveAnim() {
        monsterAnim.SetTrigger("damage");
    }
    
}
