using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAttribute
{
    Agility,
    Stamina,
    Mana,
    Strength
}

[Serializable]
public class ItemBuff
{
    public CharacterAttribute stat;
    public int value;

    [SerializeField]
    private int min;

    [SerializeField]
    private int max;

    //프로퍼티 Min, Max
    public int Min => min;
    public int Max => max;

    //생성자
    public ItemBuff(int min, int max)
    {
        this.min = min;
        this.max = max;

        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

    public void AddValue(ref int v)
    {
        v += value;
    }
}
