using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("Turn_Left", true);
            animator.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("Turn_Left", false);
            animator.SetBool("Turn_Right", false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("Turn_Right", true);
            animator.SetBool("Turn_Left", false);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("Turn_Right", false);
            animator.SetBool("Turn_Left", false);
        }
    }
}
