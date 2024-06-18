using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManager : EditorWindow
{
  [MenuItem("Tools/Waypoint Editor")]
  public static void Open()
  {
    GetWindow<WaypointManager>();
  }

  public Transform _waypointRoot;

  private void OnGUI()
  {
    SerializedObject obj = new SerializedObject(this);
    EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
    if (_waypointRoot == null)
    {
      EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
    }
    else
    {
      EditorGUILayout.BeginVertical("box");
      DrawButtons();
      EditorGUILayout.EndVertical();
    }

    obj.ApplyModifiedProperties();
  }

  private void DrawButtons()
  {
    if (GUILayout.Button("Create Waypoint"))
    {
      CreateWaypoint();
    }

    if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoint>())
    {
      if (GUILayout.Button("Create Waypoint Before"))
      {
        CreateWaypointBefore();
      }

      if (GUILayout.Button("Create Waypoint After"))
      {
        CreateWaypointAfter();
      }

      if (GUILayout.Button("Remove Waypoint"))
      {
        RemoveWaypoint();
      }
    }
    
  }

  private void CreateWaypointBefore()
  {
    GameObject waypointObject = new GameObject("Waypoint" + _waypointRoot.childCount, typeof(WayPoint));
    waypointObject.transform.SetParent(_waypointRoot, false);

    WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();
    WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();
    waypointObject.transform.position = selectedWaypoint.transform.position;
    waypointObject.transform.forward = selectedWaypoint.transform.forward;

    if (selectedWaypoint._previousWaypoint != null)
    {
      newWaypoint._previousWaypoint = selectedWaypoint._previousWaypoint;
      selectedWaypoint._previousWaypoint._nextWaypoint = newWaypoint;
    }

    newWaypoint._nextWaypoint = selectedWaypoint;
    selectedWaypoint._previousWaypoint = newWaypoint;
    newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
    Selection.activeGameObject = newWaypoint.gameObject;
  }

  private void CreateWaypointAfter()
  {
    GameObject waypointObject = new GameObject("Waypoint" + _waypointRoot.childCount, typeof(WayPoint));
    waypointObject.transform.SetParent(_waypointRoot, false);

    WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();
    WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();
    waypointObject.transform.position = selectedWaypoint.transform.position;
    waypointObject.transform.forward = selectedWaypoint.transform.forward;

    newWaypoint._previousWaypoint = selectedWaypoint;
    if (selectedWaypoint._nextWaypoint != null)
    {
      selectedWaypoint._nextWaypoint._previousWaypoint = newWaypoint;
      newWaypoint._nextWaypoint = selectedWaypoint._nextWaypoint;
      
    }

    selectedWaypoint._nextWaypoint = newWaypoint;
    newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
    Selection.activeGameObject = newWaypoint.gameObject;
  }

  private void RemoveWaypoint()
  {
    WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();
    if (selectedWaypoint._nextWaypoint != null)
    {
      selectedWaypoint._nextWaypoint._previousWaypoint = selectedWaypoint._previousWaypoint;
    }

    if (selectedWaypoint._previousWaypoint != null)
    {
      selectedWaypoint._previousWaypoint._nextWaypoint = selectedWaypoint._nextWaypoint;
      Selection.activeGameObject = selectedWaypoint._previousWaypoint.gameObject;
    }
    DestroyImmediate(selectedWaypoint.gameObject);
  }

  private void CreateWaypoint()
  {
    GameObject waypointObject = new GameObject("Waypoint" + _waypointRoot.childCount, typeof(WayPoint));
    waypointObject.transform.SetParent(_waypointRoot, false);
    WayPoint wayPoint = waypointObject.GetComponent<WayPoint>();
    if (_waypointRoot.childCount > 1)
    {
      wayPoint._previousWaypoint = _waypointRoot.GetChild(_waypointRoot.childCount - 2).GetComponent<WayPoint>();
      wayPoint._previousWaypoint._nextWaypoint = wayPoint;

      wayPoint.transform.position = wayPoint._previousWaypoint.transform.position;
      wayPoint.transform.forward = wayPoint._previousWaypoint.transform.forward;
    }

    Selection.activeGameObject = wayPoint.gameObject;
  }
}
