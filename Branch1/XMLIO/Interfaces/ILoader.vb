Imports System.Text

Public Interface ILoader
	Sub Populate()
	Sub Load(ByVal filePath As String)
	'Function ValidateAgainstSchema(ByVal filePath As String) As Boolean

	'ReadOnly Property Messagelog() As StringBuilder
	'ReadOnly Property IsValid() As Boolean
End Interface
