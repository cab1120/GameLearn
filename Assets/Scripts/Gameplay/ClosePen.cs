using UnityEngine;

public class ClosePen : MonoBehaviour
{
    public GameObject floor;
    public CatchPen catchPen;

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Pen")) catchPen.enabled = false;
        //other.gameObject.transform.position = new Vector3(-0.017f, -4.166f, 3.465f);
    }

    public void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Pen")) floor.SetActive(true);
    }
}