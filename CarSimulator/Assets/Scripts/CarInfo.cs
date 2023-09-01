using EdyCommonTools;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VehiclePhysics;

public class CarInfo : MonoBehaviour
{
    [SerializeField] private Collider _ownColider;

    private Camera _camera;
    private VehicleBase _vehicleBase;
    private VPVehicleController _controller;

    private List<Collider> _vehicles = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _vehicleBase = gameObject.GetComponent<VehicleBase>();
        _controller = gameObject.GetComponent<VPVehicleController>();

        var collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 25;
    }

    private void Update()
    {
        Debug.Log(IsActivated());
    }

    //Get speed of the car
    public float GetSpeed()
    {
        if (_vehicleBase)
            return _vehicleBase.data.Get(Channel.Vehicle, VehicleData.Speed) / 1000.0f * 3.6f;
        else
            return 0;
    }

    //Get engine's RPM of the car
    public float GetRPM()
    {
        if (_vehicleBase)
            return _vehicleBase.data.Get(Channel.Vehicle, VehicleData.EngineRpm) / 1000.0f;
        else
            return 0;
    }

    //Get engine status
    public bool IsActivated()
    {
        _vehicleBase.data.Get(Channel.Vehicle, VehicleData.EngineWorking);
        if (_vehicleBase)
            return _vehicleBase.data.Get(Channel.Vehicle, VehicleData.EngineWorking) == 1 ? true : false;
        else
            return false;
    }

    //Get active gear position
    public int GetGearPosition()
    {
        if (_vehicleBase)
            return _vehicleBase.data.Get(Channel.Vehicle)[VehicleData.GearboxGear];
        else
            return 0;
    }

    //Get transmission operating mode
    public Gearbox.Type? GetGearMode()
    {
        if (_controller)
            return _controller.gearbox.type;
        else
            return null;
    }

    //Get distance to the nearest car in player's view
    public float DistanceToTheNearestCar()
    {
        var min = 30f;

        foreach(var car in _vehicles)
            if (PointInCameraView(car.transform.position))
            {
                var point1 = _ownColider.ClosestPointOnBounds(car.gameObject.transform.position);
                var point2 = car.ClosestPointOnBounds(point1);

                var point3 = car.ClosestPointOnBounds(_ownColider.transform.position);
                var point4 = _ownColider.ClosestPointOnBounds(point3);

                min = Math.Min(min, Math.Min(Vector3.Distance(point1, point2), Vector3.Distance(point3, point4)));
            }

        if (min >= 0 && min < 20)
            return min;
        else
            return -1;
    }

    private void OnTriggerEnter(Collider car)
    {
        if (car.tag == "Car")
        {
            _vehicles.Add(car);
        }
    }

    private void OnTriggerExit(Collider car)
    {
        if (car.tag == "Car")
        {
            _vehicles.Remove(car);
        }
    }

    private bool PointInCameraView(Vector3 point)
    {
        Vector3 viewport = _camera.WorldToViewportPoint(point);
        bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
        bool inFrontOfCamera = viewport.z > 0;

        /*RaycastHit depthCheck;
        bool objectBlockingPoint = false;

        Vector3 directionBetween = point - _camera.transform.position;
        directionBetween = directionBetween.normalized;

        float distance = Vector3.Distance(_camera.transform.position, point);

        if (Physics.Raycast(_camera.transform.position, directionBetween, out depthCheck, distance + 0.05f))
        {
            if (depthCheck.point != point)
            {
                objectBlockingPoint = true;
            }
        }*/

        return inCameraFrustum && inFrontOfCamera;
    }

    private bool Is01(float a)
    {
        return a > 0 && a < 1;
    }
}
