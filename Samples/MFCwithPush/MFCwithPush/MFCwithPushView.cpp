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

// MFCwithPushView.cpp : implementation of the CMFCwithPushView class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "MFCwithPush.h"
#endif

#include "MFCwithPushDoc.h"
#include "MFCwithPushView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CMFCwithPushView

IMPLEMENT_DYNCREATE(CMFCwithPushView, CView)

BEGIN_MESSAGE_MAP(CMFCwithPushView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CMFCwithPushView::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// CMFCwithPushView construction/destruction

CMFCwithPushView::CMFCwithPushView()
{
	// TODO: add construction code here

}

CMFCwithPushView::~CMFCwithPushView()
{
}

BOOL CMFCwithPushView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CMFCwithPushView drawing

void CMFCwithPushView::OnDraw(CDC* /*pDC*/)
{
	CMFCwithPushDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// CMFCwithPushView printing


void CMFCwithPushView::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL CMFCwithPushView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CMFCwithPushView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CMFCwithPushView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CMFCwithPushView::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CMFCwithPushView::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// CMFCwithPushView diagnostics

#ifdef _DEBUG
void CMFCwithPushView::AssertValid() const
{
	CView::AssertValid();
}

void CMFCwithPushView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CMFCwithPushDoc* CMFCwithPushView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CMFCwithPushDoc)));
	return (CMFCwithPushDoc*)m_pDocument;
}
#endif //_DEBUG


// CMFCwithPushView message handlers
