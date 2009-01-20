Imports System.Xml.Linq
Imports CommonObjects
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics

Public Class DetailedTextureLoader
    Implements ILoader

    Public docDetailTextures As XDocument
    Public mDetailTextureList As DetailedTextureList

    Protected mGeneralTextureList As GeneralTextureList

    'Public Sub New(ByVal ID As Integer, ByVal Name As String, ByVal theFile As String, ByVal theGeneralTextureList As GeneralTextureList)
    Public Sub New(ByVal theFile As String, ByVal theGeneralTextureList As GeneralTextureList)

        mGeneralTextureList = theGeneralTextureList
        'mDetailTextureList = New DetailedTextureList(ID, Name)
        mDetailTextureList = New DetailedTextureList()
        Load(theFile)
        Populate()

    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Textures/DetailedTextures.xml"
        Else
            theFile = CurDir() & "/Textures/" & theFile & ".xml"
        End If
        docDetailTextures = XDocument.Load(theFile)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim theTexture As DetailedTexture
        Dim mTextures = From dt In docDetailTextures...<detailedtexture> _
                        Select mID = CInt(dt.<id>.Value), _
                        mName = dt.<name>.Value, _
                        mTextureID = CInt(dt.<textureIndex>.Value), _
                        mAlpha = CInt(dt.<alpha>.Value), _
                        mRed = CInt(dt.<red>.Value), _
                        mGreen = CInt(dt.<green>.Value), _
                        mBlue = CInt(dt.<blue>.Value)
        Dim alpha As Int16 = 0
        Dim red As Int16 = 0
        Dim green As Int16 = 0
        Dim blue As Int16 = 0

        For Each dt In mTextures
            alpha = dt.mAlpha
            red = dt.mRed
            green = dt.mGreen
            blue = dt.mBlue

            If alpha > 255 Then
                alpha = 255
            ElseIf alpha < 0 Then
                alpha = 0
            End If

            If red > 255 Then
                red = 255
            ElseIf red < 0 Then
                red = 0
            End If

            If green > 255 Then
                green = 255
            ElseIf green < 0 Then
                green = 0
            End If

            If blue > 255 Then
                blue = 255
            ElseIf blue < 0 Then
                blue = 0
            End If

            theTexture = New DetailedTexture(dt.mID, dt.mName, mGeneralTextureList(dt.mTextureID), New Color(CByte(red), CByte(green), CByte(blue), CByte(alpha)))
            mDetailTextureList.Add(theTexture)
        Next
    End Sub


End Class
