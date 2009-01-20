Imports System.Xml.Linq
Imports CommonObjects


Public Class TerrainTypeLoader
    Implements ILoader

    Public docTerrainTypes As XDocument
    Public mTerrainTypeList As TerrainTypeList

    Public Sub New(ByVal theFile As String)

        mTerrainTypeList = New TerrainTypeList()
        Load(theFile)
        Populate()
    End Sub

    Public Sub Load(ByVal theFile As String) Implements ILoader.Load
        If theFile = "" Then
            theFile = CurDir() & "/TerrainTypes/TerrainTypes.xml"
        Else
            theFile = CurDir() & "/TerrainTypes/" & theFile & ".xml"
        End If
        docTerrainTypes = XDocument.Load(theFile)
    End Sub


    Public Sub Populate() Implements ILoader.Populate
        Dim theType As TerrainType
        Dim mTerrainTypes = From tt In docTerrainTypes...<TerrainType> _
                            Select mID = CInt(tt.<id>.Value), mName = tt.<name>.Value, mPassable = CBool(tt.<passable>.Value), mMoveModifier = CSng(tt.<moveModifier>.Value)
        For Each tt In mTerrainTypes
            theType = New TerrainType(tt.mID, tt.mName, tt.mPassable, tt.mMoveModifier)
            mTerrainTypeList.AddTerrainType(theType)
        Next
    End Sub


End Class



