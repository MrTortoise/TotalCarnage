
Imports System.Xml.Linq
Imports CommonObjects

Public Class generalTileListLoader
    Implements ILoader
    Public docGeneralTiles As XDocument
    Public mGeneralTileList As GeneralTileList

    Protected mDetailTextureList As DetailedTextureList
    Protected mTerrainTypeList As TerrainTypeList

    Public Sub New(ByVal theFile As String, _
                   ByVal thedetailTextures As DetailedTextureList, ByVal theTerrainTypes As TerrainTypeList)


        mDetailTextureList = thedetailTextures
        mTerrainTypeList = theTerrainTypes
        mGeneralTileList = New GeneralTileList()
        Load(theFile)
        Populate()
    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Tiles/GeneralTiles.xml"
        Else
            theFile = CurDir() & "/Tiles/" & theFile & ".xml"
        End If
        docGeneralTiles = XDocument.Load(theFile)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim theTile As GeneralTile

        Dim mTiles = From t In docGeneralTiles...<generalTile> Select _
                     mID = CInt(t.<id>.Value), _
                     mName = t.<name>.Value, _
                     mDetailTextureID = CInt(t.<detailTextureID>.Value), _
                     mTerrainType = CInt(t.<terrainTypeID>.Value)
        For Each t In mTiles
            theTile = New GeneralTile(t.mID, t.mName, mDetailTextureList.Get(t.mDetailTextureID), mTerrainTypeList.Get(t.mTerrainType))
            mGeneralTileList.Add(theTile)
        Next

    End Sub
End Class
