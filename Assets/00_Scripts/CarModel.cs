using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cars")]
public class CarModel : ScriptableObject
{
    public List<GameObject> Cars = new List<GameObject>();
}

