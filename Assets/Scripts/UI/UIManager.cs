using System.Collections;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Text taskListText;
    public Text TopicTest;

    private void OnEnable()
    {
        // 订阅所有UI相关的事件
        EventManager.OnShowDialogue += ShowDialogue;
        EventManager.OnHideDialogue += HideDialogue;
        EventManager.OnShowTaskList += ShowTaskList;
        EventManager.OnHideTaskList += HideTaskList;
        EventManager.OnShowTopicList += ShowTopic;
    }

    private void OnDisable()
    {
        // 取消订阅
        EventManager.OnShowDialogue -= ShowDialogue;
        EventManager.OnHideDialogue -= HideDialogue;
        EventManager.OnShowTaskList -= ShowTaskList;
        EventManager.OnHideTaskList -= HideTaskList;
        EventManager.OnShowTopicList -= ShowTopic;
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

    private void ShowTopic(string text)
    {
        TopicTest.transform.parent.gameObject.SetActive(true);
        TopicTest.text = text;
        StartCoroutine(HideUIAfterTime());
    }
    private IEnumerator HideUIAfterTime()
    {
        yield return new WaitForSeconds(2);
        TopicTest.transform.parent.gameObject.SetActive(false);
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
