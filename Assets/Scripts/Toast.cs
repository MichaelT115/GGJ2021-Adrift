using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
	public const float TOAST_TIME = 7f;
	private const float REVEAL_TIME = 1f;

	[SerializeField]
    private RectTransform toast;

	[SerializeField]
	private float time;

	[SerializeField]
	private TMPro.TMP_Text text;
	[SerializeField]
	private Image image;

	private void Start()
	{
		NormalizePosition = 0;
		enabled = false;
	}

	public void DisplayMessage(string message)
	{
		text.text = message;
		time = 0;
		enabled = true;
	}

	public void DisplayMessage(Sprite sprite)
	{
		image.sprite = sprite;
		time = 0;
		enabled = true;
	}

	private void Update()
	{
		time += Time.deltaTime;

		if (time < REVEAL_TIME)
		{
			NormalizePosition = time / REVEAL_TIME;
		}
		else if (time < (TOAST_TIME - REVEAL_TIME))
		{
			NormalizePosition = 1;
		}
		else
		{
			float timeIntoAnimation = time - (TOAST_TIME - REVEAL_TIME);
			float normalizedTimeIntoAnimation = timeIntoAnimation / REVEAL_TIME;
			NormalizePosition = 1 - normalizedTimeIntoAnimation;

			if (NormalizePosition == 0)
			{
				enabled = false;
			}
		}
	}

	public float NormalizePosition
	{
		get => toast.anchoredPosition.y / toast.rect.height;
		set
		{
			var y = -toast.rect.height * Mathf.Clamp01(value);
			var anchoredPosition = toast.anchoredPosition;
			anchoredPosition.y = y;
			toast.anchoredPosition = anchoredPosition;

		}
	}
}
