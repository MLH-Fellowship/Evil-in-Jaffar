using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class bandit : MonoBehaviour

// Code taken from https://www.youtube.com/watch?v=jvtFUfJ6CP8

{
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
	// Change face direction of enemy when player goes in another direction
        if(aiPath.desiredVelocity.x >= 0.01f) {
		transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
	}
	else if (aiPath.desiredVelocity.x <= -0.01f) {
		transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
	}
    }
}
