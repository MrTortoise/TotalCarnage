Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports System.Xml.Linq
Imports CommonObjects


Public Class TextureAnimationLoader
    Implements ILoader

    Public docAnimations As XDocument
    Public mTextureAnimations As TextureAnimationList
    Protected mGeneralTextures As GeneralTextureList

    Public Sub New(ByVal theFile As String, ByVal theGeneralTextureList As GeneralTextureList)

        mGeneralTextures = theGeneralTextureList
        mTextureAnimations = New TextureAnimationList()
        Load(theFile)
        Populate()

    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Tiles/TileLayers.xml"
        Else
            theFile = CurDir() & "/Tiles/" & theFile & ".xml"
        End If
        docAnimations = XDocument.Load(theFile)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim theTextureAnimation As TextureAnimation

        Dim mTextureAnimationsLINQ = From ta In docAnimations...<textureAnimation> _
                                     Select mid = CInt(ta.<id>.Value), mName = ta.<name>.Value, _
                                        mNoFrames = CInt(ta.<noFrames>.Value), mNoLoops = CInt(ta.<noLoops>.Value), _
                                        mGenTexID = CInt(ta.<GeneralTextureID>.Value), _
                                        tAnimation = ta.<animation>


        Dim id As Integer
        Dim name As String
        Dim noFrames As Integer
        Dim noLoops As Integer

        Dim GenTexID As Integer

        Dim coords As List(Of Vector2)

        For Each ta In mTextureAnimationsLINQ
            id = ta.mid
            name = ta.mName
            noFrames = ta.mNoFrames
            noLoops = ta.mNoLoops

            GenTexID = ta.mGenTexID

            coords = New List(Of Vector2)()

            Dim mAnimation = From an In ta.tAnimation...<item> _
                             Select x = CInt(an.<x>.Value), y = CInt(an.<y>.Value)

            For Each an In mAnimation
                coords.Add(New Vector2(an.x, an.y))
            Next
            If (mGeneralTextures.Contains(GenTexID) = False) Then
                Throw New Exception("tried to add texture animations that have no corresponding generalTexture")
            End If
            theTextureAnimation = New TextureAnimation(name, id, mGeneralTextures.GetByID(GenTexID), noLoops, coords)

            mTextureAnimations.AddAnimation(theTextureAnimation)
        Next

    End Sub





End Class
