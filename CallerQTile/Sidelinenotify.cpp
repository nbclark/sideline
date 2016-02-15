// CallerQTile.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <notify.h>
#include <windows.h>
#include <windowsx.h>
#include <pm.h>
#include <aygshell.h>
#include <shellapi.h>
#include <regext.h>
#include <commctrl.h>   // includes the common control header
#include "resource.h"

BOOL LaunchProgram(LPCTSTR lpFile, LPCTSTR lpParameters)
{
    SHELLEXECUTEINFO shInfo;

    shInfo.cbSize = sizeof(SHELLEXECUTEINFO);
    shInfo.dwHotKey = 0;
    shInfo.fMask = 0;
    shInfo.hIcon = NULL;
    shInfo.hInstApp = NULL;
    shInfo.hProcess = NULL;
    shInfo.lpDirectory = NULL;
    shInfo.lpIDList = NULL;
    shInfo.lpParameters = lpParameters;
    shInfo.lpVerb = NULL;
    shInfo.nShow = SW_SHOW;
    shInfo.lpFile = lpFile;

    return ShellExecuteEx(&shInfo);
}

int _tmain(int argc, _TCHAR* argv[])
{
	SetSystemPowerState(NULL, POWER_STATE_ON, POWER_FORCE);
	HWND hwndSideline = FindWindow(NULL, L"sidelineMsg");

	if (hwndSideline)
	{
		PostMessageW(hwndSideline, WM_USER+2, 0, 0);
	}
	else
	{
		LaunchProgram(L"\\program files\\mobilesrc\\sideline\\sideline.exe", L"-notify 0");
	}
	return 0;
}

