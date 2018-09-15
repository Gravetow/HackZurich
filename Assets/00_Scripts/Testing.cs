using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var api = new OpenBankingAPI();
        StartCoroutine(api.getTransactions(13));
        StartCoroutine(api.getCustomerInfo(OpenBankingAPI.customerNick));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
