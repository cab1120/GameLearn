using UnityEngine;

public class Watercheck : MonoBehaviour
{
    public MoveController moveController;
    public CatchPen catchPen;
    public ThrowPen throwPen;
    public GameObject communicate;
    public GameObject Text1, Text2, Text3, Text4, Text5;

    public GameObject fakeFloor;
    public GameObject fakePlayer;
    public GameObject water;

    public GameObject camerainwater;
    public MoveController moveController2;
    public ThrowPen throwPen2;
    public Connetted connetted;
    private bool canMove = true;
    private int num = 1, times = 1;

    private void Update()
    {
        if (!canMove)
        {
            Showtext();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && times == 1)
        {
            times++;
            moveController.enabled = false;
            catchPen.enabled = false;
            throwPen.enabled = false;
            communicate.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            canMove = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
            if (other.gameObject.CompareTag("Player"))
            
            {
                Debug.Log("E");
                other.gameObject.SetActive(false);
                connetted.enabled = false;
                moveController2.enabled = true;
                throwPen2.enabled = true;
                camerainwater.SetActive(true);
                fakePlayer.layer = LayerMask.NameToLayer("Player");
                foreach (var child in fakePlayer.GetComponentsInChildren<Transform>(true))
                    child.gameObject.layer = LayerMask.NameToLayer("Player");
                fakePlayer.tag = "Player";
            }
    }

    public void Showtext()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            num++;
        switch (num)
        {
            case 1:
                Text1.SetActive(true);
                break;
            case 2:
                Text1.SetActive(false);
                Text2.SetActive(true);
                break;
            case 3:
                Text2.SetActive(false);
                fakeFloor.GetComponent<MeshRenderer>().enabled = false;
                water.SetActive(false);
                //fakeFloor.SetActive(false);
                GameObject.FindGameObjectWithTag("Pen").layer = LayerMask.NameToLayer("Dropped");
                Text3.SetActive(true);
                break;
            case 4:
                Text3.SetActive(false);
                Text4.SetActive(true);
                break;
            case 5:
                Text4.SetActive(false);
                Text5.SetActive(true);
                break;
            case 6:
//                Debug.Log("222");
                times++;
                communicate.SetActive(false);
                moveController.enabled = true;
                catchPen.enabled = true;
                throwPen.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
}