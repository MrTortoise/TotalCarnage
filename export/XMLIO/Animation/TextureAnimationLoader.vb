Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports System.Xml.Linq
Imports CommonObjects


Public Class TextureAnimationLoader
    Implements ILoader

	Protected docAnimations As XDocument
	Protected mTextureAnimations As TextureAnimationList
    Protected mGeneralTextures As GeneralTextureList

    Public Sub New(ByVal theFile As String, ByVal theGeneralTextureList As GeneralTextureList)

        mGeneralTextures = theGeneralTextureList
        mTextureAnimations = New TextureAnimationList()
        Load(theFile)
        Populate()

	End Sub

	Public ReadOnly Property TextureAnimationList() As TextureAnimationList
		Get
			Return mTextureAnimations
		End Get
	End Property

	Protected Sub Load(ByVal theFile As String) Implements ILoader.Load
		If theFile = "" Then
			theFile = CurDir() & "/Animation/TextureAnimations.xml"
		Else
			theFile = CurDir() & "/Animation/" & theFile & ".xml"
		End If
		docAnimations = XDocument.Load(theFile)
	End Sub

	Protected Sub Populate() Implements ILoader.Populate
		Dim theTextureAnimation As TextureAnimation

		Dim mTextureAnimationsLINQ = From ta In docAnimations...<animation> _
		  Select mid = CInt(ta.<id>.Value), _
		  mName = ta.<name>.Value, _
		  mNoFrames = CInt(ta.<noFrames>.Value), _
		  mNoLoops = CInt(ta.<noLoops>.Value), _
		  mUpdatePeriod = CInt(ta.<noFrames>.Value), _
		  mGenTexID = CInt(ta.<texture>.Value), _
		  tAnimation = ta.<sequence>


		Dim id As Integer
		Dim name As String
		Dim noFrames As Integer
		Dim noLoops As Integer
		Dim updatePeriod As Integer

		Dim GenTexID As Integer

		Dim coords As List(Of Vector2)

		For Each ta In mTextureAnimationsLINQ
			id = ta.mid
			name = ta.mName
			noFrames = ta.mNoFrames
			noLoops = ta.mNoLoops
			updatePeriod = ta.mUpdatePeriod

			GenTexID = ta.mGenTexID

			coords = New List(Of Vector2)()

			Dim mAnimation = From an In ta.tAnimation...<frame> _
			  Select x = CInt(an.<x>.Value), y = CInt(an.<y>.Value)


			For Each an In mAnimation
				coords.Add(New Vector2(an.x, an.y))
			Next
			If (mGeneralTextures.Contains((GenTexID) = False)) Then
				Throw New Exception("tried to add texture animations that have no corresponding generalTexture")
			End If
			theTextureAnimation = New TextureAnimation(name, id, mGeneralTextures.GetByID(GenTexID), noLoops, New TimeSpan(0, 0, 0, 0, updatePeriod), coords)

			mTextureAnimations.AddAnimation(theTextureAnimation)
		Next

	End Sub


End Class
