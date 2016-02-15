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

#pragma once

#include <pimstore.h>
///////////////////////////////////////////////////////////////////////////
// Calling card and contact information Structure
//

// Maximum length in chars of the numbers (calling card number and pin) we read in from the registry
#define MAX_NUMBER_LEN 64

class MenuExtension :
    public IObjectWithSite,
    public IContextMenu
{

    public:
    static HRESULT Create(IObjectWithSite** ppNew, ExtensionType extensionType);
	static HINSTANCE g_hInstance;
	static CEOID g_oid;

    ///////////////////////////////////////////////////////////////////////////
    // IUnknown Interface Methods
    //
    STDMETHODIMP QueryInterface(REFIID iid, LPVOID * ppv);
    STDMETHODIMP_(ULONG) AddRef(); 
    STDMETHODIMP_(ULONG) Release(); 
    //
    ///////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////
    // IObjectWithSite Interface Methods
    //
    virtual STDMETHODIMP SetSite(IUnknown* pSite);
    virtual STDMETHODIMP GetSite(REFIID riid, void** ppvSite);
    //
    ///////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////
    // IContextMenu Interface Methods
    //
    virtual HRESULT STDMETHODCALLTYPE QueryContextMenu(HMENU hmenu,
        UINT indexMenu, UINT idCmdFirst, UINT idCmdLast, UINT uFlags);
    virtual HRESULT STDMETHODCALLTYPE InvokeCommand(LPCMINVOKECOMMANDINFO lpici);
    virtual HRESULT STDMETHODCALLTYPE GetCommandString(UINT_PTR idCmd,
        UINT uType, UINT* pwReserved, LPSTR pszBad, UINT cchMax);
    //
    ///////////////////////////////////////////////////////////////////////////
    

    public:    
    MenuExtension(ExtensionType extensionType); 
    virtual ~MenuExtension();
    HRESULT Initialize();

    /////// Helpers
    
    BOOL GetContactsOidFromSelection(IDataObject *pdo, CEOID *poidOut);
    HRESULT InsertMenuItem(HMENU hmenu, UINT indexMenu, UINT idCmd, LPCTSTR szText); 
    BOOL GetContactInfo(CEOID* pOid);

    public:
    LONG m_cRef;                // COM refcount
    IUnknown* m_pSite;  //  a pointer to the object in the UI that the menu extension 
                        // will be acting on.
    UINT m_idcCallWork;

    IPOutlookApp2 *m_polApp;
    
    BOOL m_fInitialized;   // Have we read in 
    ExtensionType m_ExtensionType;  // type of extension Context, Softkey, etc..
};


