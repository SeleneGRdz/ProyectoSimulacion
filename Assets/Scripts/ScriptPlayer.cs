using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _horizontalInput, _forwardInput;
    private Rigidbody _playerRB;

    [SerializeField]
    private int _maxJumps = 3;
    [SerializeField]
    private int _availableJumps;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private bool _jumpRequest = false;


    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        if(_playerRB==null){
            Debug.LogWarning("El jugador no tiene RigidBody");
        }
    }

    // Update is called once per frame
    void Update()
    {
       _horizontalInput = Input.GetAxis("Horizontal");
       _forwardInput = Input.GetAxis("Vertical");

       Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
       transform.Translate(movement * _speed * Time.deltaTime); 

       if(Input.GetKeyDown(KeyCode.Space) && _availableJumps>0){
            _jumpRequest = true;
       }
    }
    
    private void FixedUpdate(){
        if(_jumpRequest){
           _playerRB.velocity = Vector3.up * _jumpForce;
           _availableJumps--;
           _jumpRequest = false;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            _availableJumps = _maxJumps;
        }
    }
}
