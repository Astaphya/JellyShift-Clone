using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
     #region TouchControl
     public Text directionText;
     private Touch theTouch;
     private Vector2 touchStartPosition,touchEndPosition;
     private string direction;

     #endregion
      public float m_Speed = 2.5f;
   // [Range(0.25f,2.25f)]
    public GameObject cubeProjectile;
    public float xScale;

   // [Range(0.25f,2.25f)]
    public float yScale;
    private const float maxScaleBoundary = 1.8f; // Küp max scale
    private const float minScalBoundary = 0.2f;  // Küp min scale

    public bool yukariMi = false;

    public float scaleFactor; // Küp scale artış oranı
    
    public float startTime; // Swipe başlangıç 
    public float endTime;   // Swipe bitiş
    public float swipeDuration;
    public float swipeVelocity;
    private float maxSwipeVelocity = 2000f; // Max y swipe distance
    private float minSwipeVelocity = 20f; // Min y swipe distance
  
   
    public float normalizedVelocity;
    public float scaleSpeed;
    
   
    void Start()
    {
      yScale = transform.localScale.y;
      xScale = transform.localScale.x;
        
    }
    private void Update()
    {
     
      if(GameManager.Instance.isAktif)
      {
         // Kübün  z ekseninde hareket etmesi.
        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
      
    
      ProgressCalculation(); // ProgressBar ilerlemesini kontrol eden fonksiyon.

      yScale = transform.localScale.y;
      xScale = transform.localScale.x;

      cubeProjectile.transform.localScale  = gameObject.transform.GetChild(0).transform.localScale; // Cube'un scale değerleri  projectile scale değerlerini eşit tutuyoruz .

      #region TouchControlUpdate

     
      if(Input.touchCount>0)
      {
        theTouch = Input.GetTouch(0);
      
      if(theTouch.phase == TouchPhase.Began)
      {
        touchStartPosition = theTouch.position;
        startTime = Time.time; // Swipe start Time
      }
      else if(theTouch.phase == TouchPhase.Moved  || theTouch.phase == TouchPhase.Ended)
      {
        touchEndPosition = theTouch.position;
        endTime = Time.time; // Swipe end time

       // float x = touchEndPosition.x - touchStartPosition.x;  // swipe x distance
        float y = touchEndPosition.y - touchStartPosition.y;; // swipe y distance
        swipeVelocity = Mathf.Abs(y);
        NormalizeVelocity();

        Debug.Log("swipeDircetion y: "+ y.ToString()); 
        Debug.Log("yDist: " + Mathf.Abs(y).ToString());

        
        
        

        
        
       // scaleFactor = yDist/ swipeDuration;

        //Debug.Log("Scale Factor: "+scaleFactor.ToString());



       // Debug.Log("y :" + y.ToString());
        
        /*
        if(Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
        {
          direction = "Tapped";
        }
*/
        if(Mathf.Abs(y) == 0) direction = "Tapped";

        /*
        else if(Mathf.Abs(x)> Mathf.Abs(y))
        {
          direction = x > 0 ? "Right" : "Left";
        }
        */
        else{
          direction = y > 0 ? "Up" : "Down";
          
             if(y>0)
             {
                  
                  yukariMi = true;
                  ScaleControl(yukariMi); 
               
              
             }
             else{
                  yukariMi = false;
                  ScaleControl(yukariMi);
               
              
             }
                
           
          }
        }
         //directionText.text = direction;
      }
    }
   
      #endregion
    
    
    }

    public void NormalizeVelocity()
    {
      /*
        yScale = Mathf.PingPong(Time.time ,maxScaleBoundary - minScalBoundary) + yScale;
        transform.localScale = new Vector3(xScale,yScale,transform.localScale.z);
      */

      // deger = deger(i) - minDeger / maxDeger - minDeger max= 10k min = 50
      //Normalization

      // Swipe değerlerini normalize ediyoruz.
      normalizedVelocity = (swipeVelocity - minSwipeVelocity) / (maxSwipeVelocity - minSwipeVelocity) * 2   ;
      
      
    }

    public void ProgressCalculation()
    {
      // Scaling object'in z konumu , end pointe göre oranlanacak.
      GameManager.Instance.progressSlider.value = this.transform.position.z;
    }
    
    public void ScaleControl(bool isUp)
    {

     
      if(isUp)
      {
             
        if(yScale <= maxScaleBoundary)
        {
          yScale +=  scaleFactor  * normalizedVelocity ;
          xScale -=  scaleFactor  * normalizedVelocity ;
          //scaleRate +=0.1f;
          // yScale =  Mathf.Lerp(transform.localScale.y,yScale,Time.deltaTime);
          // xScale = Mathf.Lerp(transform.localScale.x,xScale,Time.deltaTime);
          
          
          if(yScale >= 1.7f)
          { 
            yScale = 1.8f;
            xScale = 0.2f;
          } 
          Vector3 newScale = new Vector3(xScale,yScale,transform.localScale.z);

          transform.localScale = Vector3.Lerp (transform.localScale, newScale, scaleSpeed * Time.deltaTime); // Scale değeri üzerinde Lerp fonksiyonu ile düzgün,yumuşak bir geçiş sağlıyoruz.
          
         // transform.localScale = new Vector3(xScale,yScale,transform.localScale.z);
        } 
           
      }

      else
      {
          
          if(yScale >= minScalBoundary)
          {
              yScale -=  scaleFactor * normalizedVelocity ;
              xScale +=  scaleFactor * normalizedVelocity ;
             
           // xScale +=scaleRate;
          //yScale =  Mathf.Lerp(transform.localScale.y,yScale,Time.deltaTime);
          //xScale = Mathf.Lerp(transform.localScale.x,xScale,Time.deltaTime);
          
          if(xScale >= 1.7f)
          {
            xScale = 1.8f;
            yScale = 0.2f;

          } 
           Vector3 newScale = new Vector3(xScale,yScale,transform.localScale.z);

          transform.localScale = Vector3.Lerp (transform.localScale, newScale, scaleSpeed * Time.deltaTime);
          //transform.localScale = new Vector3(xScale,yScale,transform.localScale.z);
            
          }
          

      }
      

    }

   
   
}
    
