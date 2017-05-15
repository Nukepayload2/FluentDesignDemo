Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Public Class HostBlurBackground
    Inherits DependencyObject

    Shared sprite As SpriteVisual
    Shared WithEvents HostBackdrop As Panel

    Public Shared Function GetIsEnabled(element As DependencyObject) As Boolean
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If
        Return element.GetValue(IsEnabledProperty)
    End Function

    Public Shared Sub SetIsEnabled(element As DependencyObject, value As Boolean)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If
        element.SetValue(IsEnabledProperty, value)
    End Sub

    Public Shared ReadOnly IsEnabledProperty As _
                           DependencyProperty = DependencyProperty.RegisterAttached("IsEnabled",
                           GetType(Boolean), GetType(Panel),
                           New PropertyMetadata(False,
                                                Sub(s, e)
                                                    HostBackdrop = s
                                                    If sprite Is Nothing Then
                                                        sprite = HostBackdropBlurHelper.CreateBackdropSpriteVisual(HostBackdrop)
                                                    End If
                                                    If Not e.NewValue Then
                                                        ElementCompositionPreview.SetElementChildVisual(HostBackdrop, Nothing)
                                                    End If
                                                End Sub))

    Public Shared Function GetThemeColor(ByVal element As DependencyObject) As Color
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        Return element.GetValue(ThemeColorProperty)
    End Function

    Public Shared Sub SetThemeColor(ByVal element As DependencyObject, ByVal value As Color)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        element.SetValue(ThemeColorProperty, value)
    End Sub

    Private Shared Sub HostBackdrop_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles HostBackdrop.SizeChanged
        If sprite IsNot Nothing Then
            sprite.Size = New Numerics.Vector2(HostBackdrop.ActualWidth, HostBackdrop.ActualHeight)
        End If
    End Sub

    Public Shared ReadOnly ThemeColorProperty As _
                           DependencyProperty = DependencyProperty.RegisterAttached("ThemeColor",
                           GetType(Color), GetType(Panel),
                           New PropertyMetadata(Nothing,
                                                Sub(s, e)
                                                    Dim color = If(e.NewValue, Colors.Transparent)
                                                    HostBackdropBlurHelper.SetTitleBarColor(color)
                                                    DirectCast(s, Panel).Background = New SolidColorBrush(color)
                                                End Sub))

End Class
