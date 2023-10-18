using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInfo
{
    public string name;
    public string phone;
    public string email;
    public int age;
    public bool gender;
}

[System.Serializable]
public class ShopInfo
{
    public string name;
    public int price;
    public int model;
}

public class DataManager : MonoBehaviour
{
    public List<UserInfo> allUser;
    public List<ShopInfo> allProduct;

    void Start()
    {
        //allUser = CSV.instance.Parse("CSV_UNITY");

        allUser = CSV.instance.Parse<UserInfo>("CSV_UNITY");
        allProduct = CSV.instance.Parse<ShopInfo>("CSV_SHOP");
    }

    void Update()
    {
        
    }
}
