using UnityEngine;

public class ChangePen : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pen"))
            other.gameObject.transform.position = new Vector3(-0.017f, -4.166f, 3.465f);
    }
}