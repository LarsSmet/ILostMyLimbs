using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using LimbInfo;
using UnityEngine.UI;

public class LimbBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolderTransform;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _statueMask;

    private PlayerControls _PlayerControls;

    public bool IsLimb { get; set; } = true;

    private Statue _currentStatue;

    [SerializeField] private CharacterController _controller;

    //TODO: Remove this later when finished game because you start as a statue
    [SerializeField]
    private LimbDetails _currLimbDetails;

    public LimbDetails CurrLimbDetails { get { return _currLimbDetails; } set { _currLimbDetails = value; }}


    Text _leftLegUIText;
    Text _rightLegUIText;
    Text _leftArmUIText;
    Text _rightArmUIText;

    private GameObject _visual;
 

    [SerializeField] private Transform _limbVisualHolder;


    // Start is called before the first frame update
    void Start()
    {
        _PlayerControls = GetComponent<PlayerControls>();
        _leftLegUIText = GameObject.Find("LeftLegUIText").GetComponent<Text>();

        _PlayerControls = GetComponent<PlayerControls>();
        _rightLegUIText = GameObject.Find("RightLegUIText").GetComponent<Text>();

        _PlayerControls = GetComponent<PlayerControls>();
        _leftArmUIText = GameObject.Find("LeftArmUIText").GetComponent<Text>();

        _PlayerControls = GetComponent<PlayerControls>();
        _rightArmUIText = GameObject.Find("RightArmUIText").GetComponent<Text>();

       // _visual = Object.Instantiate<GameObject>(_currentStatue.LeftLegMainStatuePrefab, _limbVisualHolder);

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLimb)
            return;

        //raycast
        Ray r = new Ray(_cameraHolderTransform.position, _cameraHolderTransform.transform.forward);
        RaycastHit hit;
       

        if (Physics.Raycast(r, out hit, 10, _statueMask))
        {
            Debug.Log("HIT STATUE");
            _currentStatue = hit.collider.gameObject.GetComponentInParent<Statue>();

            //Can pres e while looking at statue and statue misses the limb that the player currently is


            switch (CurrLimbDetails.Limb) //check if the limb on the statue is occupied
            {
                case LimbType.LeftLeg:
                    if (!_currentStatue.LeftLegMissing)
                        //TODO: Add visual with something like: The LIMB on this statue is occupied
                        return;
                    break;
                case LimbType.RightLeg:
                    if (!_currentStatue.RightLegMissing)
                        return;
                    break;
                case LimbType.LeftArm:
                    if (!_currentStatue.LeftArmMissing)
                        return;
                    break;
                case LimbType.RightArm:
                    if (!_currentStatue.RightArmMissing)
                        return;
                    break;
                  
            }

            _PlayerControls.CanBecomeStatue = true;

   
        }
        else
        {
            _PlayerControls.CanBecomeStatue = false;
        }

  

        //Set camera -> change to statue movement in controls

    }

    public void BecomeStatue()
    {
 
        _PlayerControls.CanBecomeStatue = false;
        _PlayerControls.CurrStatue = _currentStatue;

        _controller.enabled = false;
        Vector3 newPlayerPos = new Vector3(_currentStatue.PlayerPosAfterSwap.position.x, 1.5f, _currentStatue.PlayerPosAfterSwap.position.z);
        transform.position = newPlayerPos;
        transform.rotation = _currentStatue.PlayerPosAfterSwap.rotation;
        _controller.enabled = true;

        //set the camera to the statue pos and transform
        _controller.enabled = false;
        //_cameraHolderTransform.position = statue.CameraTransform.position;
        //_cameraHolderTransform.rotation = statue.CameraTransform.rotation;
        _camera.transform.position = _currentStatue.CameraSocket.position;
        _camera.transform.rotation = _currentStatue.CameraSocket.rotation;
        _controller.enabled = true;

       

        IsLimb = false;

        if(CurrLimbDetails.IsFromMainStatue)
        {
            switch (CurrLimbDetails.Limb)
            {
                case LimbType.LeftLeg:

                    _currentStatue.LeftLegMissing = false;
                    _currentStatue.LeftLegIsFromMainStatue = true;
                    _currentStatue.LeftLeg = Object.Instantiate<GameObject>(_currentStatue.LeftLegMainStatuePrefab, _currentStatue.LeftLegSocket);
                    break;
                case LimbType.RightLeg:

                    _currentStatue.RightLegMissing = false;
                    _currentStatue.RightLegIsFromMainStatue = true;
                    _currentStatue.RightLeg = Object.Instantiate<GameObject>(_currentStatue.RightLegMainStatuePrefab, _currentStatue.RightLegSocket);
                    break;
                case LimbType.LeftArm:

                    _currentStatue.LeftArmMissing = false;
                    _currentStatue.LeftArmIsFromMainStatue = true;
                    _currentStatue.LeftArm = Object.Instantiate<GameObject>(_currentStatue.LeftArmMainStatuePrefab, _currentStatue.LeftArmSocket);
                    break;
                case LimbType.RightArm:

                    _currentStatue.RightArmMissing = false;
                    _currentStatue.RightArmIsFromMainStatue = true;
                    _currentStatue.RightArm = Object.Instantiate<GameObject>(_currentStatue.RightArmMainStatuePrefab, _currentStatue.RightArmSocket);
                    break;

            }
        }
        else
        {
            switch (CurrLimbDetails.Limb)
            {
                case LimbType.LeftLeg:

                    _currentStatue.LeftLegMissing = false;
                    _currentStatue.LeftLegIsFromMainStatue = false;
                    _currentStatue.LeftLeg = Object.Instantiate<GameObject>(_currentStatue.LeftLegPrefab, _currentStatue.LeftLegSocket);
                    break;
                case LimbType.RightLeg:

                    _currentStatue.RightLegMissing = false;
                    _currentStatue.RightLegIsFromMainStatue = false;
                    _currentStatue.RightLeg = Object.Instantiate<GameObject>(_currentStatue.RightLegPrefab, _currentStatue.RightLegSocket);
                    break;
                case LimbType.LeftArm:

                    _currentStatue.LeftArmMissing = false;
                    _currentStatue.LeftArmIsFromMainStatue = false;
                    _currentStatue.LeftArm = Object.Instantiate<GameObject>(_currentStatue.LeftArmPrefab, _currentStatue.LeftArmSocket);
                    break;
                case LimbType.RightArm:

                    _currentStatue.RightArmMissing = false;
                    _currentStatue.RightArmIsFromMainStatue = false;
                    _currentStatue.RightArm = Object.Instantiate<GameObject>(_currentStatue.RightArmPrefab, _currentStatue.RightArmSocket);
                    break;

            }
        }

        //do the UI stuff
        UpdateUI(_currentStatue.LeftArmMissing, _leftArmUIText, "Left arm missing", "Left trigger to become the left arm");
        UpdateUI(_currentStatue.RightArmMissing, _rightArmUIText, "Right arm missing", "Right trigger to become the right arm");
        UpdateUI(_currentStatue.LeftLegMissing, _leftLegUIText, "Left leg missing", "Left bumper to become the left leg");
        UpdateUI(_currentStatue.RightLegMissing, _rightLegUIText, "Right leg missing", "Right bumper to become the right leg");


        Destroy(_visual);
    }

    public void BecomeLimb(LimbDetails newLimbDetails) //TODO: Add the limbdetails struct and set all the correct stuff
    {

        CurrLimbDetails = newLimbDetails;

        switch (CurrLimbDetails.Limb) 
        {
            case LimbType.LeftLeg:
                // if (_currentStatue.LeftLegMissing)
                _currentStatue.LeftLegMissing = true;
                _currentStatue.LeftLegIsFromMainStatue = false;

                if (CurrLimbDetails.IsFromMainStatue)
                    _visual = Object.Instantiate<GameObject>(_currentStatue.LeftLegMainStatuePrefab, _limbVisualHolder);
                else
                    _visual = Object.Instantiate<GameObject>(_currentStatue.LeftLegPrefab, _limbVisualHolder);

                Destroy(_currentStatue.LeftLeg);
                break;
            case LimbType.RightLeg:
                // if (_currentStatue.RightLegMissing)
                _currentStatue.RightLegMissing = true;
                _currentStatue.RightLegIsFromMainStatue = false;

                if(CurrLimbDetails.IsFromMainStatue)
                    _visual = Object.Instantiate<GameObject>(_currentStatue.RightLegMainStatuePrefab, _limbVisualHolder);
                else
                    _visual = Object.Instantiate<GameObject>(_currentStatue.RightLegPrefab, _limbVisualHolder);

                Destroy(_currentStatue.RightLeg);
                break;
            case LimbType.LeftArm:
                // if (_currentStatue.LeftArmMissing)
                _currentStatue.LeftArmMissing = true;
                _currentStatue.LeftArmIsFromMainStatue = false;

                if (CurrLimbDetails.IsFromMainStatue)
                    _visual = Object.Instantiate<GameObject>(_currentStatue.LeftArmMainStatuePrefab, _limbVisualHolder);
                else
                    _visual = Object.Instantiate<GameObject>(_currentStatue.LeftArmPrefab, _limbVisualHolder);

                Destroy(_currentStatue.LeftArm);
                break;
            case LimbType.RightArm:
                // if (_currentStatue.RightArmMissing)
                _currentStatue.RightArmMissing = true;
                _currentStatue.RightArmIsFromMainStatue = false;

                if (CurrLimbDetails.IsFromMainStatue)
                    _visual = Object.Instantiate<GameObject>(_currentStatue.RightArmMainStatuePrefab, _limbVisualHolder);
                else
                    _visual = Object.Instantiate<GameObject>(_currentStatue.RightArmPrefab, _limbVisualHolder);

                Destroy(_currentStatue.RightArm);
                break;

        }


        _controller.enabled = false;
        _camera.transform.position = _cameraHolderTransform.position;
        _camera.transform.rotation = _cameraHolderTransform.rotation;
        _controller.enabled = true;

        IsLimb = true;

        _leftArmUIText.text = "";
        _rightArmUIText.text = "";
        _leftLegUIText.text = "";
        _rightLegUIText.text = "";

        //Potentially add visual

        

    }


    private void UpdateUI(bool isMissing, Text UIText, string missingText, string normalText)
    {
        if (isMissing)
            UIText.text = missingText;
        else
            UIText.text = normalText;
    }

}
