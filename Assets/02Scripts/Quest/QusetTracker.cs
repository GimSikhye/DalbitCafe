using UnityEngine;
// �ǽð� ���� üũ (�Ǹ�, ��ġ �� �̺�Ʈ ����)
// Ŀ�� �Ǹ�&���� ��ġ �������� QuesetTracker.Onxxx�� ȣ�����ָ� ��.
public class QusetTracker : MonoBehaviour
{
    public void OnCoffeeSold(string coffeId)
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questManager.CheckQuestProgress(coffeId, QusetType.SellCoffee);
    }

    public void OnFuniturePlaced(string furnitureId)
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questManager.CheckQuestProgress(furnitureId, QusetType.PlaceFurniture);
    }
    
    public void OnFurnitureUpgraded(string furnitureId)
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questManager.CheckQuestProgress(furnitureId, QusetType.UpgradeFurniture);
    }

}
