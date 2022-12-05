using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject nearTree;
    [SerializeField]float speed=10;
    [SerializeField]float dame=10;
    public float maxHp = 100;
    public float hp;
    float coundDown;

    Vector2 taget;

    Rigidbody2D rigidbody;
    Animator animator;

    private void Start()
    {
        hp = maxHp;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coundDown >= 0)
        {
            coundDown -= Time.deltaTime;
        }

        //if (nearTree == null)
        //{
        //    Debug.Log(1);
        //    taget = GetTaget()-gameObject.transform.position;
        //}
        CheckHit();
        taget = GetTaget() - gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = taget *speed*Time.deltaTime;
    }

    private Vector3 GetTaget()
    {
        float near=10000;
        foreach (GameObject tree in TreeController.instance.listTrees)
        {
            //Debug.Log((tree.transform.position - gameObject.transform.position).magnitude);
            if ((tree.transform.position - gameObject.transform.position).magnitude < near)
            {
                //Debug.Log(tree);
                nearTree = tree;
                near = (tree.transform.position - gameObject.transform.position).magnitude;
            }
        }

        return nearTree.transform.position;
    }

    private void CheckHit()
    {
        //Debug.Log(collision.name);
        //Debug.Log(collision.gameObject.layer);

        if (coundDown <= 0)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(gameObject.transform.position, 0.75f, taget, 0.5f);
            foreach (RaycastHit2D x in hit)
            {
                //Debug.Log(x.collider.name);
                if (x.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    SlimeController.instance.SetHp(-dame);
                    animator.Play("b");
                    coundDown = 1;
                }
                if (x.collider.gameObject.layer == LayerMask.NameToLayer("Tree"))
                {
                    x.collider.GetComponent<Tree>().SetHP(-dame);
                    animator.Play("c");
                    coundDown = 0.75f;
                }
            }
            
        }

    }

    public void SetHp(float value)
    {
        hp += value;
        if (hp <= 0)
        {
            int x= Random.Range(0, 2);
            if (x == 0)
            {
                int y= Random.Range(0, EquipController.instance.listDropItemLv1.Count);
                Instantiate(EquipController.instance.listDropItemLv1[y], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        if (value < 0)
        {
            gameObject.LeanColor(Color.red, 0.1f).setLoopPingPong(3).setOnComplete(() => { gameObject.LeanColor(Color.white, 0.1f); });

        }
    }
}
