using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    private Transform target;
    public float height = 50;

    private void Start()
    {
        target = AssetsManager.Instance.Player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, height, target.position.z);

        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, AssetsManager.Instance.MainCamera.transform.eulerAngles.y, 0);

    }
}
