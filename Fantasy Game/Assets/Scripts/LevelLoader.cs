using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
     public Animator transition;
     public Canvas healthbars;
     public GameObject Enemy;
     public GameObject Player;
     Animator enemy_anim;
     Animator player_anim;

     public float transitionTime = 1f;
     public float waitTime = 3f;
     public int win_scene = 8;
     public int lose_scene = 9;
     public int final_level = 7;
     public int first_level = 3;


     //Get access to enemy animator to trigger transition and level loader events
    void Awake() {
	enemy_anim = Enemy.GetComponent<Animator>();
	player_anim = Player.GetComponent<Animator>();
     }


    // Update is called once per frame
    void Update(){

		//Play for ending scenes
		if(player_anim.GetBool("Death")) {
			StartCoroutine(BeginTransitionConclusion(waitTime, false));
		}
		else if(enemy_anim.GetBool("dead")) {
			if (SceneManager.GetActiveScene().buildIndex == final_level) {
		    	StartCoroutine(BeginTransitionConclusion(waitTime, true));
			}
			else{
				//Play for rest of the levels
		    	StartCoroutine(BeginTransition(waitTime));
			}
		}
	}

    //Enable transitions for each fighting level
    IEnumerator BeginTransition(float time) {
	    
	    //Wait for a few seconds
	    yield return new WaitForSeconds(time);

	    //Disable the healthbar UI
	    healthbars.GetComponent<Canvas>().enabled = false;

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

    //Start the conclusion transition
    IEnumerator BeginTransitionConclusion(float time, bool win) {
	
	 //Wait for a few seconds
	 yield return new WaitForSeconds(time);

	 //Disable the healthbar UI
	healthbars.GetComponent<Canvas>().enabled = false; 

	//Start transition
	if(win) {
	    LoadLastLevel(true);
	}
	else{
	    LoadLastLevel(false);
	}
    }

    public void LoadLastLevel(bool win) {
	    if(win) {
	   	 StartCoroutine(LoadLevel(win_scene));
	    }
	    else {
		 StartCoroutine(LoadLevel(lose_scene));
	    }
    }

    public void LoadMainMenu() {
	    SceneManager.LoadScene(0);
    }

    public void LoadFirstLevel() {
	    SceneManager.LoadScene(2);
    }


}
