using System.Collections;
using System.Collections.Generic;

public static class MerchantUtilities
{
    public static string TimeFormat(float seconds) {
        int _minutes = (int) seconds / 60;
        int _seconds = (int) seconds - _minutes * 60;
        string minutesString, secondsString;
        if(_minutes < 10) {
            minutesString = "0"+_minutes;
        } else {
            minutesString = ""+_minutes;
        }

        if(_seconds < 10) {
            secondsString = "0"+_seconds;
        } else {
            secondsString = ""+_seconds;
        }
        return (minutesString + ":" + secondsString);
    }

    public static void MergeDictionaries<T>(this Dictionary<T, int> source, Dictionary<T, int> graft) {
        if(graft != null) {
            foreach(KeyValuePair<T, int> item in graft) {
                if(source.ContainsKey(item.Key)) {
                    source[item.Key] = source[item.Key] + item.Value; 
                } else {
                    source.Add(item.Key, item.Value);
                }
            }
        }
    }
}
