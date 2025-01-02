using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSlime : MonoBehaviour
{
    public GameObject dialogBox; // 对话框对象
    public Text dialogBoxText;   // 对话框文本组件
    public List<string> dialogList; // 对话列表
    private bool isPlayerInSign; // 玩家是否在NPC范围内
    private int currentDialogIndex = 0; // 当前对话索引

    public GameObject tipBox;
    public Text tipBoxText;   // 对话框文本组件

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.Find("DialogBox");
        tipBox = GameObject.Find("TipBox");
        dialogBoxText = dialogBox.GetComponentInChildren<Text>();
        tipBoxText = tipBox.GetComponentInChildren<Text>();
        dialogBox.SetActive(false); // 初始化时隐藏对话框
        tipBox.SetActive(false); // 初始化时隐藏对话框
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInSign)
        {
            ShowNextDialog(); // 按下F键时显示下一句对话
        }
    }

    // 显示下一句对话
    private void ShowNextDialog()
    {
        if (currentDialogIndex < dialogList.Count)
        {
            dialogBox.SetActive(true); // 显示对话框
            dialogBoxText.text = dialogList[currentDialogIndex]; // 设置对话文本
            currentDialogIndex++; // 移动到下一句对话
        }
        else
        {
            dialogBox.SetActive(false); // 对话结束后隐藏对话框
            currentDialogIndex = 0; // 重置对话索引
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("进入NPC范围");
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(true);
            tipBoxText.text = ("按下F键");
            isPlayerInSign = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("离开NPC范围");
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(false);

            isPlayerInSign = false;
            dialogBox.SetActive(false); // 玩家离开时隐藏对话框
            currentDialogIndex = 0; // 重置对话索引
        }
    }
}