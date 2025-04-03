using UnityEngine;

// Enum ����
public enum EventNum
{
    Prologue = 0,
    FirstChapter,
    SecondChapter,
    ThirdChapter,
    FourthChapter,
    Ending
}

[CreateAssetMenu(menuName = "SO/DialogueData")]

public class DialogueData : ScriptableObject
{
    //������ ��縦 �ϳ��ϳ� ����.
    public Sprite CharacterSprite;
    public string CharacterName;
    public EventNum eventNum; //�̺�Ʈ ��ȣ
    public Sprite ChangeBG;
    public string[] Lines; // ����
}
