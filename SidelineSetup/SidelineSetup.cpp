#include "stdafx.h"
#include <windows.h>
#include <ce_setup.h>
#include <regext.h>

codeINSTALL_INIT Install_Init(
    HWND        hwndParent,
    BOOL        fFirstCall, 
    BOOL        fPreviouslyInstalled,
    LPCTSTR     pszInstallDir
    )
{
	// Create necessary reg keys here
    return codeINSTALL_INIT_CONTINUE;
}

codeINSTALL_EXIT Install_Exit(
    HWND    hwndParent,
    LPCTSTR pszInstallDir,
    WORD    cFailedDirs,
    WORD    cFailedFiles,
    WORD    cFailedRegKeys,
    WORD    cFailedRegVals,
    WORD    cFailedShortcuts
    )
{
    return codeINSTALL_EXIT_DONE;
}



codeUNINSTALL_INIT Uninstall_Init(
    HWND        hwndParent,
    LPCTSTR     pszInstallDir
    )
{
    // Kill process here
    return codeUNINSTALL_INIT_CONTINUE;
}



codeUNINSTALL_EXIT Uninstall_Exit(
    HWND    hwndParent
    )
{
    return codeUNINSTALL_EXIT_DONE;
}

