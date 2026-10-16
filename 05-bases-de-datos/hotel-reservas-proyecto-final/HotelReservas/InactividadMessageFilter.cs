using System;
using System.Windows.Forms;

namespace HotelReservas
{
    /// <summary>
    /// Application-level message filter that intercepts mouse and keyboard messages
    /// to reset the inactivity timer in MainForm (F2.5).
    /// </summary>
    internal class InactividadMessageFilter : IMessageFilter
    {
        // WM_MOUSEMOVE, WM_LBUTTONDOWN, WM_RBUTTONDOWN, WM_KEYDOWN
        private const int WM_MOUSEMOVE    = 0x0200;
        private const int WM_LBUTTONDOWN  = 0x0201;
        private const int WM_RBUTTONDOWN  = 0x0204;
        private const int WM_MBUTTONDOWN  = 0x0207;
        private const int WM_KEYDOWN      = 0x0100;
        private const int WM_SYSKEYDOWN   = 0x0104;

        private readonly Action _resetCallback;

        public InactividadMessageFilter(Action resetCallback)
        {
            if (resetCallback == null) throw new ArgumentNullException("resetCallback");
            _resetCallback = resetCallback;
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEMOVE:
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MBUTTONDOWN:
                case WM_KEYDOWN:
                case WM_SYSKEYDOWN:
                    try { _resetCallback(); } catch { }
                    break;
            }

            return false; // Never consume the message
        }
    }
}
