   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TicketManagement.ViewTicketManagement.View  5   	   7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5   	   Y UMaterial.Icons.WPF, Version=2.1.0.0, Culture=neutral, PublicKeyToken=34ed5903a64b4509S<clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPFMaterial.Icons.WPF 5   	   %    TicketManagement.View.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:TicketManagement.View     5   	   7
components clr-namespace:RFIDApp.Components     5   	   Micon<clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPF     5   	   -   5          �Name$  loginWindow��5	       � WindowStyle$
 None=�6/    � WindowStartupLocation$ CenterScreen=�6C    � Title$ Đăng nhập��5
   	   $	��800��6   $	��800��6*   $	��700��69   $	��700��6I   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   � 5      �� 5   
      PrimaryButton#��[  6   
���  ��$��30��6�   $��30��6�   $��Right=�6�   $��5,5,10,5q�6�   ��68   �� *ExitCommand+��6X   ��  �� ElementName$ loginWindow��.��5      %  Material.Icons.WPF.MaterialIcon     Kind$ 	CloseBold=�6    5      �� 5   
   
�������$��1��6$   $	��400��61   
�������$��1q�6U   $��50q�6i    �� CornerRadius$ 8��6u   {�6�   �� *
CanExecute+._�5      �� $��Center=�6   $��50q�65   .+�5      �� $��Quản lý bãi đỗ xe��6     TitleControl#��[ 64   $��30(�6Z   $��Center=�6h   $��0 0 0 70q�6�   �� 5      $��Tên đăng nhập:��6   #��[ 61   $��15(�6W   �� 5      $��Bottom=�6     PrimaryColor#��[ 61   $��1q�6]   $ 7��6q   ._�5      �� 
������ $��0q�67   
������   DefaultTextboxStyle#��[ 6�   ��6e   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�   	 J� InputBindings	 5       �� 5!      

 �� Key$
 Enter��6O    �� Command 6)   ��  �� Path$ LoginCommand�6O   .��5"   1   5#      5$      �� 5%      $��Mật khẩu:��6   #��[ 6,   $��15(�6R   ,   &RFIDApp.Components.BindablePasswordBox  5&         Password 61   �� *
PassWord+$ PropertyChanged=�	 5'      �� 5(      $
 Enter��6K    6%   �� $ LoginCommand�6K   5*      �� 5+      $��	0,30,0,10q�6   #��[  6�   $��Stretch=�6�   ��6,   �� *LoginCommand+��6M   �� *btnLoginText+��6n   �� $ loginWindow��6�   5-      5.      �� 5/   
   $��2��6   $��Italic$�6!   $��Developed by rfidstore.vn��64   $��15(�6U   $��Center=�6c   $��
20,0,20,20q�6�   51      52      