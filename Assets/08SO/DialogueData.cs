using UnityEngine;

[CreateAssetMenu(menuName = "SO/DialogueData")]

public class DialogueData : ScriptableObject
{
    //������ ��縦 �ϳ��ϳ� ����.
    public Sprite CharacterSprite;
    public string CharacterName;
    public int EventNum; //�̺�Ʈ ��ȣ
    public Sprite ChangeBG;
    public string[] Lines;
}
