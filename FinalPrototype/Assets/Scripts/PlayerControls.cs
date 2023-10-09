using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using LimbInfo;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    private PlayerInput _playerInput;

    public PlayerInputActions _playerInputActions;

    [SerializeField] private float _moveSpeed = 15.0f;

    [SerializeField] private float sensitivity = 0.1f;

    private float _lookRotation;
    [SerializeField] private GameObject _camHolder;

    public bool CanBecomeStatue { get; set; } = false;

    [SerializeField] private LimbBehaviour _limbBehaviour;

    public Statue CurrStatue { get; set; }




    private void Awake()
    {
        
        _playerInputActions = new PlayerInputActions();

        //Limb
        _playerInputActions.Limb.Enable();
         //_playerInputActions.Limb.Jump.performed += Jump;
        _playerInputActions.Limb.BecomeStatue.performed += BecomeStatue;

        //Statue
        _playerInputActions.Statue.BecomeLeftArm.performed += BecomeLeftArm;
        _playerInputActions.Statue.BecomeRightArm.performed += BecomeRightArm;
        _playerInputActions.Statue.BecomeLeftLeg.performed += BecomeLeftLeg;
        _playerInputActions.Statue.BecomeRightLeg.performed += BecomeRightLeg;
    }

    

    private void FixedUpdate()
    {
        if (_playerInputActions.Limb.enabled)
        {

            Vector2 moveInputVec = _playerInputActions.Limb.Movement.ReadValue<Vector2>();

            Vector3 move = transform.right * moveInputVec.x + transform.forward * moveInputVec.y;



            _controller.Move(move * _moveSpeed * Time.fixedDeltaTime);

        }
    }

    

    private void LateUpdate()
    {
        if (_playerInputActions.Limb.enabled)
        {
            Vector2 lookInputVec = _playerInputActions.Limb.Look.ReadValue<Vector2>();

            //Horizontal
            transform.Rotate(Vector3.up * lookInputVec.x * sensitivity);

            //Vertical
            _lookRotation += (-lookInputVec.y * sensitivity);
            _lookRotation = Mathf.Clamp(_lookRotation, -90, 90);
            _camHolder.transform.eulerAngles = new Vector3(_lookRotation, _camHolder.transform.eulerAngles.y, _camHolder.transform.eulerAngles.z);

        }
        else if(_playerInputActions.Statue.enabled) 
        {
            //Vector2 lookInputVec = _playerInputActions.Statue.Look.ReadValue<Vector2>();

            ////Horizontal
            //transform.Rotate(Vector3.up * lookInputVec.x * sensitivity);

            //Maybe make player be able to look as statue in


        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump");
            _controller.Move(new Vector3(0, 2, 0));
        }
    }

    public void BecomeStatue(InputAction.CallbackContext context)
    {
        if(CanBecomeStatue)
        {
            


            Debug.Log("PLAYER IS now a  STATUE");
            _playerInputActions.Limb.Disable();
            _playerInputActions.Statue.Enable();
            _limbBehaviour.BecomeStatue();
        }
    }

    public void BecomeLeftArm(InputAction.CallbackContext context)
    {
       
        if (!CurrStatue.LeftArmMissing)
        {
            Debug.Log( "Became left arm");
            _playerInputActions.Statue.Disable();
            _playerInputActions.Limb.Enable();

            if (CurrStatue.LeftArmIsFromMainStatue)
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.LeftArm, true));
            else
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.LeftArm, false));

        }
    }

    public void BecomeRightArm(InputAction.CallbackContext context)
    {

        if (!CurrStatue.RightArmMissing)
        {
            Debug.Log("Became Right arm");
            _playerInputActions.Statue.Disable();
            _playerInputActions.Limb.Enable();

            if(CurrStatue.RightArmIsFromMainStatue)
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.RightArm, true));
            else
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.RightArm, false));
        }
    }

    public void BecomeLeftLeg(InputAction.CallbackContext context)
    {

        if (!CurrStatue.LeftLegMissing)
        {
            Debug.Log("Became left leg");
            _playerInputActions.Statue.Disable();
            _playerInputActions.Limb.Enable();

            if (CurrStatue.LeftLegIsFromMainStatue)
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.LeftLeg, true));
            else
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.LeftLeg, false));
        }
    }

    public void BecomeRightLeg(InputAction.CallbackContext context)
    {

        if (!CurrStatue.RightLegMissing)
        {
            Debug.Log("Became Right Leg");
            _playerInputActions.Statue.Disable();
            _playerInputActions.Limb.Enable();

            if (CurrStatue.RightLegIsFromMainStatue)
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.RightLeg, true));
            else
                _limbBehaviour.BecomeLimb(new LimbDetails(LimbType.RightLeg, false));

        }
    }


    //private void MovePlayer()
    //{
    //    Vector2 inputVec = _playerInputActions.Limb.Movement.ReadValue<Vector2>();

    //    _controller.Move(new Vector3(inputVec.x, 0, inputVec.y) * _moveSpeed * Time.fixedDeltaTime);
    //}

}
