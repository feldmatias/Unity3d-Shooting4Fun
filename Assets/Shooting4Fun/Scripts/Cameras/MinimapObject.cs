using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObject : MonoBehaviour
{

    private IDeathable target;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<IDeathable>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.SetActive(!target.IsDead());
    }
}
