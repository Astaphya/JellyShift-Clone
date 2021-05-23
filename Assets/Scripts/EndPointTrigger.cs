using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.CompareTag("Cube"))
       {
           Debug.Log("Tebrikler.Bölümü tamamladınız.");
           StartCoroutine(GameManager.Instance.CompletedTheChaper());
       }
       
       
   }
}
