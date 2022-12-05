using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public float maxHp=100;
    private float hp;

    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    public void SetHP(float value)
    {
        hp += value;
        if (hp <= 0)
        {
            TreeController.instance.listTrees.Remove(gameObject);
            if (TreeController.instance.listTrees.Count <= 0)
                GameCanvas.instance.EndGame();
            Destroy(gameObject);
        }
        gameObject.LeanAlpha(hp / maxHp, 0.1f);
    }

    private void Update()
    {
        if (countDown <= 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 5);
            foreach (Collider2D c in hit)
            {
                if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    if (SlimeController.instance.hp < SlimeController.instance.maxHp)
                    SlimeController.instance.SetHp(1);
                }
            }
            countDown = 1;
        }
        else
        {
            countDown -= Time.deltaTime;
        }
    }

}
