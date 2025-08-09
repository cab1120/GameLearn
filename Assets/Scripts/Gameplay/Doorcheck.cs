using UnityEngine;

public class Doorcheck : MonoBehaviour
{
    public MoveController moveController;
    public CatchPen catchPen;
    public ThrowPen throwPen;
    public GameObject communicate;
    public GameObject waterCheck;
    public Doorhitcontroller hintUI;
    public GameObject Drink;
    public GameObject Text1, Text2, Text3;
    private bool canMove = true;
    private int num = 1, times = 1;

    private void Update()
    {
        if (!canMove) Showtext();
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
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

        if (other.gameObject.CompareTag("Pen"))
        {
            Debug.Log("showpen");
            hintUI.ShowHint("这个门没有锁孔，不能从这一侧打开");
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
                Text3.SetActive(true);
                break;
            case 4:
                communicate.SetActive(false);
                moveController.enabled = true;
                catchPen.enabled = true;
                throwPen.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Drink.SetActive(true);
                waterCheck.SetActive(true);
                enabled = false;
                break;
        }
    }
}