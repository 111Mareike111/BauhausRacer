using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SCC_Inputs)), CanEditMultipleObjects]
public class SCC_InputsEditor : Editor {

	public override void OnInspectorGUI () {
		
		DrawDefaultInspector ();

		EditorGUILayout.HelpBox ("Receives inputs from Unity's InputManager. You can edit InputManager from Edit --> Project Settings --> Input. If you want to feed cars by your inputs, all you have to do is feed these float values in this script;" +
			" \n \n " +
			"Gas (0f - 1f) = gas \n Brake (0f - 1f) = brake \n Steering (-1f - 1f) = steering \n Handbrake (0f - 1f) = handbrake.", MessageType.Info);

	}

}
