using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class IconItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData;
    Transform parentAfterDrag;


    PointerEventData _PointerEventData;
    EventSystem _EventSystem;
    GraphicRaycaster _Raycaster;

    private void Start()
    {

        //Fetch the Raycaster from the GameObject (the Canvas)
        _Raycaster = GameCanvas.instance.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        _EventSystem = GetComponent<EventSystem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(GameCanvas.instance.transform);
        transform.SetAsLastSibling();
        EquipController.instance.UpdateEquipStats();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _PointerEventData = new PointerEventData(_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        _PointerEventData.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        _Raycaster.Raycast(_PointerEventData, results);
        
        foreach (RaycastResult rs in results)
        {
            Debug.Log(rs.gameObject.name);
            if (rs.gameObject.layer == LayerMask.NameToLayer("ItemIcon") && rs.gameObject!=gameObject)
            {
                foreach (GameObject g in EquipController.instance.listDropItemLv1)
                {
                    if (g.GetComponent<Item>().itemData == itemData)
                    {
                        foreach (GameObject v in EquipController.instance.listDropItemLv1)
                        {
                            if (v.GetComponent<Item>().itemData == rs.gameObject.GetComponent<IconItem>().itemData)
                            {
                                int y = Random.Range(0, EquipController.instance.listDropItemLv2.Count);
                                Instantiate(EquipController.instance.listDropItemLv2[y], SlimeController.instance.gameObject.transform.position,Quaternion.identity);
                                Destroy(rs.gameObject);
                                Destroy(gameObject);
                                break;
                            }
                        }
                        break;
                    }
                }

                EquipController.instance.UpdateEquipStats();
            }
            if(rs.gameObject.layer == LayerMask.NameToLayer("Equip"))
            {
                try
                {
                    rs.gameObject.GetComponentInChildren<IconItem>();
                    Debug.Log(rs.gameObject.GetComponentInChildren<IconItem>().gameObject.name);
                }
                catch
                {
                    transform.SetParent(rs.gameObject.transform);
                    EquipController.instance.UpdateEquipStats();
                    return;
                }
            }else if (rs.gameObject.layer == LayerMask.NameToLayer("Inventory"))
            {
                if (EquipController.instance.listItems.gameObject.GetComponentsInChildren<RectTransform>().Length < EquipController.instance.size)
                {
                    transform.SetParent(GameCanvas.instance.groupItem);
                    EquipController.instance.UpdateEquipStats();
                    return;
                }
            }
        }
        transform.SetParent(parentAfterDrag);
        EquipController.instance.UpdateEquipStats();
    }
   

}
