using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryCollection:", menuName = "ScriptableObjects/NewFactoryCollection", order = 2)]
public class FactoriesCollection : ScriptableObject
{
    [SerializeField] private List<FactorySettings> _factories = new List<FactorySettings>();

    public FactorySettings GetFactory(int factoryLevel)
    {
        Debug.Log("lvl" + factoryLevel);
        Debug.Log("CountFactories" + _factories.Count);
        Debug.Log("factory" + _factories[factoryLevel]);

        if (factoryLevel <= _factories.Count)
            return _factories[factoryLevel];
        else
            return null;
    }

    public bool CheckNextFactoryLevel(FactorySettings currentSettings)
    {
        if (_factories.IndexOf(currentSettings) == _factories.Count-1)
            return false;

        return true;
    }

    public FactorySettings GetNextFactory(FactorySettings currentSettings)
    {
        if (CheckNextFactoryLevel(currentSettings))
        {
            return _factories[(_factories.IndexOf(currentSettings) + 1)];
        }

        return null;
    }


}
