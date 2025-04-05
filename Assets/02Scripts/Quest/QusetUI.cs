using TMPro;
using UnityEngine;

public class QusetUI : MonoBehaviour
{
    public static QusetUI Instance;

    public GameObject questPanel;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescText;
    public GameObject completePopup;


    private void Awake()
    {
        Instance = this;
    }

    public void ShowQuest(QuestData quest)
    {
        questTitleText.text = quest.questTitle;
        questDescText.text = quest.description;
        questPanel.SetActive(true);
    }

    public void ShowQuestComplete(QuestData quest)
    {
        completePopup.SetActive(true);
        // �ִϸ��̼�, ����Ʈ �� �߰� ����
    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
