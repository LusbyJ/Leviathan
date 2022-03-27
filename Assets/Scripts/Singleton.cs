using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	static private T _instance;
	static public T instance{
		get{
			if(_instance == null){
				_instance = FindObjectOfType<T>();
			}
			return _instance;
		}
	}
}
