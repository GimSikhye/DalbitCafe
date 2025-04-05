using UnityEngine;
using UnityEngine.Rendering.LookDev;
// ����Ʈ ������ ���� (����, ���� ��)
public enum QusetType {  SellCoffee, PlaceFurniture, UpgradeFurniture}

[CreateAssetMenu(fileName = "QuestData", menuName = "SO/QuestData")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string description;
    public QusetType questType;

    public string targetItemId; // Ŀ�� ���� ID �Ǵ� ���� ID
    public int requiredAmount;

    public int rewardGold;
    public int rewardExp;

    public bool isCompleted;

}
