//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//


#define INITGUID
#include "common.h"
#include <initguid.h>
#include <pimstore.h>
#include <mapiutil.h>
#undef INITGUID
#include <phone.h>
#include <notify.h>
#include <windows.h>
#include <windowsx.h>
#include <aygshell.h>
#include <shellapi.h>
#include <regext.h>
#include <commctrl.h>   // includes the common control header
#include "CallerQ.h"
#include "resource.h"

// Globals
// Global Count of references to this DLL - defined in Main.cpp
extern UINT g_cDLLRefCount;

#define SN_ACTIVEAPPLICATION_ROOT HKEY_CURRENT_USER
#define SN_ACTIVEAPPLICATION_PATH TEXT("System\\State\\Shell")
#define SN_ACTIVEAPPLICATION_VALUE TEXT("Active Application") 

HWND g_hwndSC = NULL;
HWND g_hwndList = NULL;
LONG g_lOldProc = NULL;
LONG g_lOldProcLB = NULL;

#define MAX_BUF MAX_NUMBER_LEN * 4


// This is the text to be used in the menu items the  menu extension
// will add.  In production these would likely be pulled from a resource
// file which could be localized

const TCHAR cszSidelineContact[] = TEXT("Sideline Contact");

// Registry paths for calling card settings
const TCHAR cszRegSettings[] = TEXT("Software\\Microsoft\\CallerQ");
const TCHAR cszRegNumber[] = TEXT("Number");
const TCHAR cszRegPause1[] = TEXT("Pause1");
const TCHAR cszRegPause2[] = TEXT("Pause2");
const TCHAR cszRegPIN[] = TEXT("Pin");

int g_menuCount = 0;
MenuExtension* g_menuExts[4];

HINSTANCE MenuExtension::g_hInstance = NULL;
CEOID MenuExtension::g_oid = 0;

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

void OnClick(MenuExtension* pMenuExtension)
{
	if (!pMenuExtension)
	{
		MessageBox(NULL, L"Will not Launch Sideline", L"Will not Launch Sideline", MB_OK);
	}
	else
	{
		CEOID oid = 0;

		for (int i = 0; i < g_menuCount; ++i)
		{
			if (TRUE == g_menuExts[i]->GetContactInfo(&oid))
			{
				break;
			}
		}

		if (oid)
		{
			HWND hwndSideline = FindWindow(NULL, L"sidelineMsg");

			if (hwndSideline)
			{
				PostMessageW(hwndSideline, WM_USER+1, 0, oid);
			}
			else
			{
				WCHAR wzArgs[50];
				StringCchPrintf(wzArgs, 50, L"-oid %d", oid);
				LaunchProgram(L"\\program files\\mobilesrc\\sideline\\sideline.exe", wzArgs);
			}
		}
	}
}

void AddSidelineTile(HWND hwndList)
{
	DWORD dwEnabled = 1;
	HRESULT hr = RegistryGetDWORD(HKEY_CURRENT_USER, L"Software\\mobileSRC\\Sideline", L"IntegrationEnabled", &dwEnabled);

	if (dwEnabled)
	{
		int index2 = ListBox_FindItemData(hwndList, 0, 0);

		if (index2 < 0)
		{
			int index = ListBox_InsertItemData(hwndList, -1, 0);
		}
	}
}

LRESULT CALLBACK LBSubClass(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	static MenuExtension* pMenuExtension = NULL;
	switch (uMsg)
	{
		case (WM_USER+0xFF) :
		{
			pMenuExtension = (MenuExtension*)lParam;
		}
		return 0;
		case WM_KEYDOWN :
			{
				// get item here -- if it's ours, handle
				if (wParam == VK_RETURN)
				{
					if (0 == ListBox_GetItemData(g_hwndList, ListBox_GetCurSel(g_hwndList)))
					{
						OnClick(pMenuExtension);
						return 0;
					}
				}
			}
			break;
	}
	return CallWindowProc((WNDPROC)g_lOldProcLB, hwnd, uMsg, wParam, lParam);
}
LRESULT CALLBACK SubClass(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	static MenuExtension* pMenuExtension = NULL;
	switch (uMsg)
	{
		case (WM_USER+0xFF) :
		{
			pMenuExtension = (MenuExtension*)lParam;
		}
		return 0;
		// handle selection changed and change menu
		case (WM_APP+1) :
			{
				// get item here -- if it's ours, handle
				if (0 == ListBox_GetItemData(g_hwndList, ListBox_GetCurSel(g_hwndList)))
				{
					OnClick(pMenuExtension);
					return 0;
				}
			}
			break;
		case WM_SETFOCUS :
		case WM_COMMAND :
		{
			if (0 == LOWORD(wParam) && (LBN_SETFOCUS == HIWORD(wParam) || LBN_SELCHANGE == HIWORD(wParam)))
			{
				if (0 == ListBox_GetItemData(g_hwndList, ListBox_GetCurSel(g_hwndList)))
				{
					HWND hwndMenu = SHFindMenuBar(GetForegroundWindow());
					WCHAR wzText[50];
					TBBUTTONINFO a = {0};
					TBBUTTONINFO* tbbi = &a;
					tbbi->cbSize       = sizeof(TBBUTTONINFO);
					tbbi->dwMask       = TBIF_TEXT | TBIF_COMMAND;
					tbbi->cchText = 50;
					tbbi->idCommand = 0x32fb;
					tbbi->pszText = wzText;

					wcscpy(tbbi->pszText, L"Sideline");
					SendMessage(hwndMenu, TB_SETBUTTONINFO, 0x32fb, (LPARAM)tbbi);
					SHEnableSoftkey(hwndMenu, 0x32fb, FALSE, TRUE);

					if (uMsg == WM_COMMAND)
					{
						return 0;
					}
				}
			}
			else if (0x32FB == LOWORD(wParam))
			{
				if (0 == ListBox_GetItemData(g_hwndList, ListBox_GetCurSel(g_hwndList)))
				{
					OnClick(pMenuExtension);
					return 0;
				}
			}
		}
		break;
		case WM_ACTIVATE :
		{
			HWND hwndList = GetWindow(hwnd, GW_CHILD);
			AddSidelineTile(hwndList);
		}
		break;
		case WM_MEASUREITEM :
		{
			TEXTMETRIC tm;
			LPMEASUREITEMSTRUCT lpmis;
			lpmis = (MEASUREITEMSTRUCT FAR*) lParam;

			if (lpmis->itemData == 0)
			{
				int iconSize = GetSystemMetrics(SM_CXSMICON);
				lpmis->itemHeight = iconSize * 2;
				return 0;
			}
		}
		break;
		case WM_DRAWITEM :
		{
			HWND hwndList = GetWindow(hwnd, GW_CHILD);

			LPDRAWITEMSTRUCT lpdis = (DRAWITEMSTRUCT FAR*) lParam;
			
			if (lpdis->itemData != 0)
			{
				AddSidelineTile(hwndList);
			}
			else
			{
				HBRUSH hBrush = NULL;
				SetBkMode(lpdis->hDC, TRANSPARENT);
				if ((lpdis->itemAction & (ODA_SELECT | ODA_DRAWENTIRE)))
				{
					if ((lpdis->itemState & ODS_SELECTED))
					{
						hBrush = GetSysColorBrush(COLOR_HIGHLIGHT);
						SetTextColor(lpdis->hDC, GetSysColor(COLOR_HIGHLIGHTTEXT));
					}
					else
					{
						hBrush = GetSysColorBrush(COLOR_WINDOW);
						SetTextColor(lpdis->hDC, GetSysColor(COLOR_WINDOWTEXT));
					}
					FillRect(lpdis->hDC, &(lpdis->rcItem), hBrush);

					int iconSize = GetSystemMetrics(SM_CXSMICON);
					double padLeft = (11.0 * min(GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN)) / 480.0);
					HICON hIcon = LoadIconW(MenuExtension::g_hInstance, MAKEINTRESOURCE(IDI_SIDELINE));
					DrawIconEx(lpdis->hDC, lpdis->rcItem.left + (int)padLeft, lpdis->rcItem.top + 3, hIcon, iconSize, iconSize, 0, NULL, DI_NORMAL);
					DestroyIcon(hIcon);

					int fontSize = 0;
					SHGetUIMetrics(SHUIM_FONTSIZE_PIXEL, &fontSize, sizeof(fontSize), NULL);

					LOGFONT lf;
					ZeroMemory(&lf, sizeof(lf));
					lf.lfHeight = -fontSize;
					lf.lfWeight = FW_BOLD;
					lf.lfCharSet = DEFAULT_CHARSET;
					HFONT hFont = CreateFontIndirect(&lf);
					HGDIOBJ hOldFont = SelectObject(lpdis->hDC, hFont);

					RECT rc = {0};
					memcpy(&rc, &(lpdis->rcItem), sizeof(RECT));
					int height = (rc.bottom - rc.top) >> 1;

					rc.left += (iconSize) + (int)(2*padLeft);
					rc.bottom = rc.top + height;
					DrawText(lpdis->hDC, L"Create New Sideline Task", -1, &rc, DT_VCENTER);

					SelectObject(lpdis->hDC, hOldFont);
					DeleteObject(hFont);

					ZeroMemory(&lf, sizeof(lf));
					lf.lfHeight = -fontSize;
					lf.lfWeight = FW_NORMAL;
					lf.lfCharSet = DEFAULT_CHARSET;
					hFont = CreateFontIndirect(&lf);
					hOldFont = SelectObject(lpdis->hDC, hFont);

					rc.top = rc.bottom;
					rc.bottom += height;
					DrawText(lpdis->hDC, L"Click to Sideline a call for this contact", -1, &rc, DT_VCENTER);

					SelectObject(lpdis->hDC, hOldFont);
					DeleteObject(hFont);

				}
			}
		}
		break;
	}
	return CallWindowProc((WNDPROC)g_lOldProc, hwnd, uMsg, wParam, lParam);
}

void ActiveApplicationCallback(HREGNOTIFY hNotifyActiveApplication, DWORD dwUserData, const PBYTE pData, const UINT cbData)
{
	MenuExtension* pExtension = (MenuExtension*)dwUserData;
	Sleep(50);
	HWND hWnd = GetForegroundWindow();
	HWND hWndChild = GetWindow(GetWindow(hWnd, GW_CHILD), GW_CHILD);

	HWND hwndContacts = FindWindow(L"CONTACTS_SUMMARY_CARD", NULL);

	if (hwndContacts && IsWindowVisible(hwndContacts))
	{
		hWndChild = hwndContacts;
	}

	bool bSetActive = !pData;
	if (pData)
	{
		WCHAR* wzStr = (WCHAR*)pData;
		int iLen = wcslen(wzStr);

		if (iLen > 0)
		{
			if (wzStr[iLen-1] != L'!')
			{
				bSetActive = true;
			}
		}
	}
	if (bSetActive)
	{
		WCHAR wzText[MAX_PATH];
		RegistryGetString(SN_ACTIVEAPPLICATION_ROOT, SN_ACTIVEAPPLICATION_PATH, SN_ACTIVEAPPLICATION_VALUE, wzText, sizeof(wzText) / sizeof(WCHAR));

		StringCchCat(wzText, sizeof(wzText) / sizeof(WCHAR), L"!");

		HRESULT hr = RegistrySetString(SN_ACTIVEAPPLICATION_ROOT, SN_ACTIVEAPPLICATION_PATH, SN_ACTIVEAPPLICATION_VALUE, wzText);

		if (pData)
		{
			return;
		}
	}
	if (hWndChild == g_hwndSC)
	{
		return;
	}
	if (g_hwndSC && g_lOldProc)
	{
		SetWindowLong(g_hwndSC, GWL_WNDPROC, g_lOldProc);
		SetWindowLong(g_hwndList, GWL_WNDPROC, g_lOldProcLB);
		g_hwndSC = NULL;
		g_lOldProc = NULL;
		g_lOldProcLB = NULL;
	}

	bool bHandled = false;

	if (hWndChild)
	{
		WCHAR wzClass[500] = {0};
		GetClassName(hWndChild, wzClass, sizeof(wzClass) / sizeof(WCHAR));

		if (0 == wcscmp(L"CONTACTS_SUMMARY_CARD", wzClass))
		{
			g_hwndList = GetWindow(hWndChild, GW_CHILD);

			byte* pVoid0 = (byte*)ListBox_GetItemData(g_hwndList, 0);
			byte* pVoid1 = (byte*)ListBox_GetItemData(g_hwndList, 1);
			byte* pVoid2 = (byte*)ListBox_GetItemData(g_hwndList, 2);
			byte* pVoid3 = (byte*)ListBox_GetItemData(g_hwndList, 3);
			byte* pVoid4 = (byte*)ListBox_GetItemData(g_hwndList, 4);

			g_hwndSC = hWndChild;
			g_lOldProc = SetWindowLong(hWndChild, GWL_WNDPROC, (LONG)SubClass);
			g_lOldProcLB = SetWindowLong(g_hwndList, GWL_WNDPROC, (LONG)LBSubClass);

			SendMessage(hWndChild, WM_USER+0xFF, NULL, (LPARAM)pExtension);
			SendMessage(g_hwndList, WM_USER+0xFF, NULL, (LPARAM)pExtension);

			Sleep(500);

			AddSidelineTile(g_hwndList);
		}
		Sleep(0);
	}
}


///////////////////////////////////////////////////////////////////////////////
// GetPOOM
//
//  Helper function to CoCreate the IPOutloookApp2 Application
//
//  NOTE: POutlook.exe will be running (if it is not already running) after 
//  this function is called
//
//  Arguments:
//      [OUT] IPOutlookApp2 **ppPOOM - an unitialized pointer to pointer to the 
//          Pocket Outlook application object model. (POOM)
//
//  Return Values:
//      HRESULT - returns S_OK / E_FAIL.
//
//
HRESULT GetPOOM(IPOutlookApp2 **ppPOOM)
{
    HRESULT hr = E_FAIL;
    IUnknown * pUnknown = NULL;
    IPOutlookApp2 * polApp = NULL;

    hr = ::CoCreateInstance(CLSID_Application, 
                 NULL, CLSCTX_INPROC_SERVER, 
                 IID_IUnknown, 
                 (void **)&pUnknown);
    CHR(hr);

    hr = pUnknown->QueryInterface(IID_IPOutlookApp, (void**)&polApp);
    CHR(hr);

    hr = polApp->Logon(NULL);
    CHR(hr);

    *ppPOOM = polApp;
                 
Error:
    RELEASE_OBJ(pUnknown);
    return hr;
}

///////////////////////////////////////////////////////////////////////////////
// MenuExtension::MenuExtension - Constructor
//
//  Initializes member variables and increments the g_cDLLRefCount
//
//  Arguments:
//      [IN] ExtenstionType extensionType - a value from the enum ExtensionType
//          which indicates which type of menu extension to instantiate
//
//  Return Values:
//      MenuExtension object
//
MenuExtension::MenuExtension(ExtensionType extensionType) : 
    m_cRef(1), 
    m_pSite(NULL),
    m_idcCallWork(0),
    m_fInitialized(FALSE),
    m_polApp(NULL)
{
    RELEASE_OBJ(m_pSite);
    g_cDLLRefCount++;
    m_ExtensionType = extensionType;
}


///////////////////////////////////////////////////////////////////////////////
// MenuExtension::MenuExtension - DeConstructor
//
//  Cleans up and de-crements the g_cDLLRefCount
//
//  Arguments:
//      None
//
//  Return Values:
//      None
//
MenuExtension::~MenuExtension()
{
    if (m_polApp)
    {  
        m_polApp->Logoff();
        m_polApp->Release();
        m_polApp = NULL;
    }
    RELEASE_OBJ(m_pSite);
    ASSERT(0 == m_cRef);
    g_cDLLRefCount--;
}



///////////////////////////////////////////////////////////////////////////////
// Initialize
//
//  This method should be called after calling the constructor.  
//
//  Arguments:
//      None
//
//  Return Values:
//      HRESULT - currently always returns S_OK
//
HRESULT MenuExtension::Initialize() 
{
    // If you have logic in your constructor which MAY cause the constructor
    // to fail - you should pull it from the constructor and put it in this 
    // method, reducing the likelihood of a constructor failure

	NOTIFICATIONCONDITION nc;
	HREGNOTIFY hNotifyActiveCallCount = NULL;
	// Receive a notification whenever that bit toggles.
	nc.ctComparisonType = REG_CT_ANYCHANGE;
	nc.TargetValue.dw = 0;

	if (g_menuCount == 0)
	{
		ZeroMemory(&g_menuExts, sizeof(g_menuExts));
		ActiveApplicationCallback(NULL, (DWORD)this, NULL, NULL);
		RegistryNotifyCallback(SN_ACTIVEAPPLICATION_ROOT, SN_ACTIVEAPPLICATION_PATH, SN_ACTIVEAPPLICATION_VALUE, ActiveApplicationCallback, (DWORD)this, NULL, &hNotifyActiveCallCount);
	}
	g_menuExts[g_menuCount++] = this;
	MSG msg;

    return(S_OK);
}


///////////////////////////////////////////////////////////////////////////////
// QueryInterface - IUnknown interface Method
//
//  Returns a pointer to a specified interface on an object to which a client 
//  currently holds an interface pointer. This method must call IUnknown::AddRef 
//  on the pointer it returns.
//
//  Arguments:
//      [IN] REFIID iid - the interface ID we are testing for
//      [OUT] LPVOID *ppv - a pointer the object 
//
//  Return Values:
//      HRESULT - S_OK on success, E_NOINTERFACE if the inteface ID passed in
//          using iid is not supported by this object
//
STDMETHODIMP MenuExtension::QueryInterface(REFIID iid, LPVOID *ppv)
{ 
    HRESULT hr = S_OK;

    *ppv = NULL;

    if(IID_IUnknown == iid)
    {
        *ppv = static_cast<IObjectWithSite*>(this);
    }        
    else if(IID_IObjectWithSite == iid)
    {
        *ppv = static_cast<IObjectWithSite*>(this);
    }
    else if(IID_IContextMenu == iid)
    {
        *ppv = static_cast<IContextMenu*>(this);
    }
    else
    {
        CHR(E_NOINTERFACE); 
    }
    
    (reinterpret_cast<IUnknown*>(*ppv))->AddRef();

Error:
    return(hr);
}


///////////////////////////////////////////////////////////////////////////////
// AddRef - IUnknown interface Method
//
//  Increments the reference count for an interface on an object. It should be 
//  called for every new copy of a pointer to an interface on a specified object.
//
//  Arguments:
//      none
//
//  Return Values:
//      ULONG - the current reference count to this object
//
STDMETHODIMP_(ULONG) MenuExtension::AddRef() 
{
    return ::InterlockedIncrement(&m_cRef);
}


///////////////////////////////////////////////////////////////////////////////
// Release - IUnknown interface Method
//
//  Decrements the reference count for the calling interface on a object. If 
//  the reference count on the object falls to 0, the object is freed from 
//  memory.
//
//  Arguments:
//      none
//
//  Return Values:
//      ULONG - the current reference count to this object
//
STDMETHODIMP_(ULONG) MenuExtension::Release() 
{
    if(0 == ::InterlockedDecrement(&m_cRef)) 
    {
        delete this; 
        return(0);
    } 
    return(m_cRef);
}


///////////////////////////////////////////////////////////////////////////////
// SetSite - IObjectWithSite interface Method
//
//  Provides the site's IUnknown pointer to the object.
// 
//  This method is how the context menu extension gets a pointer to its context.
//  The "site" is a pointer to the object in the UI that the menu extension 
//  will be acting on, i.e. the menu it will be extending
//
//  Arguments:
//      [IN] IUnknown* pSite - a pointer to the object in the Application's UI
//
//  Return Values:
//      HRESULT - always will be S_OK
//
STDMETHODIMP MenuExtension::SetSite(IUnknown* pSite)
{
    if(m_pSite)
    {
        m_pSite->Release();
        m_pSite = NULL;
    }

    if(pSite)
    {
        m_pSite = pSite;
        m_pSite->AddRef();
    }
    return(S_OK);
}

///////////////////////////////////////////////////////////////////////////////
// GetSite - IObjectWithSite interface Method
//
//  Retrieves the last site set with SetSite. If there's no known site, the 
//  this returns a failure code.
// 
//  Arguments:
//      [IN] REFIID iid - the interface ID we are testing the site to see if it 
//          handles
//      [OUT] void** ppvSite - a pointer to the object will be returned
//          if the QI is succesful
//
//  Return Values:
//      HRESULT - S_OK on success else E_FAIL or other HRESULT returned by the 
//          site's QI method (potentially E_NOINTERFACE)
//
STDMETHODIMP MenuExtension::GetSite(REFIID riid, void** ppvSite)
{
    HRESULT hr = S_OK;
    if(m_pSite) 
    {
        hr = m_pSite->QueryInterface(riid, ppvSite);
        CHR(hr);       
    }
    else
    {    
        CHR(E_FAIL); 
    }
    
Error:
    return(hr);
}


///////////////////////////////////////////////////////////////////////////////
// InsertMenuItem
//      NOTE: in WinCE there is only InsertMenu
//
//  Simply insert the item in the menu
//
//  Arguments:
//      [IN] HMENU hmenu - a handle the menu to insert into
//      [IN] UINT indexMenu - the position the item will be inserted after
//      [IN] UINT idCmd - the ID of the new item
//      [IN] LPCTSTR szText - the menu text of the new item 
//
//  Return Values:
//      HRESULT - S_OK on success else E_FAIL
//
HRESULT MenuExtension::InsertMenuItem(HMENU hmenu, UINT indexMenu, UINT idCmd, LPCTSTR szText)
{
    HRESULT hr = S_OK;

    if(!::InsertMenu(hmenu, indexMenu, MF_BYPOSITION, idCmd, szText))
    {
        hr = E_FAIL;
    }

    return hr;
}


///////////////////////////////////////////////////////////////////////////////
// QueryContextMenu - IContextMenu interface Method
//
//  Adds the extra menu items to the context menu.  This method is called when 
//  the menu which our menu extension is extended is being created
//
//  Arguments:
//      [IN] HMENU hmenu - a handle the menu to insert into
//      [IN] UINT indexMenu - the position the item will be inserted after
//      [IN] UINT idCmdFirst - the lowest possible ID for a new command to use
//      [IN] UINT idCmdLast - the highest possible ID for a new command to use
//      [IN] LPCTSTR szText - the menu text of the new item 
//      [IN] UINT uFlags - a set of flags indicating what type of insertion and 
//          extension is allowed.  Please see the QueryContextMenu topic in the 
//          the SDK documentation for a complete list of these flags
//
//  Return Values:
//      HRESULT - S_OK on success else E_FAIL
//
HRESULT STDMETHODCALLTYPE MenuExtension::QueryContextMenu(HMENU hmenu,
    UINT indexMenu, UINT idCmdFirst, UINT idCmdLast, UINT uFlags)
{
    HRESULT hr = S_OK;
    BOOL fInserted = FALSE;

    m_idcCallWork = idCmdFirst;
    
	CEOID oid;
    // Let's see if we have a contact highlighted and what info it has
    GetContactInfo(&oid); 

	g_oid = oid;
    
    // We should have at least one entry in the context/softkey menu 
    hr = InsertMenuItem(hmenu, indexMenu, m_idcCallWork, cszSidelineContact); 

Error:
    return(hr);
}


///////////////////////////////////////////////////////////////////////////////
// InvokeCommand - IContextMenu interface Method
//
//  Runs a context menu command when the user selects one that was 
//  inserted with QueryContextMenu.  This method is called by the shell when one
//  of the items added by the menu extension is actioned uponed
// 
//  Arguments:
//      [IN] LPCMINVOKECOMMANDINFO lpici - a pointer to a CMINVOKECOMMANDINFO 
//          structure which contains data about the menu item clicked on by a 
//          user.  
//
//  Return Values:
//      HRESULT - S_OK on success. E_INVALIDARG, E_FAIL and other HRESULTS are
//          possible on failure
//
HRESULT STDMETHODCALLTYPE MenuExtension::InvokeCommand(LPCMINVOKECOMMANDINFO lpici)
{
    HRESULT hr = E_FAIL;

    if (lpici->lpVerb)
    {        
		OnClick(this);
    }    

    return hr;

}

///////////////////////////////////////////////////////////////////////////////
// GetCommandString - IContextMenu interface Method
//
//  Called by the shell to validate that the command exists, to get the command 
//  name, or to get the command help text.
//
//  Arguments:
//      [IN] Specifies the menu item ID, offset from the idCmdFirst 
//          parameter of QueryContextMenu. 
//      [IN] UINT uType - Bitmask that specifies that GetCommandString should 
//          either validate that the command exists, get the command name 
//          string, or get the help text string. For a list of possible flag 
//          values, see the table in IContextMenu::GetCommandString Documentation
//      [IN] UINT *pwReserved - Reserved (ignored, must pass NULL). 
//      [OUT] LPSTR pszName - Specifies the string buffer. 
//      [IN] UINT cchMax - Specifies the size of the string buffer (pszName)
//
//  Return Values:
//      HRESULT - S_OK on success. E_INVALIDARG, E_FAIL and other HRESULTS are
//          possible on failure
//
HRESULT STDMETHODCALLTYPE MenuExtension::GetCommandString(UINT_PTR idCmd,
        UINT uType, UINT* pwReserved, LPSTR pszName, UINT cchMax)
{

	StringCchCopyA(pszName, cchMax, "Sideline");
	return S_OK;
}


///////////////////////////////////////////////////////////////////////////////
//  Create (Static method)
//
//  When the applications's menu is rendered, it checks to see if there are any 
//  menu extensions registered.  If so DllGetClassObject (See Main.cpp) will be 
//  called for each extension.  DllGetClassObject instantiates MyClassFactory 
//  (see ClassFactory.cpp) which will call MyClassFactory::CreateInstance which 
//  will in turn call this method
//
//  Create then calls the MenuExtension constructor.
//
//  Arguments:
//      [OUT] IObjectWithSite** ppNew - a pointer to our new Menu Extension
//      [IN] ExtensionType extensionType - which type of extension is this
//
//  Return Values:
//      HRESULT - S_OK on success. E_OUTOFMEMORY, E_FAIL and other HRESULTS are
//          possible on failure
// 
HRESULT MenuExtension::Create(IObjectWithSite** ppNew, ExtensionType extensionType)
{
    HRESULT hr = S_OK;
    MenuExtension* pte = NULL;

    pte = new MenuExtension(extensionType);
    if (pte)
    {
        hr = pte->Initialize();
           
        if (SUCCEEDED(hr))
        {
            *ppNew = pte;
            pte = NULL;
        }
    }
    else
    {
        hr = E_OUTOFMEMORY;
    }

    delete(pte);

    return(hr);
    
}

///////////////////////////////////////////////////////////////////////////////
// GetContactInfo
//
//  Get selected contact's first name, last name, home, mobile and work numbers, if any
//
//  Arguments:
//      None.
//
//  Return Values:
//      BOOL - TRUE if at least one number (home/work/mobile) retrieved, and we have valid
//             values for first and last name (empty string is valid).
// 
BOOL MenuExtension::GetContactInfo(CEOID* pOid)
{      
    BOOL fResult = FALSE;
    HRESULT hr = E_FAIL;
    IDataObject* pobj;

    // Do not try to collect info if no contact is highlighted
    if (!m_pSite) 
    {
        return FALSE;
    }

    hr = m_pSite->QueryInterface(IID_IDataObject, (void**) &pobj);
    if (SUCCEEDED(hr))
    {
        CEOID oid;
    
        if (GetContactsOidFromSelection(pobj, pOid))
        {
			fResult = TRUE;
        }

        pobj->Release();
    }

    return fResult;
}


///////////////////////////////////////////////////////////////////////////////
// GetContactInfo
//
//  Read oid/etc. for selected item
//
//  Arguments:
//      [IN] IDataObject *pdo     IDataObject for item
//      [OUT] CEIOD *poidOut      pointer to oid of contact selected 
//  BOOL    (ret) TRUE if Contacts Oid obtained.  FALSE otherwise.
//
//  Return Values:
//      BOOL - TRUE if Contact oid obtained. FALSE otherwise
//
BOOL MenuExtension::GetContactsOidFromSelection(IDataObject *pdo, CEOID *poidOut)
{
    BOOL bRet = FALSE;
    HRESULT hr = S_OK;
    CEOID oid = 0;
    FORMATETC fmte;
    STGMEDIUM med;

    *poidOut = 0;
    
    memset(&fmte, 0, sizeof(fmte));
    memset(&med, 0, sizeof(med));
    fmte.cfFormat = RegisterClipboardFormat(CFNAME_ITEMREFARRAY);
    fmte.lindex = -1;
    fmte.tymed = TYMED_HGLOBAL;

    // Get selection info
    CHR(pdo->GetData(&fmte, &med)); 

    // Contacts give back ItemRef's    
    ItemRefArray * pArray;
    ItemRef * pRef;

    // Unpack selection info
    pArray = (ItemRefArray *) med.hGlobal;
    CPR(pArray); 

    // On SP we just get back one (no multiple-selection UI).
    // On PPC we can get back more than one, so let's pick the first one      
    pRef = &pArray->rgRefs[0];

    // Make sure this is a Contact item
    if (IsEqualGUID(*(pRef->pType), ITI_ContactItemRef))
    {
		*poidOut = (CEOID) (pRef->pRef);
        bRet = TRUE;
    }
 
    // Cleanup
    ReleaseStgMedium(&med);
  
Error:
    return bRet;
}
