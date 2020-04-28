using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public GameObject target;
    internal Transform trans;
    private Vector3 cameraPosition;
    private float moveSpeed = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        trans = GetComponent<Transform>();
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            cameraPosition.Set(target.transform.position.x, target.transform.position.y, -10f);
            trans.position = Vector3.Lerp(trans.position, cameraPosition, moveSpeed * Time.deltaTime);
        }
    }
}
