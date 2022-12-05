using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    public static EquipController instance;
    public GridLayoutGroup listItems;
    public List<GameObject> listItemInEquip = new List<GameObject>();
    public List<GameObject> listDropItemLv1;
    public List<GameObject> listDropItemLv2;

    public int size=20;

    private float hp = 0;
    private float damage = 0;
    private float magic = 0;
    private float speed = 0;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

    public void UpdateEquipStats()
    {
        hp = 0;
        damage = 0;
        magic = 0;
        speed = 0;
        //SlimeController.instance.UpdateStats();
        foreach (GameObject g in listItemInEquip)
        {
            try
            {
                var i= g.GetComponentInChildren<IconItem>().itemData;
                hp += i.hp;
                damage += i.damage;
                magic += i.magic;
                speed += i.speed;
            }
            catch 
            {

            }
        }

        SlimeController.instance.UpdateStats();
        SlimeController.instance.maxHp += hp;
        SlimeController.instance.damage += damage;
        SlimeController.instance.magic += magic;
        SlimeController.instance.speed += speed;
        GameCanvas.instance.UpdateStats();
    }
}
