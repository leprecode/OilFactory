using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryCollection:", menuName = "ScriptableObjects/NewFactoryCollection", order = 2)]
public class FactoriesCollection : ScriptableObject
{
    [SerializeField] private List<FactoryModel> _factories;

    public FactoryModel GetFactory(int factoryLevel)
    {
        if (factoryLevel <= _factories.Count)
            return _factories[factoryLevel];
        else
            return null;
    }
}
