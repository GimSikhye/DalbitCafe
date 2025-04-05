using UnityEngine;

// ���� ���� ó�� ���
public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GiveReward(QuestData quest)
    {
        PlayerStats.Instance.gold += quest.rewardGold;
        PlayerStats.Instnace.exp += quest.rewardExp;
        Debug.Log($"����Ʈ �Ϸ�! ���� ���� {quest.rewardGold}G / {quest.rewardExp}EXP");
    }
}
