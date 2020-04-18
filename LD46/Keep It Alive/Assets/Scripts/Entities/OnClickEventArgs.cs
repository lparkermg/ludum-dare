using LPSoft.LD46.Enums;
using System;

namespace LPSoft.LD46.Entities
{
    public class OnClickEventArgs : EventArgs
    {
        public MouseButton Button { get; set; }
    }
}