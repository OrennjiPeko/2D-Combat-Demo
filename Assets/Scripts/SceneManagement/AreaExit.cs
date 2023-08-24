using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] public bool Town=false;
    [SerializeField] public bool BossRoom = false;
    private float waitToLoadTime = 1f;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            
            SceneManagment.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            FindObjectOfType<AudioManager>().StopMusic();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    
    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        if (Town == true)
        {
            FindObjectOfType<AudioManager>().Play("Town");
        }
        if(BossRoom == true)
        {
                FindObjectOfType<AudioManager>().Play("BossTheme");
        }
        if(Town == false && BossRoom == false)
        {
            FindObjectOfType<AudioManager>().PlayMusic();
        }
        
        
        SceneManager.LoadScene(sceneToLoad);
    }
}
