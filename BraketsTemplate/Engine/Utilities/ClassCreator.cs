using System;
using System.Threading.Tasks;
namespace BraketsEngine;

public class ClassCreator
{
    public static async Task<object> Create(string name, object[] args)
    {
        Type type = Type.GetType(name);

        if (type == null)
        {
            type = Type.GetType(name + ", " + typeof(Program).Assembly.FullName);
        }

        if (type != null)
        {
            var constructorInfo = type.GetConstructor(Array.ConvertAll(args, a => a.GetType()));

            if (constructorInfo != null)
            {
                var instance = constructorInfo.Invoke(args);

                var initializeMethod = type.GetMethod("Init");
                if (initializeMethod != null)
                {
                    var initializeTask = (Task)initializeMethod.Invoke(instance, null);
                    await initializeTask;
                }

                return instance;
            }
            else
            {
                Debug.Error($"[UTILITY] No matching constructor found for type '{name}' with the specified parameters!");
            }
        }
        else
        {
            Debug.Error($"[UTILITY] Failed to create class of type '{name}'!");
        }

        return null;
    }
}