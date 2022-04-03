using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    #region public variables
    public float speedMove = 2f, speedRot;
    #endregion

    #region private variables
    private Transform player;
    [SerializeField] private Transform[] pointToWalk;
    private Animator anim;
    private GameObject warmingSign;
    private int currentPoint = 1;
    private bool stop;
    #endregion

    void Start()
    {
        player = ControlPlayer.Instante.transform;
        anim = GetComponent<Animator>();
        warmingSign = transform.GetChild(transform.childCount - 1).gameObject;
    }

    private void LateUpdate()
    {
        PlayerIsVisibleThroughEnemy(player);
    }

    private void FixedUpdate()
    {
        if(!stop)
            Patrolling();
    }

    private bool PlayerIsVisibleThroughEnemy(Transform target)
    {
        var distFromPlayer = Vector3.Distance(transform.position, target.position);
        if(distFromPlayer < 2.3f)
        {
            GameManager.Instance.GameOver();
            anim.SetInteger("action", 2);
            warmingSign.SetActive(true);

            //rotate enemy to the player
            Vector3 dir = (target.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, speedRot * Time.fixedDeltaTime);

            //disabled this component
            GetComponent<EnemyAi>().enabled = false;
        }
        else if(distFromPlayer > 2.3f && distFromPlayer < 4f)
            warmingSign.SetActive(true);
        else
            warmingSign.SetActive(false);
        return true;
    }

    private void Patrolling()
    {
        if(Vector3.Distance(transform.position, pointToWalk[currentPoint].position) > 0.4f)
        {
            Vector3 dir = (pointToWalk[currentPoint].position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, speedRot * Time.fixedDeltaTime);
            transform.Translate(Vector3.forward * speedMove * Time.fixedDeltaTime);
            anim.SetInteger("action", 1);
        }
        else
        {
            //stop enemy for 5 seconds
            StartCoroutine(StopPatrol());
            stop = true;

            if (currentPoint < pointToWalk.Length - 1)
                currentPoint++;
            else
                currentPoint = 0;
        }
    }

    private IEnumerator StopPatrol()
    {
        anim.SetInteger("action", 0);
        yield return new WaitForSecondsRealtime(5f);
        stop = false;
    }
}
