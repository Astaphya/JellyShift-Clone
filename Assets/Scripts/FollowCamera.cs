using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Cube;
    [SerializeField] private Vector3 offSet;
    [SerializeField]private float speed;
    
   
    private void LateUpdate()
    {
         transform.position =  Vector3.Lerp(transform.position,Cube.position+offSet , speed * Time.deltaTime);
    }
    
 
    
 

  


}
