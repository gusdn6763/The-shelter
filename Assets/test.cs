using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

public class test : MonoBehaviour
{
    private PolyNavAgent _agent;

    public GameObject target;
    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

    private void Update()
    {
        agent.rotateTransform =
        agent.SetDestination(target.transform.position);
    }
}
