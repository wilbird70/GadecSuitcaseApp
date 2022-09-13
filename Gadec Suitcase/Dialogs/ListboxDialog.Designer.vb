<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListboxDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListboxDialog))
        Me.InputListBox = New System.Windows.Forms.ListBox()
        Me.TextLabel = New System.Windows.Forms.Label()
        Me.ltCancel = New System.Windows.Forms.Button()
        Me.ltOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'InputListBox
        '
        Me.InputListBox.FormattingEnabled = True
        Me.InputListBox.Location = New System.Drawing.Point(12, 25)
        Me.InputListBox.Name = "InputListBox"
        Me.InputListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.InputListBox.Size = New System.Drawing.Size(626, 147)
        Me.InputListBox.TabIndex = 0
        '
        'TextLabel
        '
        Me.TextLabel.AutoSize = True
        Me.TextLabel.Location = New System.Drawing.Point(12, 9)
        Me.TextLabel.Name = "TextLabel"
        Me.TextLabel.Size = New System.Drawing.Size(28, 13)
        Me.TextLabel.TabIndex = 1
        Me.TextLabel.Text = "XXX"
        '
        'ltCancel
        '
        Me.ltCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltCancel.Location = New System.Drawing.Point(563, 178)
        Me.ltCancel.Name = "ltCancel"
        Me.ltCancel.Size = New System.Drawing.Size(75, 23)
        Me.ltCancel.TabIndex = 15
        Me.ltCancel.Text = "XXX"
        Me.ltCancel.UseVisualStyleBackColor = True
        '
        'ltOK
        '
        Me.ltOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltOK.Location = New System.Drawing.Point(482, 178)
        Me.ltOK.Name = "ltOK"
        Me.ltOK.Size = New System.Drawing.Size(75, 23)
        Me.ltOK.TabIndex = 16
        Me.ltOK.Text = "XXX"
        Me.ltOK.UseVisualStyleBackColor = True
        '
        'Listbox
        '
        Me.AcceptButton = Me.ltOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltCancel
        Me.ClientSize = New System.Drawing.Size(650, 211)
        Me.Controls.Add(Me.ltOK)
        Me.Controls.Add(Me.ltCancel)
        Me.Controls.Add(Me.TextLabel)
        Me.Controls.Add(Me.InputListBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Listbox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "XXX"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InputListBox As System.Windows.Forms.ListBox
    Friend WithEvents TextLabel As System.Windows.Forms.Label
    Friend WithEvents ltCancel As System.Windows.Forms.Button
    Friend WithEvents ltOK As System.Windows.Forms.Button
End Class
