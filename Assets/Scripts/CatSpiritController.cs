using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatSpiritController : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public GameObject deadCat;


    private NavMeshAgent agent;
    private float timer;


    private void Start()
    {
        //eviter que l'esprit pousse le chat
        Physics.IgnoreCollision(GetComponent<Collider>(), deadCat.GetComponent<Collider>());
    }

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(deadCat.transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }       
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
