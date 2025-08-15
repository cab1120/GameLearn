using TMPro; 
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI taskListText;

    private void OnEnable()
    {
        // 订阅所有UI相关的事件
        EventManager.OnShowDialogue += ShowDialogue;
        EventManager.OnHideDialogue += HideDialogue;
        EventManager.OnShowTaskList += ShowTaskList;
        EventManager.OnHideTaskList += HideTaskList;
    }

    private void OnDisable()
    {
        // 取消订阅
        EventManager.OnShowDialogue -= ShowDialogue;
        EventManager.OnHideDialogue -= HideDialogue;
        EventManager.OnShowTaskList -= ShowTaskList;
        EventManager.OnHideTaskList -= HideTaskList;
    }

    private void ShowDialogue(string text)
    {
        Cursor.lockState = CursorLockMode.Confined;
        dialogueText.transform.parent.gameObject.SetActive(true);
        dialogueText.text = text;
    }

    private void HideDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        dialogueText.transform.parent.gameObject.SetActive(false);
    }

    private void ShowTaskList(string text)
    {
        taskListText.transform.parent.gameObject.SetActive(true);
        taskListText.text = text;
    }

    private void HideTaskList()
    {
        taskListText.transform.parent.gameObject.SetActive(false);
    }
}
