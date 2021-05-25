Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

Public Class RTBPrint                       ' Print-enabled RTB extension, taken from Martin Mueller's 1/18/2008 Microsoft article

    ' Purpose:      Adds methods to the standard Rich Text Box that support WYSIWYG printing.
    ' Process:      Use .NET Interop mechanism to call to call underlying WinForm capability that supports
    '               fully-formatted printing.
    ' Called By:    NA
    ' Side Effects: <None>
    ' Notes:        Follows code from Mueller's article.
    ' Updates:      <1.060.3> First implemented

    Inherits RichTextBox                    ' Extends existing RTB control, so all RTB features carry forward

    <StructLayout(LayoutKind.Sequential)>   ' Forces structure to layout in memory exactly as defined
    Private Structure str_Rect              ' Defines a Win32 API rectangle
        Public left As Int32
        Public top As Int32
        Public right As Int32
        Public bottom As Int32
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure str_CharRange         ' Defines Win32 API print-ranges, starting and ending character positions
        Public cpMin As Int32
        Public cpMax As Int32
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure str_FormatRange
        Public hdc As IntPtr
        Public hdcTarget As IntPtr
        Public rc As str_Rect               ' Page margins
        Public rcPage As str_Rect           ' Page size
        Public chrg As str_CharRange        ' Print range
    End Structure

    <DllImport("user32.dll")>               ' Send message routine
    Private Shared Function SendMessage(hwnd As IntPtr,
                                        msg As Int32,
                                        wParam As Int32,
                                        lParam As IntPtr
                                        ) As Int32
    End Function

    Private Const WM_User As Int32 = &H400&
    Private Const EM_FormatRange As Int32 = WM_User + 57

    Public Function FormatRange(                            ' Invoke Win32 print formatter
           measureOnly As Boolean,                          ' True -> calculate only; False -> render text
           e As PrintPageEventArgs,                         ' PrintPage obejct
           charFrom As Integer,                             ' First char to print
           charTo As Integer                                ' Last character to print
           ) As Integer                                     ' Index of last char that made it onto current page, +1: 1st char to print on next page

        Dim cr As str_CharRange                             ' Specify which characters to print
        cr.cpMin = charFrom
        cr.cpMax = charTo

        Dim rc As str_Rect                                  ' Specify the area inside page margins
        rc.top = HundredthInchToTwips(e.MarginBounds.Top)
        rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom)
        rc.left = HundredthInchToTwips(e.MarginBounds.Left)
        rc.right = HundredthInchToTwips(e.MarginBounds.Right)

        Dim rcPage As str_Rect                              ' Specify the page size
        rcPage.top = HundredthInchToTwips(e.PageBounds.Top)
        rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom)
        rcPage.left = HundredthInchToTwips(e.PageBounds.Left)
        rcPage.right = HundredthInchToTwips(e.PageBounds.Right)

        Dim hdc As IntPtr                                   ' Get device context of output device
        hdc = e.Graphics.GetHdc()

        Dim fr As str_FormatRange                           ' Fill in the FormatRange structure
        fr.chrg = cr
        fr.hdc = hdc
        fr.hdcTarget = hdc
        fr.rc = rc
        fr.rcPage = rcPage

        Dim wParam As Int32                                 ' Non-Zero wParam means render, Zero means measure
        If measureOnly Then
            wParam = 0
        Else
            wParam = 1
        End If

        Dim lParam As IntPtr                                ' Allocate memory for the FormatRange struct and
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr)) ' copy the contents of our struct to this memory
        Marshal.StructureToPtr(fr, lParam, False)

        Dim res As Integer                                  ' Send the actual Win32 message
        res = SendMessage(Handle,                           ' Handle to the extended RTB control instance that is being printed
                          EM_FormatRange,                   ' Control block
                          wParam,                           ' Calc or Render
                          lParam)

        Marshal.FreeCoTaskMem(lParam)                       ' Free allocated memory
        e.Graphics.ReleaseHdc(hdc)                          ' and release the device context

        Return res                                          ' Index to first char to print on next page
    End Function

    Private Function HundredthInchToTwips(n As Integer) As Int32

        ' Purpose:      Convert points (used in .Net) to Twips (used in Win32 API)
        ' Process:      Simple math
        ' Called By:    FormatRange()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented

        Return Convert.ToInt32(n * 14.4)                    ' Conversion factor

    End Function

    Public Sub FormatRangeDone()

        ' Purpose:      Release graphics memory, used when print rendering is finished
        ' Process:      Use .NET Interop mechanism to message to formatter
        ' Called By:    EndPrint()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented

        Dim lParam As New IntPtr(0)                         ' Null pointer will cause memory to be released
        SendMessage(Handle, EM_FormatRange, 0, lParam)      ' Send the message. Handle is that of the extended RTB control instance

    End Sub

End Class
