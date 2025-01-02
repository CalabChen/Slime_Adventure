using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSlime : MonoBehaviour
{
    public GameObject dialogBox; // �Ի������
    public Text dialogBoxText;   // �Ի����ı����
    public List<string> dialogList; // �Ի��б�
    private bool isPlayerInSign; // ����Ƿ���NPC��Χ��
    private int currentDialogIndex = 0; // ��ǰ�Ի�����

    public GameObject tipBox;
    public Text tipBoxText;   // �Ի����ı����

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.Find("DialogBox");
        tipBox = GameObject.Find("TipBox");
        dialogBoxText = dialogBox.GetComponentInChildren<Text>();
        tipBoxText = tipBox.GetComponentInChildren<Text>();
        dialogBox.SetActive(false); // ��ʼ��ʱ���ضԻ���
        tipBox.SetActive(false); // ��ʼ��ʱ���ضԻ���
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInSign)
        {
            ShowNextDialog(); // ����F��ʱ��ʾ��һ��Ի�
        }
    }

    // ��ʾ��һ��Ի�
    private void ShowNextDialog()
    {
        if (currentDialogIndex < dialogList.Count)
        {
            dialogBox.SetActive(true); // ��ʾ�Ի���
            dialogBoxText.text = dialogList[currentDialogIndex]; // ���öԻ��ı�
            currentDialogIndex++; // �ƶ�����һ��Ի�
        }
        else
        {
            dialogBox.SetActive(false); // �Ի����������ضԻ���
            currentDialogIndex = 0; // ���öԻ�����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����NPC��Χ");
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(true);
            tipBoxText.text = ("����F��");
            isPlayerInSign = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("�뿪NPC��Χ");
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(false);

            isPlayerInSign = false;
            dialogBox.SetActive(false); // ����뿪ʱ���ضԻ���
            currentDialogIndex = 0; // ���öԻ�����
        }
    }
}