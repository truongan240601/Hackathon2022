using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public static SlimeController instance;

    private Rigidbody2D slimeRB;
    private Animator slimeAnim;

    private Vector2 huong;

    public float maxHp=500;
    [HideInInspector] public float hp;
    public float damage=50;
    public float magic=50;
    public float speed=50;
    float coundDown;

    [SerializeField]
    private GameObject farAtk;
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();
        hp = maxHp;
        GameCanvas.instance.UpdateStats();


        slimeRB = GetComponent<Rigidbody2D>();
        slimeAnim = GetComponent<Animator>();
        slimeAnim.SetFloat("lastMoveX", 0);
        slimeAnim.SetFloat("lastMoveY", -1);
    }
    public void UpdateStats()
    {
        maxHp = GameManager.instance.hp;
        damage = GameManager.instance.damage;
        magic = GameManager.instance.magic;
        speed = GameManager.instance.speed;

    }

    private void Update()
    {
        if (coundDown >= 0)
        {
            coundDown -= Time.deltaTime;
        }
        if (Input.GetKey("space") && coundDown <= 0)
        {
            if (slimeAnim.GetFloat("lastMoveX") >= 1)
            {
                slimeAnim.Play("DameAtkRight");
                CheckHitEnemy(Vector2.right);
            }
            else if (slimeAnim.GetFloat("lastMoveX") <= -1)
            {
                slimeAnim.Play("DameAtkLeft");
                CheckHitEnemy(Vector2.left);
            }
            else if (slimeAnim.GetFloat("lastMoveY") >= 1)
            {
                slimeAnim.Play("DameAtkTop");
                CheckHitEnemy(Vector2.up);
            }
            else
            {
                slimeAnim.Play("DameAtkBottom");
                CheckHitEnemy(Vector2.down);
            }
            coundDown = 0.5f;
        }
    }

    private void CheckHitEnemy(Vector2 directory)
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(gameObject.transform.position, 1, directory, 0.5f);
        hit.Distinct();
        if (hit.Length > 0)
        {
            foreach (RaycastHit2D x in hit)
            {
                if (x.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    x.collider.gameObject.GetComponent<Enemy>().SetHp(-damage);
                    //Debug.Log(x.collider.name);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        huong = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        slimeRB.velocity = huong * speed * 5 * Time.deltaTime;

        slimeAnim.SetFloat("moveX", huong.x);
        slimeAnim.SetFloat("moveY", huong.y);

        if (huong.x >= 0.5 || huong.y >= 0.5 || huong.x <= -0.5 || huong.y <= -0.5)
        {
            slimeAnim.SetFloat("lastMoveX", huong.x);
            slimeAnim.SetFloat("lastMoveY", huong.y);
        }

        
    }

    public void SetHp(float value)
    {
        GameCanvas.instance.UpdateStats();
        hp += value;
        if (hp <= 0)
        {
            GameCanvas.instance.EndGame();
        }
        if (value < 0)
        {
            gameObject.LeanColor(Color.red, 0.1f).setLoopPingPong(3).setOnComplete(() => { gameObject.LeanColor(Color.white, 0.1f); });

        }
    }



}
