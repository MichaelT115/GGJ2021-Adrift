using System;
using UnityEngine;

[Serializable]
public class Tier
{
	[SerializeField]
	private Instructions instructions;

	public Instructions Instructions { get => instructions; set => instructions = value; }
}
