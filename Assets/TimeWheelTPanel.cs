using System;
using System.Collections;
using System.Collections.Generic;
using Framework.TimerTask;
using Framework.TimerTask.Interface;
using GameFramework;
using GameFramework.Helper;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimeWheelTPanel : MonoBehaviour
{
    private ITimer _timer =
        TimingWheelTimer.Build(TimeSpan.FromMilliseconds(10), 10, new DateTimeHelper().GetTimestamp());

    private long time;

    private DateTimeHelper helper = new DateTimeHelper();
    // Start is called before the first frame update
    [SerializeField] public Text TextTimeStamp;
    [SerializeField] public InputField Input;

    void Start()
    {
    }

    public void OnAddButtonClicked()
    {
        time = helper.GetTimestamp();
        _timer.AddTask(time+ int.Parse(Input.text), Trigger);
    }

    public void Trigger()
    {
        Game.Logger.Log($"{helper.GetTimestamp() - time}");
    }

    // Update is called once per frame
    void Update()
    {
        _timer.Step(helper.GetTimestamp());
    }
}