<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CryptoDialog
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ltCancel = New System.Windows.Forms.Button()
        Me.ltOK = New System.Windows.Forms.Button()
        Me.PasswordDataGridView = New System.Windows.Forms.DataGridView()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.CategoryTextBox = New System.Windows.Forms.TextBox()
        Me.UserNameTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.ltUpdate = New System.Windows.Forms.Button()
        Me.DownButton = New System.Windows.Forms.Button()
        Me.DeleteItemButton = New System.Windows.Forms.Button()
        Me.NewItemButton = New System.Windows.Forms.Button()
        Me.UpButton = New System.Windows.Forms.Button()
        Me.ltExport = New System.Windows.Forms.Button()
        Me.WebsiteTextBox = New System.Windows.Forms.TextBox()
        Me.CommentTextBox = New System.Windows.Forms.TextBox()
        CType(Me.PasswordDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ltCancel
        '
        Me.ltCancel.Location = New System.Drawing.Point(563, 527)
        Me.ltCancel.Name = "ltCancel"
        Me.ltCancel.Size = New System.Drawing.Size(75, 23)
        Me.ltCancel.TabIndex = 3
        Me.ltCancel.Text = "XXX"
        Me.ltCancel.UseVisualStyleBackColor = True
        '
        'ltOK
        '
        Me.ltOK.Location = New System.Drawing.Point(464, 527)
        Me.ltOK.Name = "ltOK"
        Me.ltOK.Size = New System.Drawing.Size(93, 23)
        Me.ltOK.TabIndex = 2
        Me.ltOK.Text = "XXX"
        Me.ltOK.UseVisualStyleBackColor = True
        '
        'PasswordDataGridView
        '
        Me.PasswordDataGridView.AllowUserToAddRows = False
        Me.PasswordDataGridView.AllowUserToResizeColumns = False
        Me.PasswordDataGridView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PasswordDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.PasswordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PasswordDataGridView.Location = New System.Drawing.Point(12, 12)
        Me.PasswordDataGridView.MultiSelect = False
        Me.PasswordDataGridView.Name = "PasswordDataGridView"
        Me.PasswordDataGridView.RowHeadersVisible = False
        Me.PasswordDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordDataGridView.RowTemplate.Height = 16
        Me.PasswordDataGridView.RowTemplate.ReadOnly = True
        Me.PasswordDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PasswordDataGridView.ShowCellToolTips = False
        Me.PasswordDataGridView.Size = New System.Drawing.Size(596, 484)
        Me.PasswordDataGridView.TabIndex = 35
        '
        'NameTextBox
        '
        Me.NameTextBox.Location = New System.Drawing.Point(12, 501)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(40, 20)
        Me.NameTextBox.TabIndex = 36
        Me.NameTextBox.Visible = False
        '
        'CategoryTextBox
        '
        Me.CategoryTextBox.Location = New System.Drawing.Point(58, 501)
        Me.CategoryTextBox.Name = "CategoryTextBox"
        Me.CategoryTextBox.Size = New System.Drawing.Size(40, 20)
        Me.CategoryTextBox.TabIndex = 37
        Me.CategoryTextBox.Visible = False
        '
        'UserNameTextBox
        '
        Me.UserNameTextBox.Location = New System.Drawing.Point(104, 502)
        Me.UserNameTextBox.Name = "UserNameTextBox"
        Me.UserNameTextBox.Size = New System.Drawing.Size(40, 20)
        Me.UserNameTextBox.TabIndex = 38
        Me.UserNameTextBox.Visible = False
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(150, 501)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.Size = New System.Drawing.Size(40, 20)
        Me.PasswordTextBox.TabIndex = 39
        Me.PasswordTextBox.Visible = False
        '
        'ltUpdate
        '
        Me.ltUpdate.Location = New System.Drawing.Point(365, 527)
        Me.ltUpdate.Name = "ltUpdate"
        Me.ltUpdate.Size = New System.Drawing.Size(93, 23)
        Me.ltUpdate.TabIndex = 40
        Me.ltUpdate.Text = "XXX"
        Me.ltUpdate.UseVisualStyleBackColor = True
        Me.ltUpdate.Visible = False
        '
        'DownButton
        '
        Me.DownButton.Image = Global.GadecSuitcase.My.Resources.Resources.Down
        Me.DownButton.Location = New System.Drawing.Point(614, 412)
        Me.DownButton.Name = "DownButton"
        Me.DownButton.Size = New System.Drawing.Size(24, 24)
        Me.DownButton.TabIndex = 46
        Me.DownButton.UseVisualStyleBackColor = True
        '
        'DeleteItemButton
        '
        Me.DeleteItemButton.Image = Global.GadecSuitcase.My.Resources.Resources.Delete
        Me.DeleteItemButton.Location = New System.Drawing.Point(614, 442)
        Me.DeleteItemButton.Name = "DeleteItemButton"
        Me.DeleteItemButton.Size = New System.Drawing.Size(24, 24)
        Me.DeleteItemButton.TabIndex = 45
        Me.DeleteItemButton.UseVisualStyleBackColor = True
        '
        'NewItemButton
        '
        Me.NewItemButton.Image = Global.GadecSuitcase.My.Resources.Resources.NewItem
        Me.NewItemButton.Location = New System.Drawing.Point(614, 472)
        Me.NewItemButton.Name = "NewItemButton"
        Me.NewItemButton.Size = New System.Drawing.Size(24, 24)
        Me.NewItemButton.TabIndex = 44
        Me.NewItemButton.UseVisualStyleBackColor = True
        '
        'UpButton
        '
        Me.UpButton.Image = Global.GadecSuitcase.My.Resources.Resources.Up
        Me.UpButton.Location = New System.Drawing.Point(614, 382)
        Me.UpButton.Name = "UpButton"
        Me.UpButton.Size = New System.Drawing.Size(24, 24)
        Me.UpButton.TabIndex = 43
        Me.UpButton.UseVisualStyleBackColor = True
        '
        'ltExport
        '
        Me.ltExport.Location = New System.Drawing.Point(266, 527)
        Me.ltExport.Name = "ltExport"
        Me.ltExport.Size = New System.Drawing.Size(93, 23)
        Me.ltExport.TabIndex = 47
        Me.ltExport.Text = "XXX"
        Me.ltExport.UseVisualStyleBackColor = True
        '
        'WebsiteTextBox
        '
        Me.WebsiteTextBox.Location = New System.Drawing.Point(196, 501)
        Me.WebsiteTextBox.Name = "WebsiteTextBox"
        Me.WebsiteTextBox.Size = New System.Drawing.Size(40, 20)
        Me.WebsiteTextBox.TabIndex = 48
        Me.WebsiteTextBox.Visible = False
        '
        'CommentTextBox
        '
        Me.CommentTextBox.Location = New System.Drawing.Point(242, 501)
        Me.CommentTextBox.Name = "CommentTextBox"
        Me.CommentTextBox.Size = New System.Drawing.Size(40, 20)
        Me.CommentTextBox.TabIndex = 49
        Me.CommentTextBox.Visible = False
        '
        'PasswordsDialog
        '
        Me.AcceptButton = Me.ltOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 562)
        Me.Controls.Add(Me.CommentTextBox)
        Me.Controls.Add(Me.WebsiteTextBox)
        Me.Controls.Add(Me.ltExport)
        Me.Controls.Add(Me.DownButton)
        Me.Controls.Add(Me.DeleteItemButton)
        Me.Controls.Add(Me.NewItemButton)
        Me.Controls.Add(Me.UpButton)
        Me.Controls.Add(Me.ltUpdate)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UserNameTextBox)
        Me.Controls.Add(Me.CategoryTextBox)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.PasswordDataGridView)
        Me.Controls.Add(Me.ltCancel)
        Me.Controls.Add(Me.ltOK)
        Me.Name = "PasswordsDialog"
        Me.Text = "XXX"
        CType(Me.PasswordDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ltCancel As System.Windows.Forms.Button
    Friend WithEvents ltOK As System.Windows.Forms.Button
    Friend WithEvents PasswordDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CategoryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ltUpdate As System.Windows.Forms.Button
    Friend WithEvents DownButton As System.Windows.Forms.Button
    Friend WithEvents DeleteItemButton As System.Windows.Forms.Button
    Friend WithEvents NewItemButton As System.Windows.Forms.Button
    Friend WithEvents UpButton As System.Windows.Forms.Button
    Friend WithEvents ltExport As System.Windows.Forms.Button
    Friend WithEvents WebsiteTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CommentTextBox As System.Windows.Forms.TextBox
End Class
