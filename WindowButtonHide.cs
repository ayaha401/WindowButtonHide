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
    /// �E�B���h�E�̍ŏ����A�ő剻�A����{�^�����B��
    /// </summary>
    /// <param name="isHide">�B�����ǂ���</param>
    public void SetWindowButtonHide(bool isHide)
    {
        IntPtr windowHandle = GetActiveWindow();
        int style = GetWindowLong(windowHandle, GWL_STYLE);

        if (isHide)
        {
            style &= ~WS_MINIMIZEBOX;  // �ŏ����{�^���𖳌���
            style &= ~WS_MAXIMIZEBOX;  // �ő剻�{�^���𖳌���
            style &= ~WS_SYSMENU;      // �V�X�e�����j���[�i����{�^���j�𖳌���
        }
        else
        {
            style |= WS_MINIMIZEBOX;  // �ŏ����{�^����L����
            style |= WS_MAXIMIZEBOX;  // �ő剻�{�^����L����
            style |= WS_SYSMENU;      // �V�X�e�����j���[�i����{�^���j��L����
        }

        SetWindowLong(windowHandle, GWL_STYLE, style);
    }
}