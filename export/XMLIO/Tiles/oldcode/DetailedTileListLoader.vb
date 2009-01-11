Imports System.Xml.Linq
Imports CommonObjects

Public Class DetailedTileListLoader
    Implements ILoader

    Public docDetailedTileList As XDocument
    Public mDetailedTileList As DetailedTileList
    Public mGeneralTiles As GeneralTileList

    Public Sub New(ByVal theFile As String, ByVal theGeneralTiles As GeneralTileList)

        mGeneralTiles = theGeneralTiles
        mDetailedTileList = New DetailedTileList()
        Load(theFile)
        Populate()
    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/Tiles/DetailedTile.xml"
        Else
            theFile = CurDir() & "/Tiles/" & theFile & ".xml"
        End If
        docDetailedTileList = XDocument.Load(theFile)
    End Sub

    Public Sub Populate() Implements ILoader.Populate
        Dim theDetailedTile As DetailedTile

        Dim mTiles = From gt In docDetailedTileList...<detailedTile> _
                    Select Mid = CInt(gt.<id>.Value), mName = gt.<name>.Value, mGeneralTile = CInt(gt.<generalTile>.Value)

        For Each gt In mTiles
            theDetailedTile = New DetailedTile(gt.Mid, gt.mName, mGeneralTiles.Get(gt.mGeneralTile))
            mDetailedTileList.Add(theDetailedTile)
        Next
    End Sub


End Class
