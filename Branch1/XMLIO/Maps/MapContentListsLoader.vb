Imports System.Xml.Linq
Imports CommonObjects

''' <summary>
''' Creates a list of Maps with their relevant content files
''' for use on a level select screen and then to be passed into the game engine
''' </summary>
''' <remarks></remarks>
Public Class MapContentListsLoader
    Implements ILoader

    Public docMapObjectsList As XDocument
    Public mapContentsList As ListOfMapContents

    ''' <summary>
    ''' takes a map list file path
    ''' </summary>
    ''' <param name="theFile"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal theFile As String)
        mapContentsList = New ListOfMapContents()
        Load(theFile)
        Populate()
    End Sub


    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Maps/Map.xml"
        Else
            theFile = CurDir() & "/Maps/" & theFile & ".xml"
        End If
        docMapObjectsList = XDocument.Load(theFile)
    End Sub

    ''' <summary>
    ''' populates the mapContentsList object from the specified file
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Populate() Implements ILoader.Populate
        Dim mMapContents As New MapContents

        Dim mMapObjects = From mo In docMapObjectsList...<map> _
                   Select mID = CInt(mo.<id>.Value), mName = mo.<name>.Value, _
                   mGenTex = mo.<generalTextureList>.Value, _
                   mDetTex = mo.<detailedTextureList>.Value, _
                   mTerTyp = mo.<terrainTypeList>.Value, _
                   mGenTil = mo.<generalTileList>.Value, _
                   mDetTil = mo.<detailedTileList>.Value, _
                   mGenLay = mo.<tileLayers>.Value

        For Each mo In mMapObjects
            mMapContents.MapID = mo.mID
            mMapContents.MapName = mo.mName
            mMapContents.GeneralTextures = mo.mGenTex
            mMapContents.DetailedTextures = mo.mDetTex
            mMapContents.TerrainTypes = mo.mTerTyp
            mMapContents.GeneralTiles = mo.mGenTil
            mMapContents.DetailedTiles = mo.mDetTil
            mMapContents.TileLayers = mo.mGenLay

            mapContentsList.Add(mMapContents)

        Next
    End Sub
End Class
