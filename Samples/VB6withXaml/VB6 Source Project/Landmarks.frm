VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Landmarks"
   ClientHeight    =   3525
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   3675
   LinkTopic       =   "Form1"
   ScaleHeight     =   3525
   ScaleMode       =   0  'User
   ScaleWidth      =   3675
   StartUpPosition =   3  'Windows Default
   Begin VB.PictureBox Picture1 
      Height          =   540
      Left            =   100
      Picture         =   "Landmarks.frx":0000
      ScaleHeight     =   480
      ScaleWidth      =   480
      TabIndex        =   5
      Top             =   100
      Width           =   540
   End
   Begin VB.CommandButton TowerBridgeBtn 
      Caption         =   "Tower Bridge of London"
      Height          =   495
      Left            =   800
      TabIndex        =   4
      Top             =   2300
      Width           =   2000
   End
   Begin VB.CommandButton TokyoTowerBtn 
      Caption         =   "Tokyo Tower"
      Height          =   495
      Left            =   800
      TabIndex        =   3
      Top             =   1750
      Width           =   2000
   End
   Begin VB.CommandButton StatueOfLibertyBtn 
      Caption         =   "Statue of Liberty"
      Height          =   495
      Left            =   800
      TabIndex        =   2
      Top             =   2850
      Width           =   2000
   End
   Begin VB.CommandButton SydneyOperaBtn 
      Caption         =   "Sydney Opera"
      Height          =   495
      Left            =   800
      TabIndex        =   1
      Top             =   1180
      Width           =   2000
   End
   Begin VB.CommandButton SpaceNeedleBtn 
      Caption         =   "Seattle Space Needle"
      Height          =   495
      Left            =   800
      TabIndex        =   0
      Top             =   650
      Width           =   2000
   End
   Begin VB.Label Label1 
      Caption         =   "Landmarks (VB6)"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   840
      TabIndex        =   6
      Top             =   240
      Width           =   2055
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'
' The LaunchMap function maps to the UWP API for LaunchUriAsyn
' for launching the XAML component that hosts the map control
'
Private Declare Function LaunchMap Lib "UWPWrappers.dll" _
  (ByVal lat As Double, ByVal lon As Double) As Boolean
  
'
' The CreateToast function maps to the UWP API for creating
' toast notifications with an adaptive template
'
Private Declare Function CreateToast Lib "UWPWrappers.dll" _
  (ByVal index As Integer) As Boolean

Private Sub StatueOfLibertyBtn_Click()
    LaunchMap 40.689167, -74.044444
    CreateToast 2
End Sub

Private Sub SpaceNeedleBtn_Click()
    LaunchMap 47.6204, -122.3491
    CreateToast 3
End Sub

Private Sub SydneyOperaBtn_Click()
    LaunchMap -33.858667, 151.214028
    CreateToast 1
End Sub

Private Sub TokyoTowerBtn_Click()
    LaunchMap 35.658611, 139.745556
    CreateToast 4
End Sub

Private Sub TowerBridgeBtn_Click()
    LaunchMap 51.505556, -0.075556
    CreateToast 0
End Sub
