using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class DeplacerSousMarin : MonoBehaviour
{

    [SerializeField] private float _vitessePromenade = 5f;
    private Rigidbody _rb;
    private Vector3 directionInput;

    [SerializeField] private float _modifierAnimTranslation = 1.25f;
    private Animator _animator;

    private float _avancerReculerInput = 0f;
    private float _monterDescendreInput = 0f;

    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

   

    public void OnAvancerReculer(InputAction.CallbackContext context)
    {
        _avancerReculerInput = context.ReadValue<float>();
        float inputValue = context.ReadValue<float>();  
        directionInput.z = inputValue * _vitessePromenade;

    }

    public void OnMonterDescendre(InputAction.CallbackContext context)
    {
        _monterDescendreInput = context.ReadValue<float>();  
        float inputValue = context.ReadValue<float>();  
        directionInput.y = inputValue * _vitessePromenade;

    }

    
    public void OnAccelerer(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            _vitessePromenade *= 2;
        }
        else if (context.canceled)
        {
            _vitessePromenade /= 2;
        }

    }


    void FixedUpdate()
        {

            directionInput = new Vector3(0f, _monterDescendreInput * _vitessePromenade, _avancerReculerInput * _vitessePromenade);
        
            _rb.AddForce(directionInput, ForceMode.VelocityChange);

            
            Vector2 hautBas = new Vector2(0f, _rb.velocity.y);
            Vector2 avantArriere = new Vector2(_rb.velocity.z, 0f);
            
            _animator.SetFloat("VitesseAA", avantArriere.magnitude * _modifierAnimTranslation);
            _animator.SetFloat("VitesseHB", hautBas.magnitude * _modifierAnimTranslation);
           
            _animator.SetFloat("MouvementAA", avantArriere.magnitude);
            _animator.SetFloat("MouvementHB", hautBas.magnitude);
        }
}    



