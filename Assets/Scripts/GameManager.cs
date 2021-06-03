using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance ; }}
    public Button restartButton;
    public Button tapToStartButton;
    public  bool isAktif = false ;
    public Slider progressSlider;
    public Text finishedText;
    public GameObject Timeline; 
    
    private float startDelay = 0.15f;
   
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
        
    }
    void Start()
    {
        isAktif = false;

    }

    
    //Wrapping , UI üzerinden coroutine çalıştılamayacağı için bir sarmal kullandık.
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    public IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        isAktif = true;
        tapToStartButton.gameObject.SetActive(false);
        progressSlider.gameObject.SetActive(true);

    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        progressSlider.gameObject.SetActive(false);
        //isAktif false olunca UI arayüzü çağır .
    }
    public void RestartLevel()
    {
        restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Aktif sahneyi tekrar yükle.
    }

    public IEnumerator CompletedTheChaper()
    {
        //finishedText.gameObject.SetActive(true);
        progressSlider.gameObject.SetActive(false);
        isAktif = false ;
        Timeline.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        RestartLevel();
        

    }
}
