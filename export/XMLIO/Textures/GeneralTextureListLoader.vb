Imports System.Text
Imports System.Xml.Linq
Imports System.Xml
Imports System.Xml.Schema
Imports CommonObjects
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports System.Resources


Public Class GeneralTextureListLoader
	Implements ILoader


	Public docTextures As XDocument
	Public mGeneralTextureList As GeneralTextureList
	Public mGraphicsDevice As GraphicsDevice

	Protected mMessageLog As StringBuilder
	Protected mValid As Boolean



	Public Sub New(Optional ByVal theFile As String = "")

		mGeneralTextureList = New GeneralTextureList()
		'mGraphicsDevice = GraphicsDev
		'	ValidateAgainstSchema(theFile)
		Load(theFile)
		Populate()
	End Sub

	Public Sub Load(ByVal theFile As String) Implements ILoader.Load
		If theFile = "" Then
			docTextures = XDocument.Parse(My.Resources.GeneralTexturesXML)
		Else
			theFile = CurDir() & "/Textures/" & theFile & ".xml"
			docTextures = XDocument.Load(theFile)
		End If



	End Sub


	Public Sub Populate() Implements ILoader.Populate
		Dim theTexture As GeneralTexture

		Dim mTextures = From tt In docTextures...<texture> Select _
		 mID = CInt(tt.<id>.Value), mName = tt.<name>.Value, mPath = tt.<path>.Value, mNoColumns = tt.<noColumns>.Value, mNoRows = tt.<noRows>.Value

		For Each tt In mTextures
			theTexture = New GeneralTexture(tt.mID, tt.mName, CurDir() & tt.mPath, CType(tt.mNoRows, Integer), CType(tt.mNoColumns, Integer))
			mGeneralTextureList.Add(theTexture)
		Next

		'stopped this from loading the texxtures
		'  mGeneralTextureList.LoadTextures(mGraphicsDevice)

	End Sub


	Public ReadOnly Property Messagelog() As StringBuilder Implements ILoader.Messagelog
		Get
			Return mMessageLog
		End Get
	End Property

	Public ReadOnly Property IsValid() As Boolean Implements ILoader.IsValid
		Get
			Return mValid
		End Get
	End Property

	Public Function ValidateAgainstSchema(ByVal filePath As String) As Boolean Implements ILoader.ValidateAgainstSchema
		Dim schemaPath As String = CurDir() & "/textures/GeneralTextures.xsd"
		Dim document As String

		Dim settings As New XmlReaderSettings()
		settings.Schemas.Add("http://www.totalcarnage.org/GeneralTextureList", schemaPath)
		AddHandler settings.ValidationEventHandler, AddressOf ValidationCallback

		If filePath = "" Then
			document=
		Else

		End If
	End Function

	Private Sub ValidationCallback(ByVal sender As Object, ByVal args As  _
	   ValidationEventArgs)

		mValid = False
		Messagelog.AppendLine(args.Message)

	End Sub
End Class
