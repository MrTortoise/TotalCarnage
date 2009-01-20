Imports System.Xml.Linq
Imports CommonObjects

Public Class MapTileLoader
    Implements ILoader

    Public docMapTiles As XDocument
    Protected mTextureAnimations As TextureAnimationList
    Public mMapTiles As MapTileList


    Public Sub New(ByVal theFile As String, ByVal theTextureAnimationList As TextureAnimationList)

        mTextureAnimations = theTextureAnimationList
        mMapTiles = New MapTileList
        Load(theFile)
        Populate()

    End Sub

    Public Sub Load(ByVal filePath As String) Implements ILoader.Load
        If filePath = "" Then
            filePath = CurDir() & "/Tiles/TileLayers.xml"
        Else
            filePath = CurDir() & "/Tiles/" & filePath & ".xml"
        End If
        docMapTiles = XDocument.Load(filePath)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim mMapTiles As MapTile

        Dim mapTilesLINQ = From mt In docMapTiles...<ti

    End Sub
End Class
