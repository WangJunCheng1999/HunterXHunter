using System;
using System.Collections.Generic;

namespace Core.TimerTask{
internal class TimeFragment
{
    private int _interval;
    private readonly Dictionary<long, Action> _timeStmaps = new Dictionary<long, Action>();

    public TimeFragment(int interval)
    {
        _interval = interval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeStamp"></param>
    public void Add(long timeStamp)
    {
        _timeStmaps.Add(timeStamp, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeStamp"></param>
    public void Remove(long timeStamp)
    {
        _timeStmaps.Remove(timeStamp);
    }

    public void Check(long timeStamp)
    {
        foreach (KeyValuePair<long, Action> pair in _timeStmaps)
        {
            if (pair.Key < timeStamp)
            {
                continue;
            }

            pair.Value?.Invoke();
        }
    }
}
    
}