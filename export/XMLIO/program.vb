Imports CommonObjects


Public Module program

	Sub Main()
		Console.WriteLine("woot")

		Dim gtl As New GeneralTextureList()

		Dim gtll As New GeneralTextureListLoader()

		gtl = gtll.GeneralTextureList
		Console.WriteLine(gtl.ToString())

		Dim tal As New TextureAnimationList()
		Dim tall As New TextureAnimationLoader("", gtl)

		tal = tall.TextureAnimationList

		Console.WriteLine(tal.ToString())





	End Sub

End Module

