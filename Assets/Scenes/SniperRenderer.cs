using UnityEngine;
using System.Collections;

public class SniperRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform target;


    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        target = GameManager.instance.player.transform;

        lineRenderer.SetWidth(0.05f, 0.05f);
    }

    public void ShowLaser()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);
    }
}