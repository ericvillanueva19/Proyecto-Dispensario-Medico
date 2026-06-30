using System.Runtime.InteropServices;

namespace DispensarioMedico.UI
{
    /// <summary>
    /// Interop Win32 para permitir arrastrar la ventana sin barra de título.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(System.IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
