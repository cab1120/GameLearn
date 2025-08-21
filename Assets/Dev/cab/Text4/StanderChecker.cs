using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class StanderChecker : MonoBehaviour
{
    public GameObject notes;
    public GameObject notes2;
    public GameObject player;
    public PlayerInputHandler playerInputHandler;
    public PlayerMovement playerMovement;
    
    public TextMeshProUGUI textMesh;
    public Camera mainPlayerCamera; 

    public GameObject fixedObj1;
    public GameObject fixedObj2;
    public GameObject fixedObj3;
    public GameObject fixedObj4;

    public GameObject fakeWall;

    public GameObject objects;
    private bool isInPuzzleView; 
    private int fixnum = 1;
    
    private bool isPlayerInTrigger = false; // 用于跟踪玩家是否在触发器内

    private void Update()
    {
        // 逻辑1: 当玩家在触发器内，并且按下了 E 键
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            notes.SetActive(false);
            EnterPuzzleView();
        }

        // 逻辑2: 当处于解谜视角时
        if (isInPuzzleView)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitPuzzleView();
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                FixedObj();
                fixnum++;
            }
        }
        
        // 重置场景逻辑保持不变
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    // 进入解谜视角
    private void EnterPuzzleView()
    {
        isInPuzzleView = true;
        
        notes2.SetActive(true);
        notes.SetActive(false);
        // 调用 CameraSwitcher 的单例实例来启动从透视到正交的动画
        CameraSwitch.Instance.PerToOrt(); 

        Cursor.lockState = CursorLockMode.Confined;
        
        // 移动玩家的逻辑保持不变
        var controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = new Vector3(-2.199f, 1.785f, -4.481f);
        controller.enabled = true;
        player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        
        playerInputHandler.enabled = false;
        playerMovement.enabled = false;
        textMesh.text = "注意观察提示框颜色，并且按Q固定相应颜色物体";
    }

    // 退出解谜视角
    private void ExitPuzzleView()
    {
        fakeWall.SetActive(false);
        isInPuzzleView = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerInputHandler.enabled = true;
        playerMovement.enabled = true;
        notes2.SetActive(false);
        textMesh.text = "试着跳上平台吧";
        // --- 正确的调用方式 ---
        // 调用 CameraSwitcher 的单例实例来启动从正交到透视的动画
        CameraSwitch.Instance.OrtToPer();
        
        objects.SetActive(false);
        AddLayerToCameraCullingMask(mainPlayerCamera, 11);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&isPlayerInTrigger==false)
        {
            isPlayerInTrigger = true;
            notes.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            notes.SetActive(false);
        }
    }
    

    private void FixedObj()
    {
        switch (fixnum)
        {
            case 1: Fix(fixedObj1); notes2.GetComponent<Image>().color=Color.red;break;
            case 2: Fix(fixedObj2); notes2.GetComponent<Image>().color=Color.blue;break;
            case 3: Fix(fixedObj3); notes2.GetComponent<Image>().color=Color.white;break;
            case 4: Fix(fixedObj4); break;
        }
    }

    private void Fix(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void AddLayerToCameraCullingMask(Camera camera, int layer)
    {
        if (camera != null && layer != -1)
        {
            camera.cullingMask |= 1 << layer;
        }
    }
}