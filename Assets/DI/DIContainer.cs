using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DIContainer : MonoBehaviour
{
    private static Dictionary<Type, Func<object>> bindings = new Dictionary<Type, Func<object>>();

    
    /// <summary>
    /// Bind an interface to an implementation
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public static void Bind<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        bindings[typeof(TInterface)] = () => new TImplementation();
    }

    
    /// <summary>
    /// Bind an interface to a provider
    /// </summary>
    /// <param name="provider"></param>
    /// <typeparam name="TInterface"></typeparam>
    public static void Bind<TInterface>(Func<object> provider)
    {
        bindings[typeof(TInterface)] = provider;
    }

    /// <summary>
    /// Resolve an interface to an implementation
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TInterface Resolve<TInterface>()
    {
        if (bindings.ContainsKey(typeof(TInterface)))
        {
            var instanceProvider = bindings[typeof(TInterface)];
            if (instanceProvider != null)
            {
                return (TInterface)instanceProvider();
            }
            else
            {
                throw new Exception($"No binding found for {typeof(TInterface)}");
            }
        }
        else
        {
            throw new Exception($"No binding found for {typeof(TInterface)}");
        }
    }

    
    /// <summary>
    /// Inject dependencies into an object
    /// </summary>
    /// <param name="target"></param>
    public static void Inject(object target)
    {
        var targetType = target.GetType();
        var fields = targetType.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (bindings.ContainsKey(field.FieldType))
            {
                var value = bindings [field.FieldType]();
                field.SetValue(target, value);
            }
        }
    }
    
    /// <summary>
    ///  Inject dependencies into an array of objects
    /// </summary>
    /// <param name="target"></param>
    public static void Injects(params object[] targets)
    {
        foreach (var target in targets)
        {
            var targetType = target.GetType();
            var fields = targetType.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (bindings.ContainsKey(field.FieldType))
                {
                    var value = bindings[field.FieldType]();
                    field.SetValue(target, value);
                }
            }
        }
    }
}