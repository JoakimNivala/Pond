using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName ="Quests/Quest")]

public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public List<QuestObjective> objectives;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(questID))
            {
            questID = questName + Guid.NewGuid().ToString();
        }
    }
    
}
[System.Serializable]
public class QuestObjective
{
    public string objectiveID;
    public string description;
    public ObjectiveType type;
    public int RequiredAmount;
    public int CurrentAmount;

    public bool isCompleted => CurrentAmount >= RequiredAmount;
}
public enum ObjectiveType { CollectItem, DefeatEnemy, ReachLocation, TalkNPC, Custom }

[System.Serializable]
public class QuestProgress
{
    public Quest quest;
    public List<QuestObjective> objectives;

    public QuestProgress(Quest quest)
    {
        this.quest = quest;
        this.objectives = new List<QuestObjective>();

        foreach (var obj in quest.objectives)
        {
            objectives.Add(new QuestObjective
            {
                objectiveID = obj.objectiveID,
                description = obj.description,
                type = obj.type,
                RequiredAmount = obj.RequiredAmount,
                CurrentAmount = 0
            }
            );

        }
    }
    public bool IsCompleted => objectives.TrueForAll(o => o.isCompleted);
    public string QuestID => quest.questID;
}