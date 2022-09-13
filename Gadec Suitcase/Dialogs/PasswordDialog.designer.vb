<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PasswordDialog))
        Me.ltOK = New System.Windows.Forms.Button()
        Me.ltCancel = New System.Windows.Forms.Button()
        Me.InputTextBox = New System.Windows.Forms.TextBox()
        Me.TextLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ltOK
        '
        Me.ltOK.Location = New System.Drawing.Point(190, 51)
        Me.ltOK.Name = "ltOK"
        Me.ltOK.Size = New System.Drawing.Size(86, 23)
        Me.ltOK.TabIndex = 6
        Me.ltOK.Text = "XXX"
        Me.ltOK.UseVisualStyleBackColor = True
        '
        'ltCancel
        '
        Me.ltCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltCancel.Location = New System.Drawing.Point(282, 51)
        Me.ltCancel.Name = "ltCancel"
        Me.ltCancel.Size = New System.Drawing.Size(85, 23)
        Me.ltCancel.TabIndex = 7
        Me.ltCancel.Text = "XXX"
        Me.ltCancel.UseVisualStyleBackColor = True
        '
        'InputTextBox
        '
        Me.InputTextBox.Location = New System.Drawing.Point(12, 25)
        Me.InputTextBox.Name = "InputTextBox"
        Me.InputTextBox.Size = New System.Drawing.Size(355, 20)
        Me.InputTextBox.TabIndex = 8
        '
        'TextLabel
        '
        Me.TextLabel.AutoSize = True
        Me.TextLabel.Location = New System.Drawing.Point(12, 9)
        Me.TextLabel.Name = "TextLabel"
        Me.TextLabel.Size = New System.Drawing.Size(28, 13)
        Me.TextLabel.TabIndex = 9
        Me.TextLabel.Text = "XXX"
        '
        'InputBox
        '
        Me.AcceptButton = Me.ltOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltCancel
        Me.ClientSize = New System.Drawing.Size(379, 86)
        Me.Controls.Add(Me.InputTextBox)
        Me.Controls.Add(Me.ltOK)
        Me.Controls.Add(Me.ltCancel)
        Me.Controls.Add(Me.TextLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "InputBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "XXX"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ltOK As System.Windows.Forms.Button
    Friend WithEvents ltCancel As System.Windows.Forms.Button
    Friend WithEvents InputTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TextLabel As System.Windows.Forms.Label
End Class
