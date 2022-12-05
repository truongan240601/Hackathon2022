using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (EquipController.instance.listItems.gameObject.GetComponentsInChildren<RectTransform>().Length < EquipController.instance.size)
            {
                GameCanvas.instance.AddBalo(gameObject);
                Destroy(gameObject);
            }
        }
    }

}
