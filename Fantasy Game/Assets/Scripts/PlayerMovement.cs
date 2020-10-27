using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerController controller;
    public Animator animator;

    public float runSpeed = 0f;

    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed",Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump")){
            jump = true;
            animator.SetBool("jump",true);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding () {
        animator.SetBool("jump",false);
    }
}
