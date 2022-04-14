using System;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;

public class PathFollower : MonoBehaviour
{
	[SerializeField] private float _pathFollowSpeed = 1;

	[SerializeField] private Slider _slider;
	[SerializeField] private PathCreator pathCreator;
	[SerializeField] private StackManager _stackManager;

	private EndOfPathInstruction endOfPathInstruction;
	private float distanceTravelled;

	public static Action PathEnded;

	private void Start()
	{
		_slider.maxValue = 1;
		endOfPathInstruction = EndOfPathInstruction.Stop;
		enabled = false;
	}

	private void FixedUpdate()
	{
		MoveAndRotate();
		SetSliderValue();
		CheckPathEnded();
	}

	private void MoveAndRotate()
	{
		distanceTravelled += _pathFollowSpeed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
		var rotation = new Quaternion(
			0,
			pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).y,
			0,
			pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).w);
		transform.rotation = rotation;
	}

	private void SetSliderValue()
	{
		_slider.value = pathCreator.path.GetClosestTimeOnPath(transform.position);
	}

	private void CheckPathEnded()
	{
		// Path start -> 0 end -> 1
		if (1 - pathCreator.path.GetClosestTimeOnPath(transform.position) < float.Epsilon)
		{
			PathEnded?.Invoke();
			enabled = false;
		}
	}
}