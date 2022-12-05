using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float timeSpam;
    float countDown=0;
    int count = 0;
    [SerializeField] GameObject enemy;

    [SerializeField]Vector2 minRightSpam;
    [SerializeField]Vector2 maxRightSpam;
    [SerializeField]Vector2 minLeftSpam;
    [SerializeField]Vector2 maxLeftSpam;
    [SerializeField]Vector2 minTopSpam;
    [SerializeField]Vector2 maxTopSpam;
    [SerializeField]Vector2 minBottomSpam;
    [SerializeField]Vector2 maxBottomSpam;

    


    // Update is called once per frame
    void Update()
    {

        if (countDown <= 0)
        {
            if (count % 5 == 0)
            {
                Spam();
            }
            Spam();
            timeSpam *= 0.99f;
            countDown = timeSpam;
            count++;
        }
        else
        {
            countDown -= Time.deltaTime;
        }
    }

    private void Spam()
    {
        Vector2 position;

        position.x= Random.Range(minRightSpam.x, maxRightSpam.x);
        position.y=  Random.Range(minRightSpam.y, maxRightSpam.y);

        GameObject e = Instantiate(enemy, position, Quaternion.identity, transform);
        e.GetComponent<Enemy>();
    }
}
