Imports System.Xml.Linq
Imports CommonObjects

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics


Public Class MapObjectsLoader
    Implements ILoader

    Public mapObjects As MapSubObjects
    Public docMapObjects As XDocument
    Public MapID As Integer

    Protected MapContent As MapContents

    Protected mapContentsList As ListOfMapContents

    ''' <summary>
    ''' te path doesnt need .xml after it
    ''' this xml file contains a list of xml files used to buil dthe map
    ''' </summary>
    ''' <param name="MapPath"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal MapPath As String, ByVal theMapID As Integer)


        mapObjects = New MapSubObjects()
        mapContentsList = New ListOfMapContents()
        MapID = theMapID
        Load(MapPath)
        Populate()

    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        'the file parsing is taken care of in the mapcontentsList Loader object

        ' If theFile = "" Then
        'theFile = CurDir() & "/Maps/Map.xml"
        '  Else
        '  theFile = CurDir() & "/Maps/" & theFile & ".xml"
        '  End If
        'docMapObjects = XDocument.Load(theFile)

        Dim mapContentLoader As New MapContentListsLoader(theFile)
        MapContent = New MapContents()

        With mapContentLoader.mapContentsList(MapID)
            MapContent.MapID = .MapID
            MapContent.MapName = .MapName
            MapContent.GeneralTextures = .GeneralTextures
            MapContent.DetailedTextures = .DetailedTextures
            MapContent.GeneralTiles = .GeneralTiles
            MapContent.DetailedTiles = .DetailedTiles
            MapContent.TerrainTypes = .TerrainTypes
            MapContent.TileLayers = .TileLayers
        End With


    End Sub

    ''' <summary>
    ''' Populates the MapObjectsObject with the map ofbjects int he file specified in the constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Populate() Implements ILoader.Populate

        Dim terTypes As New TerrainTypeList()
        Dim terrTypeLoad As New TerrainTypeLoader(MapContent.TerrainTypes)
        mapObjects.mTerrainTypeList = terrTypeLoad.mTerrainTypeList

        Dim gentex As New GeneralTextureList()
		Dim gentexLoad As New GeneralTextureListLoader(MapContent.GeneralTextures)
        mapObjects.mgeneralTextureList = gentexLoad.mGeneralTextureList

        Dim detTexLoad As New DetailedTextureLoader(MapContent.DetailedTextures, mapObjects.mgeneralTextureList)
        mapObjects.mDetailedTextureList = detTexLoad.mDetailTextureList

        Dim genTileLoad As New generalTileListLoader(MapContent.GeneralTiles, mapObjects.mDetailedTextureList, mapObjects.mTerrainTypeList)
        mapObjects.mGeneralTileList = genTileLoad.mGeneralTileList

        Dim detTileLoad As New DetailedTileListLoader(MapContent.DetailedTiles, mapObjects.mGeneralTileList)
        mapObjects.mDetailedTileList = detTileLoad.mDetailedTileList

        Dim genLayerLoad As New TileLayersLoader(MapContent.TileLayers, mapObjects.mDetailedTileList)
        mapObjects.mTileLayerList = genLayerLoad.mTileLayersList



    End Sub



End Class
