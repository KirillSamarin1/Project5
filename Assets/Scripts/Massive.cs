using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int n = 10;
        if(n > 0)
        {
            for (int i = 2; i < n; i++)
            {
                if(n % i == 0)
                {
                    Debug.Log($"Число {n} не простое");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
