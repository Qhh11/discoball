using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerControllerInput : MonoBehaviour
{
	private PlayerInput _playerInput;
	private IObservable<Vector2> _move;
	private IObservable<Vector2> _look;
	private ReadOnlyReactiveProperty<bool> _run;
	public PlayerInput playerInput => _playerInput;
	public IObservable<Vector2> move => _move;
	public IObservable<Vector2> look => _look;
	public ReadOnlyReactiveProperty<bool> run => _run;


	private IObservable<Unit> _volumeUp;
	private IObservable<Unit> _volumeDown;

	public IObservable<Unit> VolumeUp => _volumeUp;
	public IObservable<Unit> VolumeDown => _volumeDown;

	private void Awake()
	{
		_playerInput = new PlayerInput();
		_playerInput.Player.Enable();

		_move = this.UpdateAsObservable()
			.Select(_ => _playerInput.Player.Move.ReadValue<Vector2>());

			
			
		_look = this.UpdateAsObservable()
			.Select(_ => _playerInput.Player.Look.ReadValue<Vector2>());

		_run = this.UpdateAsObservable()
			.Select(_ => _playerInput.Player.Run.ReadValueAsObject() != null)
			.ToReadOnlyReactiveProperty();
	
		_volumeUp = this.UpdateAsObservable()
			.Where(_ => _playerInput.Player.VolumeUp.triggered)
			.AsUnitObservable();

		_volumeDown = this.UpdateAsObservable()
			.Where(_ => _playerInput.Player.VolumeDown.triggered)
			.AsUnitObservable();
	}

	private void OnDisable()
		{
			_playerInput.Player.Disable();
		}

	}
