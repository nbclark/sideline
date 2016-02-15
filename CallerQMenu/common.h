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

///////////////////////////////////////////////////////////////////////////////
// Windows Core
#include <windows.h>
#include <Ocidl.h>


///////////////////////////////////////////////////////////////////////////////
// Windows Mobile
#include <shlguid.h>
#include <COGUID.H>
#include <aygshell.h>

///////////////////////////////////////////////////////////////////////////////
// Menu Extension definitions - ItemRefArray, ItemRef, CFNAME_ITEMREFARRAY
#include <appext.h>        


// NOTE:
// If you are creating new menu extensions based on this code
// you absolutely must create your own CLSIDs!  
// Visual Studio's Guidgen.exe is an excellent tool for this

// {32E6CD28-CEBD-493e-AFEC-86BA2B9C5A15}
DEFINE_GUID(CLSID_CONTACTS_SK_MENUEXT, 0x32e6cd28, 0xcebd, 0x493e, 0xaf, 0xec, 0x86, 0xba, 0x2b, 0x9c, 0x5a, 0x15);
#define CLSIDTEXT_CONTACTS_SK_MENUEXT TEXT("32E6CD28-CEBD-493e-AFEC-86BA2B9C5A15")

// {A1A5BFA9-9790-4f33-BA9E-6106F767821F}
DEFINE_GUID(CLSID_CONTACTS_CONTEXT_MENUEXT, 0xa1a5bfa9, 0x9790, 0x4f33, 0xba, 0x9e, 0x61, 0x6, 0xf7, 0x67, 0x82, 0x1F);
#define CLSIDTEXT_CONTACTS_CONTEXT_MENUEXT TEXT("A1A5BFA9-9790-4f33-BA9E-6106F767821F")

// {A1A5BFA9-9790-4f33-BA9E-6106F767821F}
DEFINE_GUID(CLSID_PHONE_LOG_MENUEXT, 0xa1a5bfa1, 0x9790, 0x4f33, 0xba, 0x9e, 0x61, 0x6, 0xf7, 0x67, 0x82, 0x1F);
#define CLSIDTEXT_PHONE_LOG_MENUEXT TEXT("A1A5BFA1-9790-4f33-BA9E-6106F767821F")

// {A1A5BFA9-9790-4f33-BA9E-6106F767821F}
DEFINE_GUID(CLSID_PHONE_MSSCUT_MENUEXT, 0xa1a5bfa2, 0x9790, 0x4f33, 0xba, 0x9e, 0x61, 0x6, 0xf7, 0x67, 0x82, 0x1F);
#define CLSIDTEXT_PHONE_MSSCUT_MENUEXT TEXT("A1A5BFA2-9790-4f33-BA9E-6106F767821F")

DEFINE_GUID(IID_IObjectWithSite, 0xFC4801A3, 0x2BA9, 0x11CF, 0xA2, 0x29, 0x00, 0xAA, 0x00, 0x3D, 0x73, 0x52);
DEFINE_GUID(IID_IDataObject,     0x0000010e, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46);


// Used to help us determine if we are a Context or Softkey Menu Ext
typedef enum {
    Context,
    Softkey,
} ExtensionType;

#include "macros.h"
