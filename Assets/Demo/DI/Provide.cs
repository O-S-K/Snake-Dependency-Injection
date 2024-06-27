using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provide 
{
    private static Provide instance;
    private readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

    public static Provide Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Provide();
            }
            return instance;
        }
    }

    public void Bind<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        instances[typeof(TInterface)] = new TImplementation();
    }

    public void ProvideInstance<TInterface>(TInterface instance)
    {
        instances[typeof(TInterface)] = instance;
    }

    public TInterface Resolve<TInterface>()
    {
        if (instances.TryGetValue(typeof(TInterface), out object instance))
        {
            return (TInterface)instance;
        }
        else
        {
            Debug.LogError($"No binding found for type {typeof(TInterface)}");
            return default;
        }
    }
}
