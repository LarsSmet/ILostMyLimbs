using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LimbInfo;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    [SerializeField] private bool _isMainStatue = false;

    [SerializeField] private Transform _cameraSocket;
    [SerializeField] private Transform _leftArmSocket;
    [SerializeField] private Transform _rightArmSocket;
    [SerializeField] private Transform _leftLegSocket;
    [SerializeField] private Transform _rightLegSocket;

    
    public Transform CameraSocket { get { return _cameraSocket; } }
    public Transform LeftArmSocket { get { return _leftArmSocket; } }
    public Transform RightArmSocket { get { return _rightArmSocket; } }
    public Transform LeftLegSocket { get { return _leftLegSocket; } }
    public Transform RightLegSocket { get { return _rightLegSocket; } }

    [SerializeField] private bool _leftArmMissing = false;
    [SerializeField] private bool _rightArmMissing = false;
    [SerializeField] private bool _leftLegMissing = false;
    [SerializeField] private bool _rightLegMissing = false;
    public bool LeftArmMissing { get { return _leftArmMissing; } set { _leftArmMissing = value; } }
    public bool RightArmMissing { get { return _rightArmMissing; } set { _rightArmMissing = value; } } 
    public bool LeftLegMissing { get { return _leftLegMissing; } set { _leftLegMissing = value; } }
    public bool RightLegMissing { get { return _rightLegMissing; } set { _rightLegMissing = value; } } 

    [SerializeField] private GameObject _leftArmPrefab;
    [SerializeField] private GameObject _rightArmPrefab;
    [SerializeField] private GameObject _leftLegPrefab;
    [SerializeField] private GameObject _rightLegPrefab;
    
 

    public GameObject LeftArmPrefab {get { return _leftArmPrefab; } set { _leftArmPrefab = value; }}

    public GameObject RightArmPrefab {get { return _rightArmPrefab; } set { _rightArmPrefab = value; }}

    public GameObject LeftLegPrefab {get { return _leftLegPrefab; } set { _leftLegPrefab = value; }}

    public GameObject RightLegPrefab {get { return _rightLegPrefab; } set { _rightLegPrefab = value; }}

    [SerializeField] private GameObject _leftArmMainStatuePrefab;
    [SerializeField] private GameObject _rightArmMainStatuePrefab;
    [SerializeField] private GameObject _leftLegMainStatuePrefab;
    [SerializeField] private GameObject _rightLegMainStatuePrefab;
    public GameObject LeftArmMainStatuePrefab { get { return _leftArmMainStatuePrefab; } set { _leftArmMainStatuePrefab = value; } }

    public GameObject RightArmMainStatuePrefab { get { return _rightArmMainStatuePrefab; } set { _rightArmMainStatuePrefab = value; } }

    public GameObject LeftLegMainStatuePrefab { get { return _leftLegMainStatuePrefab; } set { _leftLegMainStatuePrefab = value; } }

    public GameObject RightLegMainStatuePrefab { get { return _rightLegMainStatuePrefab; } set { _rightLegMainStatuePrefab = value; } }

    public GameObject LeftArm { get; set; }
    public GameObject RightArm { get; set; }
    public GameObject LeftLeg { get; set; }
    public GameObject RightLeg { get; set; }

    [SerializeField] private bool _leftArmIsFromMainStatue = false;
    [SerializeField] private bool _rightArmIsFromMainStatue = false;
    [SerializeField] private bool _leftLegIsFromMainStatue = false;
    [SerializeField] private bool _rightLegIsFromMainStatue = false;
    public bool LeftArmIsFromMainStatue { get { return _leftArmIsFromMainStatue; } set { _leftArmIsFromMainStatue = value; } }
    public bool RightArmIsFromMainStatue { get { return _rightArmIsFromMainStatue; } set { _rightArmIsFromMainStatue = value; } }
    public bool LeftLegIsFromMainStatue { get { return _leftLegIsFromMainStatue; } set { _leftLegIsFromMainStatue = value; } }
    public bool RightLegIsFromMainStatue { get { return _rightLegIsFromMainStatue; } set { _rightLegIsFromMainStatue = value; } }

    [SerializeField] private Transform _playerPosAfterSwap;
    public Transform PlayerPosAfterSwap { get { return _playerPosAfterSwap; } set { _playerPosAfterSwap = value; } }


    private void Awake()
    {
        if (!_leftArmMissing)
        {
            if (_leftArmIsFromMainStatue)
            {
                LeftArm = Object.Instantiate<GameObject>(LeftArmMainStatuePrefab, _leftArmSocket);
            }
            else
            {
                LeftArm = Object.Instantiate<GameObject>(LeftArmPrefab, _leftArmSocket);
            }
        }
        if (!_rightArmMissing)
        {
            if (_rightArmIsFromMainStatue)
            {
                RightArm = Object.Instantiate<GameObject>(RightArmMainStatuePrefab, _rightArmSocket);
            }
            else
            {
                RightArm = Object.Instantiate<GameObject>(RightArmPrefab, _rightArmSocket);

            }
        }
        if (!_leftLegMissing)
        {
            if (_leftLegIsFromMainStatue)
            {
                LeftLeg = Object.Instantiate<GameObject>(LeftLegMainStatuePrefab, _leftLegSocket);
            }
            else
            {
                LeftLeg = Object.Instantiate<GameObject>(LeftLegPrefab, _leftLegSocket);
            }
        }    
            
        if (!_rightLegMissing)
        {
            if (_rightLegIsFromMainStatue)
            {
                RightLeg = Object.Instantiate<GameObject>(RightLegMainStatuePrefab, _rightLegSocket);
            }
            else
            {
                RightLeg = Object.Instantiate<GameObject>(RightLegPrefab, _rightLegSocket);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //If main statue
        if(_isMainStatue)
        {
            if (_leftArmMissing)
                return;
            if (!_leftArmIsFromMainStatue)
                return;

            if (_rightArmMissing)
                return;
                if(!_rightArmIsFromMainStatue)
                return;

            if (_leftLegMissing)
                return;
            if (!_leftLegIsFromMainStatue)
                return;

            if (_rightLegMissing)
                return;
            if (!RightLegIsFromMainStatue)
                return;

            Debug.Log("Winner!!");

           Application.Quit();
           
        }
        //check if limb is missing
        //Check if that limb is from main statue


    }
}
