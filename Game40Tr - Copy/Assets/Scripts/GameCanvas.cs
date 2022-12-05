using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;

    public Transform groupItem;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Text MaxhpTxt;
    [SerializeField] Text hpTxt;
    [SerializeField] Text dameTxt;
    [SerializeField] Text speedTxt;
    [SerializeField] Text magicTxt;

    [SerializeField] GameObject endGamePanel;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
        Time.timeScale = 1;
    }
    public void AddBalo(GameObject item)
    {
        var i= Instantiate(itemPrefab, groupItem);
        i.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        i.GetComponent<Image>().color = item.GetComponent<SpriteRenderer>().color;
        i.GetComponent<IconItem>().itemData = item.GetComponent<Item>().itemData;
    }

    public void BaloBntClick()
    {
        if (groupItem.parent.gameObject.activeSelf)
        {
            groupItem.parent.gameObject.SetActive(false);
        }
        else
        {
            groupItem.parent.gameObject.SetActive(true);
        }
    }

    public void UpdateStats()
    {
        MaxhpTxt.text = "/" + SlimeController.instance.maxHp.ToString();
        hpTxt.text = SlimeController.instance.hp.ToString();
        dameTxt.text = SlimeController.instance.damage.ToString();
        speedTxt.text = SlimeController.instance.speed.ToString();
        magicTxt.text = SlimeController.instance.magic.ToString();
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
    }

    public void BackClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
