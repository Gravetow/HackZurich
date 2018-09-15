using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class OpenBankingAPI
{
    public const string baseUrl = "https://csopenbankingzh.azurewebsites.net";
    public const string bearerToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ik9UUXlRVGRCTkRFNU1rWTNNMFZFTnpNeU9EWkJOa1pETkRCR1FrRkZOamxDTTBJeE5EazRNdyJ9.eyJpc3MiOiJodHRwczovL2dicHJvamVjdC5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NWI5Y2Q5MGRmMTY0MjcyMWFkODc3ZDJiIiwiYXVkIjpbImh0dHBzOi8vb3BlbmJhbmtpbmcuY29tL2FwaSIsImh0dHBzOi8vZ2Jwcm9qZWN0LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE1MzcwMDYwODIsImV4cCI6MTUzNzA5MjQ4MiwiYXpwIjoicThJTVlzMVNLekt0WExvdjhWM0pab3oxaE43MHgzQTciLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwiZ3R5IjoicGFzc3dvcmQifQ.T1J0luSgVNmO6tmUdSHbwF5ijc084kZAWUlOyhvGmdE8ELcVSemKjYlqfRu_2TwuedqwlsZA-rFn5i93YVMO-mDGTx1CQunWHmR8EQ2TWR8yZckMd0CKVZvqsYWyOXpcgpYDXN5kHRtiSnDayCdd-qvsdBsPiOn7sX5D_SxtNsCtMpUsk45_OXao9IBBLtTRmtfMlhBL1-zOIuVHM10dQdF0xBwpumTxC-Rr21m35DvtaGv2HM1QaC2NYNF96MOtIU342VuMgilEh8f3pAU1QTeIv2WDbE0zKVkuldVimRdRMovovko7PxCQ06p8y5AkWPKMt-lCvnJsobwbbigDYA";
    public const int userId = 13;
    public const string customerNick = "nickname100223";

    public OpenBankingAPI()
    {
        Debug.Log("API instantiated");
    }

    public IEnumerator getCustomerInfo(string nickname)
    {
        var accountQuery = string.Format("/customers?nickname={0}", nickname);
        var uri = buildURL(accountQuery);
        var request = UnityWebRequest.Post(uri, "");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("Authorization", bearerToken);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
            var responseText = request.downloadHandler.text;
            // "Dirty unwrap" the array
            responseText = responseText.Substring(1, responseText.Length-2);
            Debug.Log(responseText);

            Customer customer = JsonUtility.FromJson<Customer>(responseText);
            Debug.Log(customer.kyc.annualSpending);
        }
    }

    public IEnumerator getTransactions(int accountId)
    {
        var accountQuery = string.Format("/accounts/{0}/cashtransactions", accountId);
        var uri = buildURL(accountQuery);
        var request = UnityWebRequest.Get(uri);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("Authorization", bearerToken);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
            var responseText = request.downloadHandler.text;

            // The unity mapper uses the field names for parsing. `Object` is a reserved keyword, so we replace it with `Items`
            responseText = responseText.Replace("\"object\"", "\"Items\"");
            Transaction[] tx = JsonHelper.FromJson<Transaction>(responseText);
            Debug.Log(tx[0].id);
        }
    }

    private string buildURL(string endpoint)
    {
        return baseUrl + "/" + endpoint;
    }


}

[Serializable]
public class KYC
{
    public string id;
    public string annualIncome;
    public string annualSpending;
    public string totalWealth;
}

[Serializable]
public class Customer
{
    public string id;
    public string nickname;
    public KYC kyc;
    public string[] hobbies;
}

[Serializable]
public class Transaction
{
    public string id;
    public string date;
    public double amount;
}


// As seen on: https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity/36244111#36244111
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}