using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New item", menuName = "Item/Create new item")]
public class ItemData : ScriptableObject
{
    public string name;
    public int hp = 0;
    public int damage = 0;
    public int magic = 0;
    public int speed = 0;

}
