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
	[Serializable]
	private class MessageSpriteEvent : UnityEvent<Sprite> { }

	[SerializeField]
	private MessageEvent OnMessage;
	[SerializeField]
	private MessageSpriteEvent OnMessageSprite;

	[SerializeField]
	private string[] messages;
	[SerializeField]
	private Sprite[] messageSprites;

	private void Update()
	{
		countdown -= Time.deltaTime;

		if (countdown <= 0)
		{
			countdown += Toast.TOAST_TIME + Random.Range(minimumTime, maximumTime);
			var randomSprite = RandomSprite;

			OnMessageSprite.Invoke(randomSprite);
			Debug.Log($"Message: {randomSprite}");
		}
	}

	private string RandomMessage => messages[Random.Range(0, messages.Length)];
	private Sprite RandomSprite => messageSprites[Random.Range(0, messageSprites.Length)];
}
