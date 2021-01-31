using System;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class MessageGenerator : MonoBehaviour
{
	[SerializeField]
	private float countdown = 5;
	[SerializeField]
	private float minimumTime = 3;
	[SerializeField]
	private float maximumTime = 8;

	[Serializable]
	private class MessageEvent : UnityEvent<string> { }

	[SerializeField]
	private MessageEvent OnMessage;

	[SerializeField]
	private string[] messages;

	private void Update()
	{
		countdown -= Time.deltaTime;

		if (countdown <= 0)
		{
			countdown += Toast.TOAST_TIME + Random.Range(minimumTime, maximumTime);
			string message = RandomMessage;

			OnMessage.Invoke(message);
			Debug.Log($"Message: {message}");
		}
	}

	private string RandomMessage => messages[Random.Range(0, messages.Length)];

    public void onSMS(string message) {
        Debug.Log($"Message: {message}");
        OnMessage.Invoke(message);
    }
}
