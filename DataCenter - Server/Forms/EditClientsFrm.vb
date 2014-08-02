Public Class EditClientsFrm
    Private Int1 As Int16
    Private ErrorProvider As String = Nothing
    Private Sub ReloadClients()
        CBCon.Items.Clear()
        ErrorProvider = Nothing
        ErrorProvider = FunLib.GetAllClients(True)
        If ErrorProvider <> Nothing Then
            MsgBox_(ErrorProvider, 0, 23, False, Me)
            Me.Close()
        ElseIf FunLib.AllClients IsNot Nothing Then
            For Int1 = 0 To FunLib.AllClients.Length - 1
                CBCon.Items.Add(FunLib.AllClients(Int1).ClientName)
            Next
        Else
            MsgBox_("�� ���� �����", 128, 23, False, Me)
            Me.Close()
        End If
    End Sub
    Private Sub ResetForm()
        TxtAddress.Clear()
        Txtmail1.Clear()
        Txtmail2.Clear()
        TxtMob1.Clear()
        TxtMob2.Clear()
        TxtName.Clear()
        Txtpho1.Clear()
        TxtPho2.Clear()
        BtnSave.Enabled = False
        BtnDeactivate.Enabled = False
        CBCon.SelectedIndex = -1
    End Sub
    Private Sub EditContractorsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReloadClients()
    End Sub
    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtName.TextChanged
        If TxtName.Text.Trim.Length = 0 Then
            BtnSave.Enabled = False
        ElseIf TxtName.Text.Trim.Length > 0 And CBCon.SelectedIndex <> -1 Then
            BtnSave.Enabled = True
        End If
    End Sub
    Private Sub CBCon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCon.SelectedIndexChanged
        If CBCon.SelectedIndex <> -1 Then
            TxtName.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientName
            TxtAddress.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientAddress
            TxtMob1.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientMob1
            TxtMob2.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientMob2
            Txtpho1.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientPho1
            TxtPho2.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientPho2
            Txtmail1.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientMail1
            Txtmail2.Text = FunLib.AllClients(CBCon.SelectedIndex).ClientMail2
            BtnDeactivate.Enabled = True
        Else
            BtnDeactivate.Enabled = False
        End If
    End Sub
    Private Sub BtnDeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeactivate.Click
        If MsgBox("�� ��� ����� ?", MsgBoxStyle.YesNo, "WARNING") = MsgBoxResult.Yes Then
            If CBCon.SelectedIndex <> -1 Then
                ErrorProvider = Nothing
                ErrorProvider = FunLib.DisactivateClients(FunLib.AllClients(CBCon.SelectedIndex).ClientID, False)
                If ErrorProvider = Nothing Then
                    ResetForm()
                    MsgBox_("�� �������", 106, 23, True, Me)
                    ReloadClients()
                Else
                    MsgBox_("�� ��� �������", 0, 23, False, Me)
                End If
            End If
        End If
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If CBCon.SelectedIndex <> -1 And TxtName.Text.Trim.Length > 0 Then
            Me.Enabled = False
            ErrorProvider = Nothing
            ErrorProvider = FunLib.UpdateClients(FunLib.AllClients(CBCon.SelectedIndex).ClientID, TxtName.Text.Trim, TxtAddress.Text.Trim, TxtMob1.Text.Trim, TxtMob2.Text.Trim, Txtpho1.Text.Trim, TxtPho2.Text.Trim, Txtmail1.Text.Trim, Txtmail2.Text.Trim)
            If ErrorProvider = Nothing Then
                FunLib.AllClients(CBCon.SelectedIndex).ClientName = TxtName.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientAddress = TxtAddress.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientMob1 = TxtMob1.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientMob2 = TxtMob2.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientPho1 = Txtpho1.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientPho2 = TxtPho2.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientMail1 = Txtmail1.Text.Trim
                FunLib.AllClients(CBCon.SelectedIndex).ClientMail2 = Txtmail2.Text.Trim
                CBCon.Items.Item(CBCon.SelectedIndex) = TxtName.Text.Trim
                MsgBox_("�� ������", 107, 23, True, Me)
            Else
                MsgBox_("�� ��� �������", 113, 23, False, Me)
            End If
            Me.Enabled = True
        End If
    End Sub
End Class