using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float _radius;
    public float _angle;

    public GameObject _player;

    public LayerMask _playerMask;
    public LayerMask _obstructionMask;

    public bool _canSeePlayer;

    LimbBehaviour _limbBehaviourPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _limbBehaviourPlayer = _player.GetComponent<LimbBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        if (!_limbBehaviourPlayer.IsLimb)
            return;


        Collider[] hitCheck = Physics.OverlapSphere(transform.position, _radius, _playerMask);
        //Check if player is in range
        if (hitCheck.Length != 0)
        {
            Transform target = hitCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2) //Check if player is in fov angle
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask)) //Check if something obstructs the vision
                {
                    _canSeePlayer = true;
                    Application.Quit();
                }
                else
                {
                    _canSeePlayer = false;
                }
            }
            else
            {
                _canSeePlayer = false;
            }
        }
        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }
}
