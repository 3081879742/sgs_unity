using System.Collections.Generic;
using System;
using UnityEngine;

// public class JsonClass
// {
//     public static List<T> FromJson<T>(string json)
//     {
//         return JsonUtility.FromJson<JsonList<T>>(json).list;
//     }

//     // public static Dictionary<TKey, TValue> DicFromJson<TKey, TValue>(string json)
//     // {
//     //     return JsonUtility.FromJson<JsonDictionary<TKey, TValue>>(json).dictionary;
//     // }
// }
public class JsonList<T>
{
    public List<T> list;

    public static List<T> FromJson(string json)
    {
        return JsonUtility.FromJson<JsonList<T>>(json).list;
    }
}

// public class JsonDictionary<TKey, TValue>
// {
//     public Dictionary<TKey, TValue> dictionary;
// }

[Serializable]
public class ResultResponse
{
    public string result;
}

[Serializable]
public class GetinfoResponse
{
    public string result;
    public string username;
    public string portrait;
}

[Serializable]
public class CardJson
{
    public int id;
    public string suit;
    public int weight;
    public string type;
    public string name;
}

[Serializable]
public class WebsocketJson
{
    public string eventname;
    // public string username;
    // public List<string> players;

    // public bool timerResult;
    // public List<int> cards;
    // public List<int> dests;
    // public List<int> equipages;
}

[Serializable]
public class CreatePlayerJson
{
    public string eventname;
    public string username;
}

[Serializable]
public class StartGameJson
{
    public string eventname;
    public List<string> players;
}

[Serializable]
public class TimerJson
{
    public string eventname;
    public int id;
    public bool result;
    public List<int> cards;
    public List<int> dests;
    public List<int> equipages;
    public int src;
}

[Serializable]
public class PhaseJson
{
    public string eventname;
    public int id;
    public int player;
    public Phase phase;
}

[Serializable]
public class CardPileJson
{
    public string eventname;
    public int id;
    public List<int> cards;
}