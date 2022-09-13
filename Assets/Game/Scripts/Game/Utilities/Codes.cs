using Game.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallBack();

[Serializable]
public class CameraSystemProperty
{
    public Vector3 Offset;
    public Vector3 Rotation;
    public float FieldOfView;

    public Vector3 Damping;
}

public class ScriptOrder : Attribute
{
    public int order;

    public ScriptOrder(int order)
    {
        this.order = order;
    }
}

[Serializable]
public class CreatablePoolItemProperty
{
    [Space(5)] public GameObject Prefab;

    [Space(5)] public int SpawnCount;
    [Space(5)] public Transform Parent;

    public string GetPoolItemName()
    {
        string poolItemName = Prefab.GetComponent<IPoolItem>()?.ToString();

        if (string.IsNullOrEmpty(poolItemName))
        {
            Debug.LogError("Your Prefab does not have a IPoolItem!");
            return "";
        }

        poolItemName = poolItemName.Substring(poolItemName.IndexOf("(") + 1);
        poolItemName = poolItemName.Substring(0, poolItemName.IndexOf(")"));

        return poolItemName;
    }
}

[Serializable]
public class TowerData
{
    public List<TowerCharacter> TowerCharacters = new()
    {
        new TowerCharacter(),
    };
}


[Serializable]
public class TowerCharacter
{
    public int Level = 1;
    public int SlotIndex = 0;

}

[Serializable]
public class TowerCharacterCard
{
    public int CharacterLevel;
    public Sprite CharacterCardSprite;

}

[Serializable]
public class TowerCharacterModel
{
    public int CharacterLevel;
    public TowerCharacterModelActor TowerCharacterModelActor;
}

public static class Codes
{
    public static TowerCharacter CreateNewTowerCharacter(int level, int slotIndex = 0)
    {
        return new TowerCharacter()
        {
            Level = level,
            SlotIndex = slotIndex
        };
    }


}