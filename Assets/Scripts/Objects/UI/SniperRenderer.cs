using UnityEngine;
using System.Collections;

public class SniperRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform target;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        target = GameManager.instance.player.transform;

        lineRenderer.SetColors(Color.yellow, Color.red);
        lineRenderer.SetWidth(0.05f, 0.05f);
    }

    public void ShowLaser()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);
    }
    public void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}