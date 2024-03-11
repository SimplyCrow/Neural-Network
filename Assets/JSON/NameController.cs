using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

public class NameController
{

    public static string GetRandomStringFromJson()
    {
        JArray jsonArray = JArray.Parse(CircleGenrator.nameJson);

        List<string> stringList = jsonArray.ToObject<List<string>>();

        int randomIndex = UnityEngine.Random.Range(0, stringList.Count);
        string randomString = stringList[randomIndex];

        return randomString;
    }


}
