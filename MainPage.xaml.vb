' https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Sub BtnFullScreen_Click(sender As Object, e As RoutedEventArgs) Handles BtnFullScreen.Click
        With ApplicationView.GetForCurrentView
            If .IsFullScreenMode Then
                .ExitFullScreenMode()
                IcnFullScreen.Glyph = ChrW(Symbol.FullScreen)
            Else
                .TryEnterFullScreenMode()
                IcnFullScreen.Glyph = ChrW(Symbol.BackToWindow)
            End If
        End With
    End Sub

End Class
