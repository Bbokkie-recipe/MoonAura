using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType : int
{
    Hat =0,
    Bow=1,
    Potion=2,
    Default,
}

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public ItemType type;
    public bool stackable;

    public Sprite icon;
    public GameObject modelPrefab;
    public Item data = new Item();
    public List<string> boneNames = new List<string>();

    [TextArea(15, 20)]
    public string description;

    private void OnValidate()
    {
        boneNames.Clear();
        if(modelPrefab==null || modelPrefab.GetComponentInChildren<SkinnedMeshRenderer>() == null)
        {
            return;
        }
        SkinnedMeshRenderer renderer = modelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] bones = renderer.bones;

        foreach(Transform t in bones)
        {
            boneNames.Add(t.name);
        }
    }
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
