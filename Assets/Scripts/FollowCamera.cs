using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Cube;
   // [SerializeField] private Vector3 offSet;
    private float lerpTime = 0.1f;

    public Vector3 dist;
    
   
    void LateUpdate()
    {
        transform.position = Cube.position + dist;
        //transform.LookAt(Cube.position);
       // transform.position = Vector3.SmoothDamp(transform.position,Cube.position+offSet,ref velocity,lerpTime);
    }
    
    /*
    
    private void FixedUpdate()
    {
        while(GameManager.Instance.isAktif)
        {
            ChangeTheCam(camTarget);
        }
        
        
    }

    public void ChangeTheCam(Transform cameraTarget)
    {
        Vector3 dPos = cameraTarget.position + dist;
        Vector3 sPos = Vector3.Lerp(transform.position,dPos,speed*Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);

    }
    */


}
