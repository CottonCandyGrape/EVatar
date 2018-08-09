using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeHelpers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>() as T;
                    if (!instance)
                    {
                        Debug.LogWarning("There's no active " + typeof(T) + " in this scene.");
                        return null;
                    }
                }

                return instance;
            }
        }
    }
}