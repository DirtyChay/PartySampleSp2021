using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttacker : MonoBehaviour
{
    public float lifespan; // not sure how to get this from RangedAttack.cs
    private float _age;

    // Start is called before the first frame update
    void Start()
    {
        _age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_age > lifespan)
        {
            Destroy(this.gameObject);
        }
        _age += Time.deltaTime;
    }
}