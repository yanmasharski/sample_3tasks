using System;
using UnityEngine;

[Serializable]
public class DataMessage
{
    [SerializeField] private string name;
    [SerializeField] private string text;

    public string userName => name;
    public string message => text;
}