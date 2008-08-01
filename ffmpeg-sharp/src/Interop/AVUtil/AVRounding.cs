using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpegSharp.Interop.AVUtil
{
    public enum AVRounding
    {
        ///<summary>round toward zero</summary> 
        AV_ROUND_ZERO = 0,
        ///<summary>round away from zero</summary>
        AV_ROUND_INF = 1,
        ///<summary>round toward -infinity</summary>
        AV_ROUND_DOWN = 2,
        ///<summary>round toward +infinity</summary>
        AV_ROUND_UP = 3,
        ///<summary>round to nearest and halfway cases away from zero</summary>
        AV_ROUND_NEAR_INF = 5, 
    }
}
