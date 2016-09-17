﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	#region Public
	public Transform target;

	public bool down = false;
	#endregion

	#region Private
	private float _prevX;
	private float _prevY;

	private Vector2 _delta = new Vector2(0.0f, 0.0f);

	private Transform camTransform;
	#endregion

	#region Event
	void Awake()
	{
		if (target == null)
		{
			target = transform;
		}

		var cam = Camera.main;
		camTransform = cam.transform;
	}

	void Start ()
	{
		
	}

	void Update()
	{
		Controller();
	}
	#endregion

	#region Private Methods

	private void Controller ()
	{
		#if UNITY_EDITOR

		if (Input.GetMouseButtonDown(0))
		{
			_delta.x = 0.0f;
			_delta.y = 0.0f;
			_prevX = Input.mousePosition.x;
			_prevY = Input.mousePosition.y;
			down = true;
		}

		if (Input.GetMouseButtonUp(0))
		{
			down = false;
		}

		if (down)
		{
			_delta.x = _prevX - Input.mousePosition.x;
			_delta.y = _prevY - Input.mousePosition.y;
			_prevX = Input.mousePosition.x;
			_prevY = Input.mousePosition.y;

//			Vector3 aular = new Vector3(_delta.y, -_delta.x, 0.0f);

//			Debug.Log(""+target.eulerAngles.x);
//			Debug.Log(""+target.eulerAngles.y);
//			Debug.Log("-----------------------------------------");
//			Debug.Log(""+aular.x);
//			Debug.Log(""+aular.y);
//			Debug.Log(""+aular.z);
//			Debug.Log("*****************************************");

			var x = _delta.y;// + target.eulerAngles.x;
			var y = -_delta.x + target.eulerAngles.y;

			// // 現在の回転角度を0～360から-180～180に変換
			var rotate = (target.transform.eulerAngles.x > 180) ? target.transform.eulerAngles.x - 360 : target.transform.eulerAngles.x;

			// 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
			var angle = Mathf.Clamp(rotate + x, -40, 40);

			// 回転角度を-180～180から0～360に変換
			angle = (angle < 0) ? angle + 360 : angle;

//			x = Mathf.Clamp(x, -40, 40);

			target.rotation = Quaternion.Euler(angle, y, 0);

//			target.Rotate(aular, Space.World);
//
//			Vector3 currentRotation = target.localRotation.eulerAngles;
//			currentRotation.x = Mathf.Clamp(currentRotation.x, -40, 40);
//			target.localRotation = Quaternion.Euler (currentRotation);
		}

		// マウスホイールによるズーム
		var wheel = Input.GetAxis("Mouse ScrollWheel");
		camTransform.Translate(0, 0, wheel);

		#endif
	}

	#endregion
}
