using UnityEngine;

public class Connetted : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    public Animator animator;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            animator.SetBool("CanWalk", true);
        else
            animator.SetBool("CanWalk", false);
    }

    private void LateUpdate()
    {
        player2.transform.localPosition = player.transform.localPosition;
        player2.transform.localRotation = player.transform.localRotation;
    }
}