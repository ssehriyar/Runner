using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class StackManager : MonoBehaviour
{
	public Stack<Block> Blocks { get; private set; } = new Stack<Block>();
	public bool IsStackEmpty { get; private set; } = true;

	[SerializeField] private Transform _pathFollow;
	[SerializeField] private Transform _block;
	[SerializeField] private Transform _blockStep;
	[SerializeField] private Transform _ladders;
	[SerializeField] private Transform _risePoint;
	[SerializeField] private Transform _hips;

	private float _currentRow;
	private float _nextRow;
	private float _columnPos1;
	private float _columnPos2;
	private float _columnPos3;

	private void Start()
	{
		transform.SetParent(_hips);
		_nextRow = _block.localScale.y + 0.01f;
		_currentRow = -_nextRow;

		_columnPos1 = 0f;
		_columnPos2 = -(_block.localScale.z + 0.01f);
		_columnPos3 = _columnPos2 * 2;
	}

	public void PushBlock(Block block)
	{
		block.ChangeCollide(false);
		block.transform.SetParent(transform);
		block.transform.DOLocalMove(EmptyPositon(), 0f);
		block.transform.DOLocalRotate(Vector3.zero, 0f);

		Blocks.Push(block);
		IsStackEmpty = false;
	}

	public void PopBlock()
	{
		if (Blocks.Count % 3 == 1)
			_currentRow -= _nextRow;

		var block = Blocks.Pop();
		block.transform.SetParent(_ladders);
		block.transform.DOLocalMove(_risePoint.position, 0f);
		block.transform.DOLocalRotate(_pathFollow.localRotation.eulerAngles, 0f);
		block.transform.DOScale(_blockStep.localScale, 0f);

		if (Blocks.Count <= 0)
			IsStackEmpty = true;
	}

	private Vector3 EmptyPositon()
	{
		return (Blocks.Count % 3) switch
		{
			0 => new Vector3(0, _currentRow += _nextRow, _columnPos1),
			1 => new Vector3(0, _currentRow, _columnPos2),
			2 => new Vector3(0, _currentRow, _columnPos3),
			_ => new Vector3(0, _currentRow, _columnPos1),
		};
	}
}