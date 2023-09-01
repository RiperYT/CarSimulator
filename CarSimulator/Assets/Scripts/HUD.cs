using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VehiclePhysics;

public class HUD : MonoBehaviour
{
    [SerializeField] private CarInfo _carInfo;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _RPM;
    [SerializeField] private TextMeshProUGUI _statusOn;
    [SerializeField] private TextMeshProUGUI _statusOff;
    [SerializeField] private TextMeshProUGUI _gearNumber;
    [SerializeField] private TextMeshProUGUI _gearType;
    [SerializeField] private GameObject _distance;
    [SerializeField] private TextMeshProUGUI _distanceNumber;

    // Updating HUD
    void FixedUpdate()
    {
        if (_carInfo)
        {
            _speed.text = Math.Round(_carInfo.GetSpeed()).ToString() + " km/h";
            _RPM.text = Math.Round(_carInfo.GetRPM()).ToString() + " RPM";
            if (_carInfo.IsActivated()) { _statusOn.gameObject.SetActive(true); _statusOff.gameObject.SetActive(false); } else { _statusOn.gameObject.SetActive(false); _statusOff.gameObject.SetActive(true); }
            switch(_carInfo.GetGearPosition())
            {
                case -1:
                    _gearNumber.text = "R";
                    break;
                case 0:
                    _gearNumber.text = "N";
                    break;
                default:
                    _gearNumber.text = _carInfo.GetGearPosition().ToString();
                    break;
            }
            switch (_carInfo.GetGearMode())
            {
                case Gearbox.Type.Manual:
                    _gearType.text = "M";
                    break;
                case Gearbox.Type.Automatic:
                    _gearType.text = "A";
                    break;
            }
            switch (_carInfo.DistanceToTheNearestCar())
            {
                case -1:
                    _distance.gameObject.SetActive(false);
                    break;
                default:
                    _distance.gameObject.SetActive(true);
                    _distanceNumber.text = Math.Round(_carInfo.DistanceToTheNearestCar()).ToString();
                    break;
            }
        }
    }
}
