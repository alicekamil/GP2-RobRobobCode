using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        LeanTween.cancelAll();
        _ship.GetComponent<Animator>().SetTrigger("PlayPressed");
        //Game starts from script on _ship "PlayPressedStartGame" by animation event on "shipintrofadetoblack"
    }

    public void CreditsClicked()
    {
        _creditsIsClicked = true;
        _camSwitch = 3;
    }

    public void CreditsBack()
    {
        _creditsIsClicked = false;
        _camSwitch = 1;
    }

    public void GuideClicked()
    {
        _guideIsClicked = true;
        _camSwitch = 2;
    }

    public void GuideBack()
    {
        _guideIsClicked = false;
        _camSwitch = 1;
    }

    private void OriginAnim()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _camOrigin.transform.position,
            _camSpeed / 2 * Time.deltaTime);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _camOrigin.transform.rotation,
            _camRotateSpeed / 2 * Time.deltaTime);
        _camera.transform.SetParent(null);
    }

    private void GuideAnim()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _guideCamTarget.transform.position,
            _camSpeed * Time.deltaTime);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _guideCamTarget.transform.rotation,
            _camRotateSpeed * Time.deltaTime);
    }

    private void CreditsAnim()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _creditCamTarget.transform.position,
            _camSpeed * Time.deltaTime);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _creditCamTarget.transform.rotation,
            _camRotateSpeed * Time.deltaTime);
        _camera.transform.SetParent(_creditCamTarget.transform);
    }

    private void CamSwitch()
    {
        switch (_camSwitch)
        {
            case 1:
                OriginAnim();
                break;
            case 2:
                GuideAnim();
                break;
            case 3:
                CreditsAnim();
                break;
            default:
                OriginAnim();
                break;
        }
    }

    private void Update()
    {
        CamSwitch();
    }

    private void Start()
    {
        Time.timeScale = 1;
        _ship.GetComponent<Animator>().Play("shipintroanim");
    }

    [SerializeField] private GameObject _ship;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _creditCamTarget;
    [SerializeField] private GameObject _guideCamTarget;
    [SerializeField] private GameObject _camOrigin;
    [SerializeField] private float _camSpeed = 15f;
    [SerializeField] private float _camRotateSpeed = 5f;

    private bool _creditsIsClicked;
    private bool _guideIsClicked;

    private int _camSwitch;
}