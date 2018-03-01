

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Thu Mar 16 14:30:29 2017
 */
/* Compiler settings for Autoclik.odl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __ACDual_h__
#define __ACDual_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IAClick_FWD_DEFINED__
#define __IAClick_FWD_DEFINED__
typedef interface IAClick IAClick;

#endif 	/* __IAClick_FWD_DEFINED__ */


#ifndef __IDualAutoClickPoint_FWD_DEFINED__
#define __IDualAutoClickPoint_FWD_DEFINED__
typedef interface IDualAutoClickPoint IDualAutoClickPoint;

#endif 	/* __IDualAutoClickPoint_FWD_DEFINED__ */


#ifndef __IDualAClick_FWD_DEFINED__
#define __IDualAClick_FWD_DEFINED__
typedef interface IDualAClick IDualAClick;

#endif 	/* __IDualAClick_FWD_DEFINED__ */


#ifndef __Document_FWD_DEFINED__
#define __Document_FWD_DEFINED__

#ifdef __cplusplus
typedef class Document Document;
#else
typedef struct Document Document;
#endif /* __cplusplus */

#endif 	/* __Document_FWD_DEFINED__ */


#ifndef __IAutoClickPoint_FWD_DEFINED__
#define __IAutoClickPoint_FWD_DEFINED__
typedef interface IAutoClickPoint IAutoClickPoint;

#endif 	/* __IAutoClickPoint_FWD_DEFINED__ */


#ifndef __Point_FWD_DEFINED__
#define __Point_FWD_DEFINED__

#ifdef __cplusplus
typedef class Point Point;
#else
typedef struct Point Point;
#endif /* __cplusplus */

#endif 	/* __Point_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __ACDual_LIBRARY_DEFINED__
#define __ACDual_LIBRARY_DEFINED__

/* library ACDual */
/* [version][uuid] */ 



EXTERN_C const IID LIBID_ACDual;

#ifndef __IAClick_DISPINTERFACE_DEFINED__
#define __IAClick_DISPINTERFACE_DEFINED__

/* dispinterface IAClick */
/* [uuid] */ 


EXTERN_C const IID DIID_IAClick;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("4B115280-32F0-11cf-AC85-444553540000")
    IAClick : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct IAClickVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IAClick * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IAClick * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IAClick * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IAClick * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IAClick * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IAClick * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IAClick * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IAClickVtbl;

    interface IAClick
    {
        CONST_VTBL struct IAClickVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAClick_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IAClick_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IAClick_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IAClick_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IAClick_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IAClick_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IAClick_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* __IAClick_DISPINTERFACE_DEFINED__ */


#ifndef __IDualAutoClickPoint_INTERFACE_DEFINED__
#define __IDualAutoClickPoint_INTERFACE_DEFINED__

/* interface IDualAutoClickPoint */
/* [object][dual][oleautomation][uuid] */ 


EXTERN_C const IID IID_IDualAutoClickPoint;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0BDD0E80-0DD7-11cf-BBA8-444553540000")
    IDualAutoClickPoint : public IDispatch
    {
    public:
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_x( 
            /* [in] */ short newX) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_x( 
            /* [retval][out] */ short *retval) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_y( 
            /* [in] */ short newY) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_y( 
            /* [retval][out] */ short *retval) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IDualAutoClickPointVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDualAutoClickPoint * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDualAutoClickPoint * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDualAutoClickPoint * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IDualAutoClickPoint * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IDualAutoClickPoint * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IDualAutoClickPoint * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IDualAutoClickPoint * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_x )( 
            IDualAutoClickPoint * This,
            /* [in] */ short newX);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_x )( 
            IDualAutoClickPoint * This,
            /* [retval][out] */ short *retval);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_y )( 
            IDualAutoClickPoint * This,
            /* [in] */ short newY);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_y )( 
            IDualAutoClickPoint * This,
            /* [retval][out] */ short *retval);
        
        END_INTERFACE
    } IDualAutoClickPointVtbl;

    interface IDualAutoClickPoint
    {
        CONST_VTBL struct IDualAutoClickPointVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDualAutoClickPoint_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDualAutoClickPoint_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDualAutoClickPoint_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDualAutoClickPoint_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IDualAutoClickPoint_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IDualAutoClickPoint_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IDualAutoClickPoint_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IDualAutoClickPoint_put_x(This,newX)	\
    ( (This)->lpVtbl -> put_x(This,newX) ) 

#define IDualAutoClickPoint_get_x(This,retval)	\
    ( (This)->lpVtbl -> get_x(This,retval) ) 

#define IDualAutoClickPoint_put_y(This,newY)	\
    ( (This)->lpVtbl -> put_y(This,newY) ) 

#define IDualAutoClickPoint_get_y(This,retval)	\
    ( (This)->lpVtbl -> get_y(This,retval) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDualAutoClickPoint_INTERFACE_DEFINED__ */


#ifndef __IDualAClick_INTERFACE_DEFINED__
#define __IDualAClick_INTERFACE_DEFINED__

/* interface IDualAClick */
/* [object][dual][oleautomation][uuid] */ 


EXTERN_C const IID IID_IDualAClick;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0BDD0E81-0DD7-11cf-BBA8-444553540000")
    IDualAClick : public IDispatch
    {
    public:
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_text( 
            /* [in] */ BSTR newText) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_text( 
            /* [retval][out] */ BSTR *retval) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_x( 
            /* [in] */ short newX) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_x( 
            /* [retval][out] */ short *retval) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_y( 
            /* [in] */ short newY) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_y( 
            /* [retval][out] */ short *retval) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_Position( 
            /* [in] */ IDualAutoClickPoint *newPosition) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_Position( 
            /* [retval][out] */ IDualAutoClickPoint **retval) = 0;
        
        virtual /* [id][propputref] */ HRESULT STDMETHODCALLTYPE putref_Position( 
            /* [in] */ IDualAutoClickPoint *newPosition) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE RefreshWindow( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE SetAllProps( 
            /* [in] */ short x,
            /* [in] */ short y,
            /* [in] */ BSTR text) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE ShowWindow( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE TestError( 
            /* [in] */ short wCode) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IDualAClickVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDualAClick * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDualAClick * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDualAClick * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IDualAClick * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IDualAClick * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IDualAClick * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IDualAClick * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_text )( 
            IDualAClick * This,
            /* [in] */ BSTR newText);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_text )( 
            IDualAClick * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_x )( 
            IDualAClick * This,
            /* [in] */ short newX);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_x )( 
            IDualAClick * This,
            /* [retval][out] */ short *retval);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_y )( 
            IDualAClick * This,
            /* [in] */ short newY);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_y )( 
            IDualAClick * This,
            /* [retval][out] */ short *retval);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Position )( 
            IDualAClick * This,
            /* [in] */ IDualAutoClickPoint *newPosition);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Position )( 
            IDualAClick * This,
            /* [retval][out] */ IDualAutoClickPoint **retval);
        
        /* [id][propputref] */ HRESULT ( STDMETHODCALLTYPE *putref_Position )( 
            IDualAClick * This,
            /* [in] */ IDualAutoClickPoint *newPosition);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *RefreshWindow )( 
            IDualAClick * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *SetAllProps )( 
            IDualAClick * This,
            /* [in] */ short x,
            /* [in] */ short y,
            /* [in] */ BSTR text);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *ShowWindow )( 
            IDualAClick * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *TestError )( 
            IDualAClick * This,
            /* [in] */ short wCode);
        
        END_INTERFACE
    } IDualAClickVtbl;

    interface IDualAClick
    {
        CONST_VTBL struct IDualAClickVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDualAClick_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDualAClick_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDualAClick_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDualAClick_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IDualAClick_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IDualAClick_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IDualAClick_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IDualAClick_put_text(This,newText)	\
    ( (This)->lpVtbl -> put_text(This,newText) ) 

#define IDualAClick_get_text(This,retval)	\
    ( (This)->lpVtbl -> get_text(This,retval) ) 

#define IDualAClick_put_x(This,newX)	\
    ( (This)->lpVtbl -> put_x(This,newX) ) 

#define IDualAClick_get_x(This,retval)	\
    ( (This)->lpVtbl -> get_x(This,retval) ) 

#define IDualAClick_put_y(This,newY)	\
    ( (This)->lpVtbl -> put_y(This,newY) ) 

#define IDualAClick_get_y(This,retval)	\
    ( (This)->lpVtbl -> get_y(This,retval) ) 

#define IDualAClick_put_Position(This,newPosition)	\
    ( (This)->lpVtbl -> put_Position(This,newPosition) ) 

#define IDualAClick_get_Position(This,retval)	\
    ( (This)->lpVtbl -> get_Position(This,retval) ) 

#define IDualAClick_putref_Position(This,newPosition)	\
    ( (This)->lpVtbl -> putref_Position(This,newPosition) ) 

#define IDualAClick_RefreshWindow(This)	\
    ( (This)->lpVtbl -> RefreshWindow(This) ) 

#define IDualAClick_SetAllProps(This,x,y,text)	\
    ( (This)->lpVtbl -> SetAllProps(This,x,y,text) ) 

#define IDualAClick_ShowWindow(This)	\
    ( (This)->lpVtbl -> ShowWindow(This) ) 

#define IDualAClick_TestError(This,wCode)	\
    ( (This)->lpVtbl -> TestError(This,wCode) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDualAClick_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Document;

#ifdef __cplusplus

class DECLSPEC_UUID("4B115281-32F0-11cf-AC85-444553540000")
Document;
#endif

#ifndef __IAutoClickPoint_DISPINTERFACE_DEFINED__
#define __IAutoClickPoint_DISPINTERFACE_DEFINED__

/* dispinterface IAutoClickPoint */
/* [uuid] */ 


EXTERN_C const IID DIID_IAutoClickPoint;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("4B115283-32F0-11cf-AC85-444553540000")
    IAutoClickPoint : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct IAutoClickPointVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IAutoClickPoint * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IAutoClickPoint * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IAutoClickPoint * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IAutoClickPoint * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IAutoClickPoint * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IAutoClickPoint * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IAutoClickPoint * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IAutoClickPointVtbl;

    interface IAutoClickPoint
    {
        CONST_VTBL struct IAutoClickPointVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAutoClickPoint_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IAutoClickPoint_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IAutoClickPoint_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IAutoClickPoint_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IAutoClickPoint_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IAutoClickPoint_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IAutoClickPoint_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* __IAutoClickPoint_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Point;

#ifdef __cplusplus

class DECLSPEC_UUID("4B115285-32F0-11cf-AC85-444553540000")
Point;
#endif
#endif /* __ACDual_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


