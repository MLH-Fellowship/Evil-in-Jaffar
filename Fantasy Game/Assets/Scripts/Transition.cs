using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public float waitTime = 3f;
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BeginTransition(waitTime));
    }

     //Enable transitions from transition to fight level
     IEnumerator BeginTransition(float time) {
	    
	    //Wait for a few seconds
	    yield return new WaitForSeconds(time);

	    //Start transition
	    LoadNextLevel();
    }

    public void LoadNextLevel() {
	    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {

	    //Play animation
	    transition.SetTrigger("Start");

	    //Wait
	    yield return new WaitForSeconds(transitionTime);

	    //Load Scene
	    SceneManager.LoadScene(levelIndex);
    }



}
