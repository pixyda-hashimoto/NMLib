using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
	/// <summary>
	/// 
	/// </summary>
	[AddComponentMenu("")]
	public class LogHandlerComponent : MonoBehaviour
	{
		private void OnEnable()	 { Application.logMessageReceived += this.OnReceived; }
		private void OnDisable() { Application.logMessageReceived -= this.OnReceived; }
		private void OnReceived(string condition, string stackTrace, LogType type)
		{
			switch (type) {
			case LogType.Log:
			case LogType.Warning:
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				break;
			}

			//Debug.Log(type+":"+condition);
		}
	}
}