using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace AutoCommitMessage;

/// <summary>
/// Modern folder picker built directly on the Win32 shell <c>IFileOpenDialog</c> (the Vista-style
/// "Select Folder" dialog). Using the shell COM API instead of WinForms' <c>FolderBrowserDialog</c>
/// means no dependency on the .NET Windows Desktop runtime, so the standalone browser app can be a
/// plain framework-dependent ASP.NET Core executable.
///
/// Must be called from an STA thread.
/// </summary>
[SupportedOSPlatform("windows")]
internal static class ShellFolderPicker
{
    /// <summary>
    /// Shows the folder picker and returns the selected path, or <c>null</c> if the user cancels or
    /// the dialog cannot be shown.
    /// </summary>
    public static string? PickFolder(string title)
    {
        IFileOpenDialog? dialog = null;
        try
        {
            var dialogType = Type.GetTypeFromCLSID(CLSID_FileOpenDialog);
            if (dialogType is null)
            {
                return null;
            }

            dialog = (IFileOpenDialog)Activator.CreateInstance(dialogType)!;

            dialog.GetOptions(out var options);
            dialog.SetOptions(options | FOS_PICKFOLDERS | FOS_FORCEFILESYSTEM | FOS_NOCHANGEDIR);
            if (!string.IsNullOrWhiteSpace(title))
            {
                dialog.SetTitle(title);
            }

            // Owning the dialog to the host process's main window keeps it in front of Studio Pro.
            // The standalone console app has no main window (handle is zero), which is fine.
            var owner = Process.GetCurrentProcess().MainWindowHandle;

            var hr = dialog.Show(owner);
            if (hr != 0)
            {
                // ERROR_CANCELLED (0x800704C7) or any other failure — treat as "no selection".
                return null;
            }

            dialog.GetResult(out var item);
            try
            {
                item.GetDisplayName(SIGDN_FILESYSPATH, out var path);
                return string.IsNullOrWhiteSpace(path) ? null : path;
            }
            finally
            {
                Marshal.ReleaseComObject(item);
            }
        }
        catch
        {
            // Shell API unavailable or failed — caller falls back to manual path entry.
            return null;
        }
        finally
        {
            if (dialog is not null)
            {
                Marshal.ReleaseComObject(dialog);
            }
        }
    }

    private const uint FOS_PICKFOLDERS = 0x00000020;
    private const uint FOS_FORCEFILESYSTEM = 0x00000040;
    private const uint FOS_NOCHANGEDIR = 0x00000008;
    private const uint SIGDN_FILESYSPATH = 0x80058000;

    private static readonly Guid CLSID_FileOpenDialog = new("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7");

    [ComImport]
    [Guid("42F85136-DB7E-439C-85F1-E4075D135FC8")] // IID_IFileOpenDialog
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFileOpenDialog
    {
        // IModalWindow
        [PreserveSig]
        int Show(IntPtr parent);

        // IFileDialog (only the members we need; order must match the vtable exactly)
        void SetFileTypes();          // placeholder — not called
        void SetFileTypeIndex(uint iFileType);
        void GetFileTypeIndex(out uint piFileType);
        void Advise();                // placeholder — not called
        void Unadvise();              // placeholder — not called
        void SetOptions(uint fos);
        void GetOptions(out uint pfos);
        void SetDefaultFolder(IShellItem psi);
        void SetFolder(IShellItem psi);
        void GetFolder(out IShellItem ppsi);
        void GetCurrentSelection(out IShellItem ppsi);
        void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);
        void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
        void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);
        void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        void GetResult(out IShellItem ppsi);
        void AddPlace(IShellItem psi, int fdap);
        void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
        void Close(int hr);
        void SetClientGuid(ref Guid guid);
        void ClearClientData();
        void SetFilter(IntPtr pFilter);
    }

    [ComImport]
    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")] // IID_IShellItem
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IShellItem
    {
        void BindToHandler(IntPtr pbc, ref Guid bhid, ref Guid riid, out IntPtr ppv);
        void GetParent(out IShellItem ppsi);
        void GetDisplayName(uint sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);
        void Compare(IShellItem psi, uint hint, out int piOrder);
    }
}
