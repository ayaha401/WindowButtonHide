using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class WindowButtonHide : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GWL_STYLE = -16;
    private const int WS_MINIMIZEBOX = 0x00020000;
    private const int WS_MAXIMIZEBOX = 0x00010000;
    private const int WS_SYSMENU = 0x00080000;

    /// <summary>
    /// ウィンドウの最小化、最大化、閉じるボタンを隠す
    /// </summary>
    /// <param name="isHide">隠すかどうか</param>
    public void SetWindowButtonHide(bool isHide)
    {
        IntPtr windowHandle = GetActiveWindow();
        int style = GetWindowLong(windowHandle, GWL_STYLE);

        if (isHide)
        {
            style &= ~WS_MINIMIZEBOX;  // 最小化ボタンを無効化
            style &= ~WS_MAXIMIZEBOX;  // 最大化ボタンを無効化
            style &= ~WS_SYSMENU;      // システムメニュー（閉じるボタン）を無効化
        }
        else
        {
            style |= WS_MINIMIZEBOX;  // 最小化ボタンを有効化
            style |= WS_MAXIMIZEBOX;  // 最大化ボタンを有効化
            style |= WS_SYSMENU;      // システムメニュー（閉じるボタン）を有効化
        }

        SetWindowLong(windowHandle, GWL_STYLE, style);
    }
}