//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************


/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Mon Jan 18 19:14:07 2038
 */
/* Compiler settings for C:\Users\User\AppData\Local\Temp\Microsoft.SDKSamples.Kitchen.idl-dd1f0364:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
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

/* verify that the <rpcsal.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCSAL_H_VERSION__
#define __REQUIRED_RPCSAL_H_VERSION__ 100
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __Microsoft2ESDKSamples2EKitchen_h__
#define __Microsoft2ESDKSamples2EKitchen_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#if defined(__cplusplus)
#if defined(__MIDL_USE_C_ENUM)
#define MIDL_ENUM enum
#else
#define MIDL_ENUM enum class
#endif
#endif


/* Forward Declarations */ 

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_FWD_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_FWD_DEFINED__
typedef interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread;

#ifdef __cplusplus
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                interface IBread;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

#endif /* __cplusplus */

#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_FWD_DEFINED__ */


#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_FWD_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_FWD_DEFINED__
typedef interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance;

#ifdef __cplusplus
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                interface IAppliance;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

#endif /* __cplusplus */

#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_FWD_DEFINED__ */


#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_FWD_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_FWD_DEFINED__
typedef interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs;

#ifdef __cplusplus
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                interface IBreadBakedEventArgs;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

#endif /* __cplusplus */

#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_FWD_DEFINED__ */


#ifndef ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_FWD_DEFINED__
#define ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_FWD_DEFINED__
typedef interface __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs;

#endif 	/* ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_FWD_DEFINED__ */


#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_FWD_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_FWD_DEFINED__
typedef interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven;

#ifdef __cplusplus
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                interface IOven;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

#endif /* __cplusplus */

#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_FWD_DEFINED__ */


#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_FWD_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_FWD_DEFINED__
typedef interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory;

#ifdef __cplusplus
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                interface IOvenFactory;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

#endif /* __cplusplus */

#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_FWD_DEFINED__ */


/* header files for imported files */
#include "Windows.Foundation.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0000 */
/* [local] */ 

#ifdef __cplusplus
} /*extern "C"*/ 
#endif
#include <windows.foundation.collections.h>
#ifdef __cplusplus
extern "C" {
#endif
#ifdef __cplusplus
namespace ABI {
namespace Microsoft {
namespace SDKSamples {
namespace Kitchen {
class Oven;
} /*Kitchen*/
} /*SDKSamples*/
} /*Microsoft*/
}
#endif

#ifdef __cplusplus
namespace ABI {
namespace Microsoft {
namespace SDKSamples {
namespace Kitchen {
interface IOven;
} /*Kitchen*/
} /*SDKSamples*/
} /*Microsoft*/
}
#endif
#ifdef __cplusplus
namespace ABI {
namespace Microsoft {
namespace SDKSamples {
namespace Kitchen {
class BreadBakedEventArgs;
} /*Kitchen*/
} /*SDKSamples*/
} /*Microsoft*/
}
#endif

#ifdef __cplusplus
namespace ABI {
namespace Microsoft {
namespace SDKSamples {
namespace Kitchen {
interface IBreadBakedEventArgs;
} /*Kitchen*/
} /*SDKSamples*/
} /*Microsoft*/
}
#endif


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0000 */
/* [local] */ 





extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0000_v0_0_s_ifspec;

/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0328 */




/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0328 */




extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0328_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0328_v0_0_s_ifspec;

/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0001 */
/* [local] */ 

#ifndef DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_USE
#define DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_USE
#if defined(__cplusplus) && !defined(RO_NO_TEMPLATE_NAME)
} /*extern "C"*/ 
namespace ABI { namespace Windows { namespace Foundation {
template <>
struct __declspec(uuid("6582b851-18cc-5835-8c50-d896eedda300"))
ITypedEventHandler<ABI::Microsoft::SDKSamples::Kitchen::Oven*,ABI::Microsoft::SDKSamples::Kitchen::BreadBakedEventArgs*> : ITypedEventHandler_impl<ABI::Windows::Foundation::Internal::AggregateType<ABI::Microsoft::SDKSamples::Kitchen::Oven*, ABI::Microsoft::SDKSamples::Kitchen::IOven*>,ABI::Windows::Foundation::Internal::AggregateType<ABI::Microsoft::SDKSamples::Kitchen::BreadBakedEventArgs*, ABI::Microsoft::SDKSamples::Kitchen::IBreadBakedEventArgs*>> {
static const wchar_t* z_get_rc_name_impl() {
return L"Windows.Foundation.TypedEventHandler`2<Microsoft.SDKSamples.Kitchen.Oven, Microsoft.SDKSamples.Kitchen.BreadBakedEventArgs>"; }
};
typedef ITypedEventHandler<ABI::Microsoft::SDKSamples::Kitchen::Oven*,ABI::Microsoft::SDKSamples::Kitchen::BreadBakedEventArgs*> __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_t;
#define ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_FWD_DEFINED__
#define __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs ABI::Windows::Foundation::__FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_t

/* ABI */ } /* Windows */ } /* Foundation */ }
extern "C" {
#endif //__cplusplus
#endif /* DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_USE */
#pragma warning(push)
#pragma warning(disable:4668) 
#pragma warning(disable:4001) 
#pragma once
#pragma warning(pop)
#if !defined(__cplusplus)
struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CDimensions
    {
    double Depth;
    double Height;
    double Width;
    } ;
typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CDimensions __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CDimensions;

#endif
#if !defined(__cplusplus)

#if !defined(__cplusplus)
/* [v1_enum] */ 
enum __x_ABI_CMicrosoft_CSDKSamples_CKitchen_COvenTemperature
    {
        OvenTemperature_Low	= 0,
        OvenTemperature_Medium	= ( OvenTemperature_Low + 1 ) ,
        OvenTemperature_High	= ( OvenTemperature_Medium + 1 ) 
    } ;
#endif /* end if !defined(__cplusplus) */

#if !defined(__cplusplus)

typedef enum __x_ABI_CMicrosoft_CSDKSamples_CKitchen_COvenTemperature __x_ABI_CMicrosoft_CSDKSamples_CKitchen_COvenTemperature;


#endif /* end if !defined(__cplusplus) */


#endif
#ifdef __cplusplus
namespace ABI {
namespace Microsoft {
namespace SDKSamples {
namespace Kitchen {
class Bread;
} /*Kitchen*/
} /*SDKSamples*/
} /*Microsoft*/
}
#endif

#if !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_INTERFACE_DEFINED__)
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Microsoft_SDKSamples_Kitchen_IBread[] = L"Microsoft.SDKSamples.Kitchen.IBread";
#endif /* !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_INTERFACE_DEFINED__) */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0001 */
/* [local] */ 


#ifdef __cplusplus
} /* end extern "C" */
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                
                struct Dimensions
                    {
                    double Depth;
                    double Height;
                    double Width;
                    } ;
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

extern "C" { 
#endif

#ifdef __cplusplus

} /* end extern "C" */
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                
                typedef struct Dimensions Dimensions;
                
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

extern "C" { 
#endif

#ifdef __cplusplus
} /* end extern "C" */
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                
                /* [v1_enum] */ 
                MIDL_ENUM OvenTemperature
                    {
                        Low	= 0,
                        Medium	= ( Low + 1 ) ,
                        High	= ( Medium + 1 ) 
                    } ;

                const MIDL_ENUM OvenTemperature OvenTemperature_Low = OvenTemperature::Low;
                const MIDL_ENUM OvenTemperature OvenTemperature_Medium = OvenTemperature::Medium;
                const MIDL_ENUM OvenTemperature OvenTemperature_High = OvenTemperature::High;
                
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

extern "C" { 
#endif

#ifdef __cplusplus

} /* end extern "C" */
namespace ABI {
    namespace Microsoft {
        namespace SDKSamples {
            namespace Kitchen {
                
                typedef MIDL_ENUM OvenTemperature OvenTemperature;
                
            } /* end namespace */
        } /* end namespace */
    } /* end namespace */
} /* end namespace */

extern "C" { 
#endif




extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0001_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0001_v0_0_s_ifspec;

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_INTERFACE_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_INTERFACE_DEFINED__

/* interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread */
/* [uuid][object] */ 



/* interface ABI::Microsoft::SDKSamples::Kitchen::IBread */
/* [uuid][object] */ 


EXTERN_C const IID IID___x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread;

#if defined(__cplusplus) && !defined(CINTERFACE)
    } /* end extern "C" */
    namespace ABI {
        namespace Microsoft {
            namespace SDKSamples {
                namespace Kitchen {
                    
                    MIDL_INTERFACE("F13EA3D5-7B24-4CDE-9E5F-57AF30F0733C")
                    IBread : public IInspectable
                    {
                    public:
                        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_Flavor( 
                            /* [out][retval] */ __RPC__deref_out_opt HSTRING *value) = 0;
                        
                    };

                    extern const __declspec(selectany) IID & IID_IBread = __uuidof(IBread);

                    
                }  /* end namespace */
            }  /* end namespace */
        }  /* end namespace */
    }  /* end namespace */
    extern "C" { 
    
#else 	/* C style interface */

    typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIids )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This,
            /* [out] */ __RPC__out ULONG *iidCount,
            /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids);
        
        HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This,
            /* [out] */ __RPC__deref_out_opt HSTRING *className);
        
        HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This,
            /* [out] */ __RPC__out TrustLevel *trustLevel);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_Flavor )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread * This,
            /* [out][retval] */ __RPC__deref_out_opt HSTRING *value);
        
        END_INTERFACE
    } __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadVtbl;

    interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread
    {
        CONST_VTBL struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_GetIids(This,iidCount,iids)	\
    ( (This)->lpVtbl -> GetIids(This,iidCount,iids) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_GetRuntimeClassName(This,className)	\
    ( (This)->lpVtbl -> GetRuntimeClassName(This,className) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_GetTrustLevel(This,trustLevel)	\
    ( (This)->lpVtbl -> GetTrustLevel(This,trustLevel) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_get_Flavor(This,value)	\
    ( (This)->lpVtbl -> get_Flavor(This,value) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0002 */
/* [local] */ 

#if !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_INTERFACE_DEFINED__)
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Microsoft_SDKSamples_Kitchen_IAppliance[] = L"Microsoft.SDKSamples.Kitchen.IAppliance";
#endif /* !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_INTERFACE_DEFINED__) */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0002 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0002_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0002_v0_0_s_ifspec;

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_INTERFACE_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_INTERFACE_DEFINED__

/* interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance */
/* [uuid][object] */ 



/* interface ABI::Microsoft::SDKSamples::Kitchen::IAppliance */
/* [uuid][object] */ 


EXTERN_C const IID IID___x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance;

#if defined(__cplusplus) && !defined(CINTERFACE)
    } /* end extern "C" */
    namespace ABI {
        namespace Microsoft {
            namespace SDKSamples {
                namespace Kitchen {
                    
                    MIDL_INTERFACE("332FD2F1-1C69-4C91-949E-4BB67A85BDC5")
                    IAppliance : public IInspectable
                    {
                    public:
                        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_Volume( 
                            /* [out][retval] */ __RPC__out double *value) = 0;
                        
                    };

                    extern const __declspec(selectany) IID & IID_IAppliance = __uuidof(IAppliance);

                    
                }  /* end namespace */
            }  /* end namespace */
        }  /* end namespace */
    }  /* end namespace */
    extern "C" { 
    
#else 	/* C style interface */

    typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIApplianceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIids )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This,
            /* [out] */ __RPC__out ULONG *iidCount,
            /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids);
        
        HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This,
            /* [out] */ __RPC__deref_out_opt HSTRING *className);
        
        HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This,
            /* [out] */ __RPC__out TrustLevel *trustLevel);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_Volume )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance * This,
            /* [out][retval] */ __RPC__out double *value);
        
        END_INTERFACE
    } __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIApplianceVtbl;

    interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance
    {
        CONST_VTBL struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIApplianceVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_GetIids(This,iidCount,iids)	\
    ( (This)->lpVtbl -> GetIids(This,iidCount,iids) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_GetRuntimeClassName(This,className)	\
    ( (This)->lpVtbl -> GetRuntimeClassName(This,className) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_GetTrustLevel(This,trustLevel)	\
    ( (This)->lpVtbl -> GetTrustLevel(This,trustLevel) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_get_Volume(This,value)	\
    ( (This)->lpVtbl -> get_Volume(This,value) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIAppliance_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0003 */
/* [local] */ 

#if !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_INTERFACE_DEFINED__)
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Microsoft_SDKSamples_Kitchen_IBreadBakedEventArgs[] = L"Microsoft.SDKSamples.Kitchen.IBreadBakedEventArgs";
#endif /* !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_INTERFACE_DEFINED__) */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0003 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0003_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0003_v0_0_s_ifspec;

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_INTERFACE_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_INTERFACE_DEFINED__

/* interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs */
/* [uuid][object] */ 



/* interface ABI::Microsoft::SDKSamples::Kitchen::IBreadBakedEventArgs */
/* [uuid][object] */ 


EXTERN_C const IID IID___x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs;

#if defined(__cplusplus) && !defined(CINTERFACE)
    } /* end extern "C" */
    namespace ABI {
        namespace Microsoft {
            namespace SDKSamples {
                namespace Kitchen {
                    
                    MIDL_INTERFACE("699B1394-3CEB-4A14-AE23-EFEC518B088B")
                    IBreadBakedEventArgs : public IInspectable
                    {
                    public:
                        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_Bread( 
                            /* [out][retval] */ __RPC__deref_out_opt ABI::Microsoft::SDKSamples::Kitchen::IBread **value) = 0;
                        
                    };

                    extern const __declspec(selectany) IID & IID_IBreadBakedEventArgs = __uuidof(IBreadBakedEventArgs);

                    
                }  /* end namespace */
            }  /* end namespace */
        }  /* end namespace */
    }  /* end namespace */
    extern "C" { 
    
#else 	/* C style interface */

    typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIids )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This,
            /* [out] */ __RPC__out ULONG *iidCount,
            /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids);
        
        HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This,
            /* [out] */ __RPC__deref_out_opt HSTRING *className);
        
        HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This,
            /* [out] */ __RPC__out TrustLevel *trustLevel);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_Bread )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs * This,
            /* [out][retval] */ __RPC__deref_out_opt __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBread **value);
        
        END_INTERFACE
    } __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgsVtbl;

    interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs
    {
        CONST_VTBL struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_GetIids(This,iidCount,iids)	\
    ( (This)->lpVtbl -> GetIids(This,iidCount,iids) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_GetRuntimeClassName(This,className)	\
    ( (This)->lpVtbl -> GetRuntimeClassName(This,className) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_GetTrustLevel(This,trustLevel)	\
    ( (This)->lpVtbl -> GetTrustLevel(This,trustLevel) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_get_Bread(This,value)	\
    ( (This)->lpVtbl -> get_Bread(This,value) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0329 */




/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0329 */




extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0329_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen2Eidl_0000_0329_v0_0_s_ifspec;

/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0005 */
/* [local] */ 

#ifndef DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs
#define DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs
#if !defined(__cplusplus) || defined(RO_NO_TEMPLATE_NAME)


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0005 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0005_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0005_v0_0_s_ifspec;

#ifndef ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_INTERFACE_DEFINED__
#define ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_INTERFACE_DEFINED__

/* interface __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs */
/* [unique][uuid][object] */ 



/* interface __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs */
/* [unique][uuid][object] */ 


EXTERN_C const IID IID___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("6582b851-18cc-5835-8c50-d896eedda300")
    __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Invoke( 
            /* [in] */ __RPC__in_opt ABI::Microsoft::SDKSamples::Kitchen::IOven *sender,
            /* [in] */ __RPC__in_opt ABI::Microsoft::SDKSamples::Kitchen::IBreadBakedEventArgs *e) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs * This);
        
        HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            __RPC__in __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs * This,
            /* [in] */ __RPC__in_opt __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven *sender,
            /* [in] */ __RPC__in_opt __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIBreadBakedEventArgs *e);
        
        END_INTERFACE
    } __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgsVtbl;

    interface __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs
    {
        CONST_VTBL struct __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_Invoke(This,sender,e)	\
    ( (This)->lpVtbl -> Invoke(This,sender,e) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0006 */
/* [local] */ 

#endif /* pinterface */
#endif /* DEF___FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs */
#if !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_INTERFACE_DEFINED__)
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Microsoft_SDKSamples_Kitchen_IOven[] = L"Microsoft.SDKSamples.Kitchen.IOven";
#endif /* !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_INTERFACE_DEFINED__) */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0006 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0006_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0006_v0_0_s_ifspec;

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_INTERFACE_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_INTERFACE_DEFINED__

/* interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven */
/* [uuid][object] */ 



/* interface ABI::Microsoft::SDKSamples::Kitchen::IOven */
/* [uuid][object] */ 


EXTERN_C const IID IID___x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven;

#if defined(__cplusplus) && !defined(CINTERFACE)
    } /* end extern "C" */
    namespace ABI {
        namespace Microsoft {
            namespace SDKSamples {
                namespace Kitchen {
                    
                    MIDL_INTERFACE("6A112353-4F87-4460-A908-2944E92686F3")
                    IOven : public IInspectable
                    {
                    public:
                        virtual HRESULT STDMETHODCALLTYPE ConfigurePreheatTemperature( 
                            /* [in] */ ABI::Microsoft::SDKSamples::Kitchen::OvenTemperature temperature) = 0;
                        
                        virtual HRESULT STDMETHODCALLTYPE BakeBread( 
                            /* [in] */ __RPC__in HSTRING flavor) = 0;
                        
                        virtual HRESULT STDMETHODCALLTYPE add_BreadBaked( 
                            /* [in] */ __RPC__in_opt __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs *handler,
                            /* [out][retval] */ __RPC__out EventRegistrationToken *token) = 0;
                        
                        virtual HRESULT STDMETHODCALLTYPE remove_BreadBaked( 
                            /* [in] */ EventRegistrationToken token) = 0;
                        
                    };

                    extern const __declspec(selectany) IID & IID_IOven = __uuidof(IOven);

                    
                }  /* end namespace */
            }  /* end namespace */
        }  /* end namespace */
    }  /* end namespace */
    extern "C" { 
    
#else 	/* C style interface */

    typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIids )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [out] */ __RPC__out ULONG *iidCount,
            /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids);
        
        HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [out] */ __RPC__deref_out_opt HSTRING *className);
        
        HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [out] */ __RPC__out TrustLevel *trustLevel);
        
        HRESULT ( STDMETHODCALLTYPE *ConfigurePreheatTemperature )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [in] */ __x_ABI_CMicrosoft_CSDKSamples_CKitchen_COvenTemperature temperature);
        
        HRESULT ( STDMETHODCALLTYPE *BakeBread )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [in] */ __RPC__in HSTRING flavor);
        
        HRESULT ( STDMETHODCALLTYPE *add_BreadBaked )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [in] */ __RPC__in_opt __FITypedEventHandler_2_Microsoft__CSDKSamples__CKitchen__COven_Microsoft__CSDKSamples__CKitchen__CBreadBakedEventArgs *handler,
            /* [out][retval] */ __RPC__out EventRegistrationToken *token);
        
        HRESULT ( STDMETHODCALLTYPE *remove_BreadBaked )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven * This,
            /* [in] */ EventRegistrationToken token);
        
        END_INTERFACE
    } __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenVtbl;

    interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven
    {
        CONST_VTBL struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_GetIids(This,iidCount,iids)	\
    ( (This)->lpVtbl -> GetIids(This,iidCount,iids) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_GetRuntimeClassName(This,className)	\
    ( (This)->lpVtbl -> GetRuntimeClassName(This,className) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_GetTrustLevel(This,trustLevel)	\
    ( (This)->lpVtbl -> GetTrustLevel(This,trustLevel) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_ConfigurePreheatTemperature(This,temperature)	\
    ( (This)->lpVtbl -> ConfigurePreheatTemperature(This,temperature) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_BakeBread(This,flavor)	\
    ( (This)->lpVtbl -> BakeBread(This,flavor) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_add_BreadBaked(This,handler,token)	\
    ( (This)->lpVtbl -> add_BreadBaked(This,handler,token) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_remove_BreadBaked(This,token)	\
    ( (This)->lpVtbl -> remove_BreadBaked(This,token) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0007 */
/* [local] */ 

#if !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_INTERFACE_DEFINED__)
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Microsoft_SDKSamples_Kitchen_IOvenFactory[] = L"Microsoft.SDKSamples.Kitchen.IOvenFactory";
#endif /* !defined(____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_INTERFACE_DEFINED__) */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0007 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0007_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0007_v0_0_s_ifspec;

#ifndef ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_INTERFACE_DEFINED__
#define ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_INTERFACE_DEFINED__

/* interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory */
/* [uuid][object] */ 



/* interface ABI::Microsoft::SDKSamples::Kitchen::IOvenFactory */
/* [uuid][object] */ 


EXTERN_C const IID IID___x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory;

#if defined(__cplusplus) && !defined(CINTERFACE)
    } /* end extern "C" */
    namespace ABI {
        namespace Microsoft {
            namespace SDKSamples {
                namespace Kitchen {
                    
                    MIDL_INTERFACE("F36C2A21-F4E3-4C39-94CE-EB9190AFDAB3")
                    IOvenFactory : public IInspectable
                    {
                    public:
                        virtual HRESULT STDMETHODCALLTYPE CreateOven( 
                            /* [in] */ ABI::Microsoft::SDKSamples::Kitchen::Dimensions dimensions,
                            /* [out][retval] */ __RPC__deref_out_opt ABI::Microsoft::SDKSamples::Kitchen::IOven **result) = 0;
                        
                    };

                    extern const __declspec(selectany) IID & IID_IOvenFactory = __uuidof(IOvenFactory);

                    
                }  /* end namespace */
            }  /* end namespace */
        }  /* end namespace */
    }  /* end namespace */
    extern "C" { 
    
#else 	/* C style interface */

    typedef struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactoryVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIids )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This,
            /* [out] */ __RPC__out ULONG *iidCount,
            /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids);
        
        HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This,
            /* [out] */ __RPC__deref_out_opt HSTRING *className);
        
        HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This,
            /* [out] */ __RPC__out TrustLevel *trustLevel);
        
        HRESULT ( STDMETHODCALLTYPE *CreateOven )( 
            __RPC__in __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory * This,
            /* [in] */ __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CDimensions dimensions,
            /* [out][retval] */ __RPC__deref_out_opt __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOven **result);
        
        END_INTERFACE
    } __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactoryVtbl;

    interface __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory
    {
        CONST_VTBL struct __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactoryVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_GetIids(This,iidCount,iids)	\
    ( (This)->lpVtbl -> GetIids(This,iidCount,iids) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_GetRuntimeClassName(This,className)	\
    ( (This)->lpVtbl -> GetRuntimeClassName(This,className) ) 

#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_GetTrustLevel(This,trustLevel)	\
    ( (This)->lpVtbl -> GetTrustLevel(This,trustLevel) ) 


#define __x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_CreateOven(This,dimensions,result)	\
    ( (This)->lpVtbl -> CreateOven(This,dimensions,result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* ____x_ABI_CMicrosoft_CSDKSamples_CKitchen_CIOvenFactory_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0008 */
/* [local] */ 

#ifndef RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_Bread_DEFINED
#define RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_Bread_DEFINED
extern const __declspec(selectany) _Null_terminated_ WCHAR RuntimeClass_Microsoft_SDKSamples_Kitchen_Bread[] = L"Microsoft.SDKSamples.Kitchen.Bread";
#endif
#ifndef RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_BreadBakedEventArgs_DEFINED
#define RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_BreadBakedEventArgs_DEFINED
extern const __declspec(selectany) _Null_terminated_ WCHAR RuntimeClass_Microsoft_SDKSamples_Kitchen_BreadBakedEventArgs[] = L"Microsoft.SDKSamples.Kitchen.BreadBakedEventArgs";
#endif
#ifndef RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_Oven_DEFINED
#define RUNTIMECLASS_Microsoft_SDKSamples_Kitchen_Oven_DEFINED
extern const __declspec(selectany) _Null_terminated_ WCHAR RuntimeClass_Microsoft_SDKSamples_Kitchen_Oven[] = L"Microsoft.SDKSamples.Kitchen.Oven";
#endif


/* interface __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0008 */
/* [local] */ 



extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0008_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_Microsoft2ESDKSamples2EKitchen_0000_0008_v0_0_s_ifspec;

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  HSTRING_UserSize(     __RPC__in unsigned long *, unsigned long            , __RPC__in HSTRING * ); 
unsigned char * __RPC_USER  HSTRING_UserMarshal(  __RPC__in unsigned long *, __RPC__inout_xcount(0) unsigned char *, __RPC__in HSTRING * ); 
unsigned char * __RPC_USER  HSTRING_UserUnmarshal(__RPC__in unsigned long *, __RPC__in_xcount(0) unsigned char *, __RPC__out HSTRING * ); 
void                      __RPC_USER  HSTRING_UserFree(     __RPC__in unsigned long *, __RPC__in HSTRING * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif



